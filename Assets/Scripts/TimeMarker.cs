using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimeMarker : Marker, INotification,INotificationOptionProvider
{
    [SerializeField] private string message = "";

    [Space(20)]
    [SerializeField] private bool retroactive = false;
    [SerializeField] private bool emitOnce = false;

    public PropertyName id => new PropertyName();
    public string Message => message;

    public NotificationFlags flags =>
        (retroactive ? NotificationFlags.Retroactive : default) |
        (emitOnce ? NotificationFlags.TriggerOnce : default);
}
