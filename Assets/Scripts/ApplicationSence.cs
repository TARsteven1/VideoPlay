using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace QHStudio.Game
{
    /// <summary>
    /// 切换场景
    /// </summary>
    public class ApplicationSence : MonoBehaviour
    {
        AsyncOperation async;
        private int progress = 0;
        public enum LoadType { Single, Additive }
        public LoadType loadType = LoadType.Single;
        [HideInInspector]
        public List<GameObject> notDestroy;
        public BaseEvent.FloatEvent progressEvent;
        void Start()
        { 
            if (notDestroy == null || notDestroy.Count < 1) return;
            for (int i = 0; i < notDestroy.Count; i++)
            {
                if (notDestroy[i])
                    DontDestroyOnLoad(notDestroy[i]);
            }
            SystemLanguage language = Application.systemLanguage;
            switch (language)
            {
                case SystemLanguage.ChineseSimplified:

                    break;
                case SystemLanguage.ChineseTraditional:

                    break;
                case SystemLanguage.English:

                    break;

                default:

                    break;
            }

           // SceneManager.activeSceneChanged += changeScene;
        }

        void Update()
        {
            if (async != null)
            {
                progress = (int)(async.progress * 100);
                progressEvent.Invoke(async.progress);
                if (progress >= 100) async = null;
            }
        }
        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="num"></param>
        public void changeApplication(int num)
        {
            switch (loadType)
            {
                case LoadType.Single:
                    SceneManager.LoadScene(num, LoadSceneMode.Single);
                    notDes();
                    break;
                case LoadType.Additive:
                    SceneManager.LoadScene(num, LoadSceneMode.Additive);
                    notDes();
                    break;
            }
        }
        //加载当前场景

        public void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //加载下一个场景

        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        /// <summary>
        /// 加载切换场景 按序号
        /// </summary>
        /// <param name="num"></param>
        public void loadScene(int num)
        {
            switch (loadType)
            {
                case LoadType.Single:
                    SceneManager.LoadScene(num, LoadSceneMode.Single);
                    notDes();
                    break;
                case LoadType.Additive:
                    SceneManager.LoadScene(num, LoadSceneMode.Additive);
                    notDes();
                    break;
            }
        }
        /// <summary>
        /// 加载场景 按名字
        /// </summary>
        /// <param name="name"></param>
        public void loadSceneName(string name)
        {
            switch (loadType)
            {
                case LoadType.Single:
                    SceneManager.LoadScene(name, LoadSceneMode.Single);
                    notDes();
                    break;
                case LoadType.Additive:
                    SceneManager.LoadScene(name, LoadSceneMode.Additive);
                    notDes();
                    break;
            }
        }
        /// <summary>
        /// 切换场景  异步  按序号
        /// </summary>
        /// <param name="num"></param>
        public void loadSceneAsync(int num)
        {
            StartCoroutine(load(num));
        }
        /// <summary>
        /// 切换场景  异步  按名字
        /// </summary>
        /// <param name="name"></param>
        public void loadSceneAsync(string name)
        {
            StartCoroutine(load(name));
        }
        /// <summary>
        /// 卸载场景  按序号 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public void unloadScenen(int num)
        {
            SceneManager.UnloadScene(num);
        }
        /// <summary>
        /// 卸载场景 按名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void unloadScenen(string name)
        {
            SceneManager.UnloadScene(name);

        }
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerator load(string name)
        {
            yield return new WaitForEndOfFrame();
            switch (loadType)
            {
                case LoadType.Single:
                    async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
                    notDes();
                    break;
                case LoadType.Additive:
                    async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
                    notDes();
                    break;
            }
            yield return async;
        }
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        IEnumerator load(int num)
        {
            yield return new WaitForEndOfFrame();
            switch (loadType)
            {
                case LoadType.Single:
                    async = SceneManager.LoadSceneAsync(num, LoadSceneMode.Single);
                    notDes();
                    break;
                case LoadType.Additive:
                    async = SceneManager.LoadSceneAsync(num, LoadSceneMode.Additive);
                    notDes();
                    break;
            }
            yield return async;
        }


        /// <summary>
        /// 取消销毁
        /// </summary>
        void notDes()
        {
            if (notDestroy == null || notDestroy.Count < 1) return;
            for (int i = 0; i < notDestroy.Count; i++)
            {
                if (notDestroy[i])
                    DontDestroyOnLoad(notDestroy[i]);
            }
        }
        /// <summary>
        /// 设置取消销毁
        /// </summary>
        /// <param name="obj"></param>
        public void setNotDestroy(GameObject obj)
        {
            if (!notDestroy.Contains(obj))
                this.notDestroy.Add(obj);
        }
        /// <summary>
        /// 手动销毁
        /// </summary>
        public void DestroyObj()
        {
            if (notDestroy == null || notDestroy.Count < 1) return;
            for (int i = 0; i < notDestroy.Count; i++)
            {
                if (notDestroy[i])
                    Destroy(notDestroy[i]);
            }
        }
        /// <summary>
        /// 退出
        /// </summary>
        public void Quit()
        {
            Application.Quit();
        }
        /// <summary>
        /// 打开URL
        /// </summary>
        /// <param name="url"></param>
        public void OpenUrl(string url)
        {
            if (url != null && url.Length > 0)
                Application.OpenURL(url);
        }
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="t"></param>
        public void setTimeScale(float t)
        {
            Time.timeScale=t;
        }

    }
}
