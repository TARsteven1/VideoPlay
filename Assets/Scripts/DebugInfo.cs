using UnityEngine;
using System.Collections;

namespace QHStudio.Game
{
    public class DebugInfo : MonoBehaviour
    {
        public bool debug = true;

        public void log(string info)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(info);
            if (!debug) return;
            Debug.Log(info);
          
        }
        public void log(int v)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(v.ToString());
            if (!debug) return;
            Debug.Log(v);
           
        }
        public void log(float v)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(v.ToString());
            if (!debug) return;
            Debug.Log(v);
          
        }
        public void log(Vector3 v)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(v.ToString());
            if (!debug) return;
            Debug.Log(v);
           
        }
        public void log(Vector2 v)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(v.ToString());
            if (!debug) return;
            Debug.Log(v);
           
        }
        public void log(bool v)
        {
            if (!ConsoleController.isNull) ConsoleController.Instacne.debug(v.ToString());
            if (!debug) return;
            Debug.Log(v);
         
        }

        public void log(Texture texure)
        {
            string v = texure ? texure.name : " texture is null";
            log(v);
        }

        public void log(Object obj)
        {
            string v = obj ? obj.name + ":" + obj.ToString() : " Object is null";
            log(v);
        }

    }
}