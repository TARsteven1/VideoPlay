using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeed : MonoBehaviour
{
    public float defaultspeed = 1;

    void Start()
    {
        setscale(defaultspeed);
    }
    public void setscale(float value)
    {
        Time.timeScale = value;

    }

}
