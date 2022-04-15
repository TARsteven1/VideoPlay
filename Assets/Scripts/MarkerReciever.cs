using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MarkerReciever : MonoBehaviour, INotificationReceiver
{
    public int markerNum;

    public void OnNotify(Playable origin, INotification notification, object context)
    {

        if(notification  is TimeMarker timeMarker)
        {
            
        }
    }
}
