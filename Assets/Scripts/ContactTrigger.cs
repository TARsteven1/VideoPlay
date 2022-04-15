using UnityEngine;
using System.Collections.Generic;

namespace QHStudio.Game
{
    public class ContactTrigger : MonoBehaviour
    {

        public bool trigger = false;
        public List<InfoConfig> config;
        //  public BaseEvent.TransformEvent transformEvent;
        public void setTrigger(bool trigger)
        {
            this.trigger = trigger;
        }

        public void excute(GameObject obj)
        {
            if (!trigger) return;
            //  Debug.Log(obj.name);
            for (int i = 0; i < config.Count; i++)
            {
                config[i].excute(obj);
            }
        }

        public void excute(Transform obj)
        {
            if (!trigger) return;
            excute(obj.gameObject);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnCollisionEnter);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(collision.gameObject);
            }
           // 
        }

        void OnCollisionExit(Collision collision)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnCollisionExit);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(collision.gameObject);
            }
        }

        void OnCollisionStay(Collision collision)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnCollisionStay);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(collision.gameObject);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnTriggerEnter);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(other.gameObject);
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnTriggerExit);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(other.gameObject);
            }
        }
        void OnTriggerStay(Collider other)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnTriggerStay);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(other.gameObject);
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!trigger) return;
            List<InfoConfig> temp = getList(ContactType.OnControllerColliderHit);
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i].excute(hit.gameObject);
            }
        }

        List<InfoConfig> getList(ContactType type)
        {

            return config.FindAll(a => { return a.contackType == type; });
        }

        [System.Serializable]
        public class InfoConfig
        {
            public string configName;
            public ContactType contackType = ContactType.OnCollisionEnter;
            public GameObjectInfoBase info;
            public BaseEvent.TransformEvent infoEvent;

            public void excute(GameObject obj)
            {

                bool temp = info.compareInfo(obj);
                //  Debug.Log(obj.name + ">>>" + contackType+">>>"+temp);
                if (temp) infoEvent.Invoke(obj.transform);
            }
        }
    }
}
