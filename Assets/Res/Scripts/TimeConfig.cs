using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace QHStudio.Game
{
    public class TimeConfig : MonoBehaviour
    {
        public List<ConfigItem> configList;
        // Use this for initialization
        private bool start = false;
        float keepTime = 0;
        float keepDown = 0;

        List<ConfigItem> disRun = new List<ConfigItem>();
        void Awake()
        {
            foreach (ConfigItem item in configList)
            {
                item.enabled = true;
                item.excuteTime = Time.time;
            }

            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.AWAKE))
            {
                StartCoroutine(excute(item, item.Countdown));
            }
        }
        void Start()
        {
            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.START))
            {
                StartCoroutine(excute(item, item.Countdown));
            }
        }

        // Update is called once per frame
        void Update()
        {


            //foreach (ConfigItem item in configList)
            //{
            //    if (item.excute && item.enabled)
            //    {
            //        item.configEvent.Invoke();
            //        item.ExecuteCount += 1;
            //        item.excuteTime = Time.time;
            //        switch (item.executeType)
            //        {
            //            case ConfigItem.ExecuteType.Keep:
            //                item.excute = true;
            //                break;
            //            case ConfigItem.ExecuteType.Non:
            //                item.excute = false;
            //                break;
            //            case ConfigItem.ExecuteType.Once:
            //                item.enabled = false;
            //                break;
            //        }
            //    }
            //}

            for (int i = 0; i < configList.Count; i++)
            {
                if (configList[i].excute && configList[i].enabled)
                {
                    configList[i].configEvent.Invoke();
                    configList[i].ExecuteCount += 1;
                    configList[i].excuteTime = Time.time;
                    switch (configList[i].executeType)
                    {
                        case ConfigItem.ExecuteType.Keep:
                            configList[i].excute = true;
                            break;
                        case ConfigItem.ExecuteType.Non:
                            configList[i].excute = false;
                            break;
                        case ConfigItem.ExecuteType.Once:
                            configList[i].enabled = false;
                            break;
                    }
                }
            }

            //foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.UPDATE))
            //{
            //    if (Time.time - item.excuteTime > item.Countdown && item.enabled)
            //    {
            //        item.excute = true;
            //    }
            //}

            List<ConfigItem> tempU = getFindType(ConfigItem.ConfigType.UPDATE);
            for (int i = 0; i < tempU.Count; i++)
            {
                if (Time.time - tempU[i].excuteTime > tempU[i].Countdown && tempU[i].enabled)
                {
                    tempU[i].excute = true;
                }
            }

            if (start)
            {
                //foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.KEEP))
                //{
                //    if (Time.time - item.excuteTime > item.Countdown && item.enabled)
                //    {
                //        item.excute = true;
                //    }
                //}

                List<ConfigItem> tempK = getFindType(ConfigItem.ConfigType.KEEP);
                for (int i = 0; i < tempK.Count; i++)
                {
                    //if (Time.time - tempK[i].excuteTime > tempK[i].Countdown && tempK[i].enabled)
                    //{
                    //    tempK[i].excute = true;
                    //}

                    if (keepTime > tempK[i].Countdown && tempK[i].enabled && Time.time - tempK[i].excuteTime > tempK[i].Countdown)
                    {
                        tempK[i].excute = true;
                    }
                }
            }

            if (Time.time - keepDown > 0.1f && start)
            {
                keepTime = 0;
                start = false;
            }
        }

        void OnEnable()
        {
            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.ENABLE))
            {
                StartCoroutine(excute(item, item.Countdown));
            }
        }
        void OnDisable()
        {
            if (gameObject.activeInHierarchy)
            {

                foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.DISABLE))
                {
                    StartCoroutine(excute(item, item.Countdown, EXCUTE_ATONCE));
                    //  item.configEvent.Invoke();
                }
            }

            disRun.Clear();
            disRun.AddRange(getFindType(ConfigItem.ConfigType.DISABLE));
            Invoke("disRunAction", 0);
        }
        void disRunAction()
        {
            if (disRun == null || disRun.Count < 1) return;

            foreach (ConfigItem item in disRun)
            {
                // StartCoroutine(excute(item, item.Countdown, EXCUTE_ATONCE));
                item.configEvent.Invoke();
            }
            disRun.Clear();
        }

        void OnDestroy()
        {
            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.DESTROY))
            {
                //  StartCoroutine(excute(item, item.Countdown, EXCUTE_ATONCE));
                item.configEvent.Invoke();
            }
        }


        public void customAction()
        {
            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.COSTOM))
            {
                StartCoroutine(excute(item, item.Countdown));
            }
        }

        public void OnKeep(bool keep)
        {
            if (keep) keepDown = Time.time;
            keepTime += Time.deltaTime;

            if (!keep)
            {
                keepDown = 0;
                keepTime = 0;
            }
            start = keep;
        }

        public void reset()
        {
            foreach (ConfigItem item in configList)
            {
                item.enabled = true;
            }

            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.AWAKE))
            {
                StartCoroutine(excute(item, item.Countdown));
            }

            foreach (ConfigItem item in getFindType(ConfigItem.ConfigType.START))
            {
                StartCoroutine(excute(item, item.Countdown));
            }
        }

        public void testOutPut(string s)
        {
            Debug.Log(s);
        }
        List<ConfigItem> getFindType(ConfigItem.ConfigType type)
        {

            return configList.FindAll(config =>
            {
                return config.configType.Equals(type) && config.enabled;
            });
        }

        List<ConfigItem> getFindType(ConfigItem.ConfigType type, string info)
        {

            return configList.FindAll(config =>
            {
                return config.configType.Equals(type)
                            && (config.configName.Equals(info)
                                        || config.configName.Equals(info)) && config.enabled;
            });
        }

        IEnumerator excute(ConfigItem item, float time)
        {
            yield return new WaitForSeconds(time);
            if (item.independent)
            {
                item.excute = true;
                run(item);
            }
            else
            {
                item.excute = true;
            }
        }

        const int EXCUTE_ATONCE = 1;
        IEnumerator excute(ConfigItem item, float time, int sp)
        {
            switch (sp)
            {
                case EXCUTE_ATONCE:
                    item.excute = true;
                    run(item);
                    break;
                default:
                    //   item.excute = true;
                    break;
            }

            yield return new WaitForSeconds(time);
            switch (sp)
            {
                case EXCUTE_ATONCE:
                    // run(item);
                    break;
                default:
                    item.excute = true;
                    break;
            }
        }
        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="item"></param>
        void run(ConfigItem item)
        {
            if (item.excute && item.enabled)
            {
                item.configEvent.Invoke();
                item.ExecuteCount += 1;
                item.excuteTime = Time.time;
                switch (item.executeType)
                {
                    case ConfigItem.ExecuteType.Keep:
                        item.excute = true;
                        break;
                    case ConfigItem.ExecuteType.Non:
                        item.excute = false;
                        break;
                    case ConfigItem.ExecuteType.Once:
                        item.enabled = false;
                        break;
                }
            }
        }
        [System.Serializable]
        public class ConfigItem
        {
            public enum ConfigType { NON, AWAKE, START, UPDATE, ENABLE, COSTOM, KEEP, DISABLE, DESTROY }
            public enum ExecuteType { Non, Keep, Once }
            public string configName;
            public ConfigType configType = ConfigType.NON;
            public ExecuteType executeType = ExecuteType.Non;
            public float Countdown = 4f;
            public bool independent = false;
            [HideInInspector]
            public bool enabled = true;
            [HideInInspector]
            public bool excute = false;
            [HideInInspector]
            public int ExecuteCount = 0;
            [HideInInspector]
            public float excuteTime = 0f;

            public UnityEvent configEvent;

        }
    }
}
