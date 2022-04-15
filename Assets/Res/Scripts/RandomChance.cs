using UnityEngine;
using System.Collections.Generic;

namespace QHStudio.Game
{
    public class RandomChance : MonoBehaviour
    {
        public enum RunType { axes, line, random, cout, surplus }
        public RunType runType = RunType.axes;
        public List<ChanceConfig> changeConfig;

        float max = 0;
        float itemMax = 0;
        int state = -1;

        List<int> surplusList = new List<int>();
        void Awake()
        {
            if (state == -1) init();
        }

        void OnEnable()
        {
           // excute();
        }

        void init()
        {
            if (changeConfig == null) return;
            float start = 0;
            float end = 0;
            for (int i = 0; i < changeConfig.Count; i++)
            {
                end = start + changeConfig[i].chance;
                changeConfig[i].line = new ValueLine(start, end);
                if (changeConfig[i].chance > itemMax) itemMax = changeConfig[i].chance;
                start = end;
                max = end;
                //  Debug.Log(string.Format("start:{0},end:{1},item max:{2}", changeConfig[i].line.Start, changeConfig[i].line.End,itemMax));
            }
            state = 0;
        }

        public void setChange(string value)
        {
            string[] ar = value.Split('#');
            if (ar.Length == 2)
            {

                ChanceConfig temp = changeConfig.Find(a => { return a.name.CompareTo(ar[0]) == 0; });

                float ch = 0;
                if (float.TryParse(ar[1], out ch))
                {
                    ch = ch < 0 ? 0 : (ch > 100 ? 100 : ch);
                    if (temp != null) temp.chance = ch;
                }
            }
        }

        public void debugInfo(string info)
        {
            Debug.Log(info);
        }

        public void excute()
        {
            if (state == -1) init();

            if (changeConfig == null) return;
            switch (runType)
            {
                case RunType.axes:
                    runAxes();
                    break;
                case RunType.line:
                    runLine();
                    break;
                case RunType.cout:
                    runCount();
                    break;
                case RunType.random:
                    runRandom();
                    break;
                case RunType.surplus:
                    runSurplus();
                    break;
            }
        }

        void runAxes()
        {
            float r = Random.Range(0f, max);
            ChanceConfig f = changeConfig.Find(a => ((a.line.Start <= r) && (a.line.End > r)));
            if (f != null)
            {
                f.excute();
            }
        }

        void runLine()
        {
            for (int i = 0; i < changeConfig.Count; i++)
            {
                if (changeConfig[i].randomChance()) return;
            }
        }


        void runCount()
        {
            for (int i = 0; i < changeConfig.Count; i++)
            {
                if (changeConfig[i].randomChance()) return;
            }
            runAxes();
        }

        void runRandom()
        {
            for (int i = 0; i < changeConfig.Count; i++)
            {
                int t = Random.Range(0, changeConfig.Count);
                if (changeConfig[t].randomChance()) return;
            }
        }

        void runSurplus()
        {
            if (surplusList == null) surplusList = new List<int>();
            if (surplusList.Count == 0) creatSurplus();
            if (surplusList.Count < 1) return;
            int rd = Random.Range(0, surplusList.Count);
            int run = surplusList[rd];
            changeConfig[run].excute();
            surplusList.RemoveAt(rd);
        }

        void creatSurplus()
        {
            for (int a = 0; a < changeConfig.Count; a++)
            {
                for (int b = 0; b < (int)changeConfig[a].chance; b++)
                {
                    surplusList.Add(a);
                }
            }
        }

        [System.Serializable]
        public class ChanceConfig
        {
            public string name;
            [Range(0f, 100f)]
            public float chance = 0f;
            public ValueLine line;
            public BaseEvent.EmptyEvent changeEvent;

            public bool excute(float value)
            {
                if (value >= line.Start && value < line.End)
                {
                    changeEvent.Invoke();
                    return true;
                }
                return false;
            }

            public bool randomChance()
            {
                float a = Random.Range(0f, 100f);
                if (a < chance)
                {
                    changeEvent.Invoke();
                    return true;
                }

                return false;
            }
            public bool excute()
            {
                changeEvent.Invoke();
                return true;
            }

        }
    }
}