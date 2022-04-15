using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CopyrightMonitor : MonoBehaviour
{
    private int ischeckCodeOk;

    private DateTime PresetDateTime;

    public string Year,Mouth,Day;

    public UnityEvent TimeOutEvent;
    void Start()
    {
        ischeckCodeOk = 0;
        LoadData();
        if (ischeckCodeOk!=0)
        {
            return;
        }
        else
        {
            checkDateTime();
        }
        
    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
private void checkDateTime()
    {
        PresetDateTime = Convert.ToDateTime(Year + "/" + Mouth + "/" + Day);
        Debug.Log(PresetDateTime);

        DateTime CurDateTime = DateTime.Now;
        int CompareCur = int.Parse(CurDateTime.ToString("yyyyMMdd"));
        int ComparePreset = int.Parse(PresetDateTime.ToString("yyyyMMdd"));
        Debug.Log(CompareCur + ">" + ComparePreset);

        if (CompareCur >= ComparePreset)
        {
            ischeckCodeOk = 1;
            TimeOutEvent.Invoke();
        }
        SaveData();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("ischeckCodeOk"))
            ischeckCodeOk = PlayerPrefs.GetInt("ischeckCodeOk");
    }
    public void SaveData()
    {
            PlayerPrefs.SetInt("ischeckCodeOk", ischeckCodeOk);

    }




}
