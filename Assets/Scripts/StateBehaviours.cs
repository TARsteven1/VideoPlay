using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QHStudio.Game
{
    public class StateBehaviours : MonoBehaviour
    {
        public int maxCache = 0;
        public bool recordCache = false;
        public List<StateInfo> states;

        List<string> cacheRun = new List<string>();
        int current = -1;
        public string StateCache
        {
            set;
            get;
        }
        public void excuteCache()
        {
            if (StateCache == null || StateCache.Length < 1) return;
            excuteState(StateCache);
        }
        public void excuteIndexCache()
        {
            if (StateCache == null || StateCache.Length < 1) return;
            int a = -1;
            if (int.TryParse(StateCache, out a))
            {
                excuteIndex(a);
            }
        }

        void excute(string name)
        {
            if (name == null || name.Length < 1) return;
            if (name.ToLower().CompareTo("back") == 0 && maxCache > 0)
            {
                runPrevious();
            }
            else if (name.ToLower().CompareTo("next") == 0 && maxCache > 0)
            {
                runNext();
            }

            else
            {
                StateInfo temp = getState(name);
                run(temp, true);
            }
        }

        public void excuteState(string name)
        {
            string[] arr = name.Split(';');
            foreach (string temp in arr)
            {
                excute(temp);
            }
        }

        public void excuteIndex(int index)
        {
            if (index < 0 || index > states.Count) return;
            run(states[index], true);
        }

        public void excuteIndexName(int name)
        {
            excute(name + "");
        }

        public void runPrevious()
        {
            if (cacheRun.Count < 1) return;
            current--;

            runCache(current);
            current = current < 0 ? 0 : current;
        }

        public void runNext()
        {
            if (cacheRun.Count < 1) return;
            current++;
            runCache(current);
            current = current > cacheRun.Count - 1 ? cacheRun.Count - 1 : current;
        }

        public void closeState(string value)
        {
            if (value.CompareTo("?") == 0 || value.CompareTo("*") == 0)
            {
                cloaseAll();
                return;
            }
            string[] arr = value.Split(';');
            for (int a = 0; a < arr.Length; a++)
            {
                setStateEnable(getState(arr[a]), false);
            }
        }
        public void cloaseAll()
        {
            for (int a = 0; a < states.Count; a++)
            {
                setStateEnable(states[a], false);
            }
        }
        public void openState(string value)
        {
            if (value.CompareTo("?") == 0 || value.CompareTo("*") == 0)
            {
                openAll();
                return;
            }
            string[] arr = value.Split(';');
            for (int a = 0; a < arr.Length; a++)
            {
                setStateEnable(getState(arr[a]), true);
            }
        }
        public void openAll()
        {
            for (int a = 0; a < states.Count; a++)
            {
                setStateEnable(states[a], true);
            }
        }

        public void useState(string value)
        {
            List<string> arr = new List<string>();
            arr.AddRange(value.Split(';'));
            for (int a = 0; a < states.Count; a++)
            {
                setStateEnable(states[a], arr.Contains(states[a].stateName));
            }
        }

        void setStateEnable(StateInfo state, bool use)
        {
            if (state == null) return;
            state.enable = use;
        }

        void run(StateInfo item, bool cache)
        {
            if (item == null) return;
            item.excute();
            if (maxCache > 0 && cache)
            {
                setCache(item.stateName);
            }
        }

        void runCache(int index)
        {
            if (cacheRun == null) return;
            if (index < 0) return;
            if (index >= cacheRun.Count) return;
            string temp = cacheRun[index];
            StateInfo state = getState(temp);
            run(state, recordCache);
        }

        void setCache(string name)
        {
            if (cacheRun.Count >= maxCache)
            {
                cacheRun.RemoveAt(0);
            }
            cacheRun.Add(name);
            current = cacheRun.Count - 1;
        }

        StateInfo getState(string name)
        {
            return states.Find(a => { return a.stateName.CompareTo(name) == 0; });
        }

        [System.Serializable]
        public class StateInfo
        {
            public string stateName;
            public bool enable = true;
            public BaseEvent.StingEvent stateEvent;

            public void excute()
            {
                if (!enable) return;
                stateEvent.Invoke(stateName);
            }

        }
    }
}