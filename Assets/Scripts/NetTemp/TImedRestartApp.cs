using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Diagnostics;
using System;
using System.Globalization;

public class TImedRestartApp : MonoBehaviour
{
    private double surplusTime;
    public string tempDate;
    public string tempnum;
    public int temp1 ;

    private int setHours;
    private int setMins;
    private int setSecs;
    private bool Cancal;

    public Text Text1;
    public Text Text2;
    public Text Text3;
    public GameObject tisps;

    public int SetHours { get => setHours; set {
            if (value<=24&&value>=0)
            
            setHours = value; } }
    public int SetMins { get => setMins; set 
            {
            if(value > 60&&value>=0)
            {

        setMins = value; }
            }
        }
    public int SetSecs { get => setSecs; set { if(value > 60 && value >= 0) setSecs = value; } }

    public void Test()
    {
        CultureInfo cultures = new CultureInfo("zh-cn");
        DateTime CurDateTime = DateTime.Now;
        
        string tempDate = DateTime.Now.ToString("yyyy/MM/dd ");
       // string tempDate = DateTime.Now.ToString(cultures);
        tempDate += "2" + ":" + "00"+ ":" + "00";


        DateTime SetDateTime = Convert.ToDateTime(tempDate, cultures);
        surplusTime = SetDateTime.Subtract(CurDateTime).TotalSeconds;

        //DateTime SetDateTime = Convert.ToDateTime("2007-8-15");
        // UnityEngine.Debug.Log(SetDateTime.ToString());
    }
    private void Start()
    {
        TimedRestart();
       
    }
    public void TimedRestart()
    {
        ////获取当前时间
        //DateTime CurDateTime = DateTime.Now;
        ////第二天的00点00分00秒
        ////DateTime SetDateTime = DateTime.Now.AddDays(1).Date;
        ////string tempDate = DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss");
        //string tempDate = DateTime.Now.ToString("yyyy_MM_dd");
        //UnityEngine.Debug.Log(tempDate);
        //DateTime SetDateTime = Convert.ToDateTime ("2007-8-15"); 

        CultureInfo cultures = new CultureInfo("zh-cn");
        DateTime CurDateTime = DateTime.Now;

        string tempDate = DateTime.Now.ToString("yyyy/MM/dd ");
        // string tempDate = DateTime.Now.ToString(cultures);
        //tempDate += "2" + ":" + "00" + ":" + "00";
       // tempnum = (DateTime.Now.Minute + 1).ToString("00");
        //Text2.text = tempnum;
        tempDate += "2" + ":" + "00" + ":" + "00";
        Text1.text = tempDate.ToString();
        DateTime SetDateTime = Convert.ToDateTime(tempDate, cultures);
        UnityEngine.Debug.Log(SetDateTime.ToString());

        if (Cancal)
        {
            SetDateTime = SetDateTime.AddDays(1);
        }



        //if (SetDateTime.Hour < CurDateTime.Hour)
        //{

        //    SetDateTime = SetDateTime.AddDays(1);

        //}
        //else if (SetDateTime.Hour == CurDateTime.Hour)
        //{
        //    if (SetDateTime.Minute < CurDateTime.Minute)
        //    {
        //        SetDateTime = SetDateTime.AddDays(1);
               
                
        //    }

        //}
        Cancal = false;
        //计算两个时间相差多少秒
        surplusTime = SetDateTime.Subtract(CurDateTime).TotalSeconds;
        UnityEngine.Debug.Log(surplusTime);

        if (!IsInvoking())
        {
            Text2.text = surplusTime.ToString();
            Invoke("StartTips", (float)surplusTime-30f);
            Invoke("Restart", (float)surplusTime);
            
        }
        else
        {
            Text2.text = surplusTime.ToString();
        }
        

    }
    private void StartTips()
    {
        tisps.SetActive(true);
    }
    public void CancalInvokeBtn()
    {
        CancelInvoke("Restart");
        tisps.SetActive(false);
        Cancal = true;
        TimedRestart();

    }
    private  void Restart()
    {
        string productname = Application.productName;


        string ExecutablePath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));;
        Thread thtmp = new Thread(new ParameterizedThreadStart(run));
        object appName = ExecutablePath+ "\\"+ productname + ".exe" /*Application.ExecutablePath*/;
        Thread.Sleep(1000);
        thtmp.Start(appName);
        Application.Quit();
        //Process.GetCurrentProcess().Kill();
    }
    private static void run(object obj)
    {
        Process ps = new Process();
        ps.StartInfo.FileName = obj.ToString();
        ps.Start();
        //TImedRestartApp Tp = new TImedRestartApp();
        //Tp.TimedRestart();
        

    }
    [ContextMenu("AddTime")]
    public void AddTime()
    {
        
        tempnum = temp1++.ToString("00");
        //UnityEngine.Debug.Log(tempnum);
        tempnum = Text3.text;

    }
}
