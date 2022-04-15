using UnityEngine;
using System.Collections;

namespace QHStudio.Game
{
    public class MoveTowards : MonoBehaviour
    {

        // Use this for initialization
        public Transform targetPostion;
        public bool tracking = false;
        Vector3 target;

        public float _speed = 1.0f;
        bool run = true;
        void Start()
        {
            target = transform.position;

            if (targetPostion)
                target = targetPostion.position;

        }

        // Update is called once per frame
        void Update()
        {
            if (tracking && targetPostion) target = targetPostion.position;
           if(run)transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _speed);
        }

        public void setTarget(Transform target)
        {
            targetPostion = target;
           this.target = targetPostion.position;
            run = true;
        }

        public void stop()
        {
            run = false;
        }
    }
}