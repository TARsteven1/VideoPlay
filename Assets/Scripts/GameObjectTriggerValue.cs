using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTriggerValue : MonoBehaviour
{

    public delegate void GameObjectTrigger(GameObject controller);
    
    public event GameObjectTrigger StartTrigger,
                                   AwakeTrigger,
                                   OnVisibleTrigger,
                                   OnInvisibleTrigger,
                                   OnDestroyTrigger,
                                   OnDisableTrigger,
                                   OnEnableTrigger;
    public event GameObjectTrigger TriggerEnterListener,
                                   TriggerExitListener,
                                   CollisionEnterListener,
                                   CollisionExitListener,
                                   OnControllerHitListener;


    private void Awake()
    {
        if (AwakeTrigger != null) AwakeTrigger.Invoke(gameObject);
    }

    private void Start()
    {
        if (StartTrigger != null) StartTrigger.Invoke(gameObject);
    }

    private void OnBecameInvisible()
    {
        if (OnInvisibleTrigger != null) OnInvisibleTrigger.Invoke(gameObject);
    }

    private void OnBecameVisible()
    {
        if (OnVisibleTrigger != null) OnVisibleTrigger.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        if (OnDestroyTrigger != null) OnDestroyTrigger.Invoke(gameObject);
    }

    private void OnDisable()
    {
        if (OnDisableTrigger != null) OnDisableTrigger.Invoke(gameObject);
    }

    private void OnEnable()
    {
        if (OnEnableTrigger != null) OnEnableTrigger.Invoke(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (CollisionEnterListener != null) CollisionEnterListener.Invoke(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CollisionExitListener != null) CollisionExitListener.Invoke(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerEnterListener != null) TriggerEnterListener.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (TriggerExitListener != null) TriggerExitListener.Invoke(other.gameObject);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (OnControllerHitListener != null) OnControllerHitListener.Invoke(hit.gameObject);
    }

    //private void OnParticleCollision(GameObject other)
    //{

    //}

    //private void OnParticleTrigger()
    //{

    //}


}
