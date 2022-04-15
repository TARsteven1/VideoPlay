using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Diagnostics;

public class ExceptionHandler : MonoBehaviour
{
    //是否作为异常处理者
    public bool IsHandler = true;
    //是否退出程序当异常发生时
    public bool IsQuitWhenException = true;
    //异常日志保存路径（文件夹）
    private string LogPath;
    //Bug反馈程序的启动路径
    //private string BugExePath;

    private string LastEMsg;
    private LogType LastLogT;
    private string LastTrack;
    private int reCallCount;

    private string StartTime;
    private string EndTime;



    void Awake()
    {
        LogPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
       // t1.text = LogPath;
       // BugExePath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "\\Bug.exe";
       // t2.text = BugExePath;

        //注册异常处理委托
        if (IsHandler)
        {
            Application.logMessageReceived += Handler;
            //Application.RegisterLogCallback(Handler);
           
        }
    }

    void OnDestory()
    {
        //清除注册
       // Application.RegisterLogCallback(null);
        Application.logMessageReceived -= Handler;
    }

    void Handler(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            string logPath = LogPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd HH") + ".log";
            string logPathRepeat = LogPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd HH")+"Repeat" + ".log";
            //UnityEngine.Debug.Log(logPath);
            //打印日志
            if (Directory.Exists(LogPath))
            {
                if (logString!= LastEMsg|| stackTrace!=LastTrack&&StartTime!=null)
            {
                    UnityEngine.Debug.Log(1);
                    if (LastEMsg!=null)
                    {

                    File.AppendAllText(logPath, "[Starttime]:" + StartTime + "\r\n");
                    File.AppendAllText(logPath, "[type]:" + LastLogT + "\r\n");
                    File.AppendAllText(logPath, "[exception message]:" + LastEMsg + "\r\n");
                    File.AppendAllText(logPath, "[stack trace]:" + LastTrack + "\r\n");
                    File.AppendAllText(logPath, "[Endtime]:" +DateTime.Now.ToString() + "\r\n");
                    File.AppendAllText(logPath, "[RECallCount]:" + reCallCount.ToString() + "\r\n");
                    }



                File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
                File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
                File.AppendAllText(logPath, "[exception message]:" + logString + "\r\n");
                File.AppendAllText(logPath, "[stack trace]:" + stackTrace + "\r\n");
                LastEMsg = logString;
                LastLogT = type;
                LastTrack = stackTrace;
                    reCallCount = 0;
                    StartTime = null;
            }
            else
            {
                    //File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
                    //File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
                    //File.AppendAllText(logPath, "[exception message]:" + logString + "\r\n");
                    //File.AppendAllText(logPath, "[stack trace]:" + stackTrace + "\r\n");
                    reCallCount++;
                    if (StartTime==null)
                    {
                    StartTime = DateTime.Now.ToString();
                    }
                    if (reCallCount>1&& logString!=null)
                    {

                    File.AppendAllText(logPathRepeat, "[time]:" + DateTime.Now.ToString() + "\r\n");
                    File.AppendAllText(logPathRepeat, "[type]:" + type.ToString() + "\r\n");
                    File.AppendAllText(logPathRepeat, "[exception message]:" + logString + "\r\n");
                    File.AppendAllText(logPathRepeat, "[stack trace]:" + stackTrace + "\r\n");
                    File.AppendAllText(logPathRepeat, "[RECallCount]:" + reCallCount.ToString() + "\r\n");
                    }

                    // File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
                }
            }

            ////启动bug反馈程序
            //if (File.Exists(BugExePath))
            //{
            //    ProcessStartInfo pros = new ProcessStartInfo();
            //    pros.FileName = BugExePath;
            //    pros.Arguments = "\"" + logPath + "\"";
            //    Process pro = new Process();
            //    pro.StartInfo = pros;
            //    pro.Start();
            //}
            ////退出程序，bug反馈程序重启主程序
            //if (IsQuitWhenException)
            //{
            //    Application.Quit();
            //}
        }
    }
  

    public List<GameObject> notDestroy;
    public static List<GameObject> tempCreat;
    private void OnEnable()
    {
        if (notDestroy == null || notDestroy.Count < 1) return;

        if (tempCreat == null) tempCreat = new List<GameObject>();

        foreach (GameObject temp in notDestroy)
        {
            if (getContainsName(temp.name))
            {
                Destroy(temp);
            }
            else
            {
                DontDestroyOnLoad(temp);
                tempCreat.Add(temp);
            }
        }
    }

    bool getContainsName(string name)
    {
        if (tempCreat == null) return false;
        for (int a = 0; a < tempCreat.Count; a++)
        {
            if (tempCreat[a].name.CompareTo(name) == 0) return true;
        }
        return false;
    }

}