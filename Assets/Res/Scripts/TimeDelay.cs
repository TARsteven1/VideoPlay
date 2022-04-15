using UnityEngine;
using Hvs = UnityEngine.Behaviour;

namespace QHStudio.Game
{
    public class TimeDelay : MonoBehaviour
    {
        public RangeFloat delayStart;
        float time = 0;
        public BaseEvent.EmptyEvent timeEvent;

        public void destroy(Transform temp)
        {
            destroy(temp.gameObject);
        }

        public void destroy(GameObject temp)
        {
            Destroy(temp.gameObject);
        }
        public void destroySelf()
        {
            destroy(gameObject);
        }

        public void removeSelf()
        {
            Destroy(this);
        }

        public void remove(string value)
        {
            string[] vs = value.Split(';');
            for (int a = 0; a < vs.Length; a++)
            {
                Component temp = gameObject.GetComponent(vs[a]);
                if (!temp) continue;

                if (temp is Transform)
                {
                    DestroyObject(temp.gameObject);
                }
                else
                {
                    DestroyObject(temp);
                }
            }
        }

        public void active(string value)
        {
            string[] vs = value.Split(';');
            for (int a = 0; a < vs.Length; a++)
            {
                Component temp = gameObject.GetComponent(vs[a]);
                if (temp is Hvs)
                {
                    ((Hvs)temp).enabled = true;
                }
                else if (temp is Renderer)
                {
                    ((Renderer)temp).enabled = true;
                }
                else if (temp is Collider)
                {
                    ((Collider)temp).enabled = true;
                }
                else if (temp is Rigidbody)
                {
                    ((Rigidbody)temp).isKinematic = false;
                }
                else if (temp is Transform)
                {
                    ((Transform)temp).gameObject.SetActive(true);
                }
            }
        }

        public void disable(string value)
        {
            string[] vs = value.Split(';');
            for (int a = 0; a < vs.Length; a++)
            {
                Component temp = gameObject.GetComponent(vs[a]);

                if (temp is Hvs)
                {
                    ((Hvs)temp).enabled = false;
                }
                else if (temp is Renderer)
                {
                    ((Renderer)temp).enabled = false;
                }
                else if (temp is Collider)
                {
                    ((Collider)temp).enabled = false;
                }
                else if (temp is Rigidbody)
                {
                    ((Rigidbody)temp).isKinematic = true;
                }
                else if (temp is Transform)
                {
                    ((Transform)temp).gameObject.SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            float a = delayStart.getValue();
            if (a >= 0)
            {
                time = a;
            }
            Invoke("action", time);
        }
        public void setDelayTime(float value)
        {
            this.time = value;
        }

        public void stop()
        {
            if (IsInvoking("action"))
            {
                CancelInvoke("action");
            }
        }

        public void reStart()
        {
            stop();
            Invoke("action", time);
        }

        private void OnDisable()
        {
            CancelInvoke("action");
        }

        void action()
        {
            timeEvent.Invoke();
        }

    }
}