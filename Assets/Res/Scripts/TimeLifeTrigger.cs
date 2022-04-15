using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeLifeTrigger : MonoBehaviour
{

    public enum LifeTriggerType
    {
        non, onEnable, onDisable, onDestroy, onVisiable, onInvisible, onAwake, onStart,
        onApplicationFocus, onApplicationPause, onMouseDown, onMouseDrag, onMouseEnter, onMouseExit, onMouseOver,
        onMouseUp, onMouseUpAsButton, onTransformChildrenChanged, onTransformParentChanged, onReset
    }
    public List<TriggerConfig> triggers = new List<TriggerConfig>();

    private void Awake()
    {
        excute(LifeTriggerType.onAwake);
    }

    void Start()
    {
        excute(LifeTriggerType.onStart);
    }
    private void OnEnable()
    {
        excute(LifeTriggerType.onEnable);
    }

    private void OnDisable()
    {
        excute(LifeTriggerType.onDisable);
    }

    private void OnDestroy()
    {
        excute(LifeTriggerType.onDestroy);
    }

    private void OnBecameVisible()
    {
        excute(LifeTriggerType.onVisiable);
    }

    private void OnBecameInvisible()
    {
        excute(LifeTriggerType.onInvisible);
    }

    private void OnApplicationFocus(bool focus)
    {
        excute(LifeTriggerType.onApplicationFocus);
    }

    private void OnApplicationPause(bool pause)
    {
        excute(LifeTriggerType.onApplicationPause);
    }

    private void OnMouseDown()
    {
        excute(LifeTriggerType.onMouseDown);
    }

    private void OnMouseDrag()
    {
        excute(LifeTriggerType.onMouseDrag);
    }

    private void OnMouseEnter()
    {
        excute(LifeTriggerType.onMouseEnter);
    }

    private void OnMouseExit()
    {
        excute(LifeTriggerType.onMouseExit);
    }

    private void OnMouseOver()
    {
        excute(LifeTriggerType.onMouseOver);
    }

    private void OnMouseUp()
    {
        excute(LifeTriggerType.onMouseUp);
    }

    private void OnMouseUpAsButton()
    {
        excute(LifeTriggerType.onMouseUpAsButton);
    }

    private void OnTransformChildrenChanged()
    {
        excute(LifeTriggerType.onTransformChildrenChanged);
    }

    private void OnTransformParentChanged()
    {
        excute(LifeTriggerType.onTransformParentChanged);
    }

    private void Reset()
    {
        excute(LifeTriggerType.onReset);
    }


    void excute(LifeTriggerType type)
    {
        if (triggers == null || triggers.Count < 1) return;
        List<TriggerConfig> temps = triggers.FindAll(a => { return a.type == type; });
        for (int a = 0; a < temps.Count; a++)
        {
            temps[a].excute();
        }
    }

    [System.Serializable]
    public class LifeEvent : UnityEvent { }
    [System.Serializable]
    public class TriggerConfig
    {
        public LifeTriggerType type = LifeTriggerType.non;
        public LifeEvent lifeEvent;
        public void excute()
        {
            lifeEvent.Invoke();
        }
    }


}
