using bosqmode.libvlc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace bosqmode.libvlc
{
    public class ConfigURL : MonoBehaviour
    {
        public string key = "temp";
        public string filePath = "";
        string value = "";
        string fileValue = "";
        string[] list;
        string path = "";
        public ValueEvent valueVent;
        [System.Serializable]
        public class ValueEvent : UnityEvent<string> { }
        void OnEnable()
        {
            fileValue = ReadTxtThird(filePath);
            fileValue = fileValue.Replace("\r", "");
            fileValue = fileValue.Replace("\n", "");
            fileValue = fileValue.Replace("\t", "");
            list = fileValue.Split(';');

            foreach (string temp in list)
            {
                if (temp.Length < 1) continue;
                path = temp.Trim();
                if (path.StartsWith(key + "="))
                {
                    value = temp.Replace(key + "=", "");
                }
            }

            if (value.Length > 0)
            {
                //player.Path = value;
                //player.Play();
                valueVent.Invoke(value);
            }
        }

        string ReadTxtThird(string p)
        {

            string path = Application.streamingAssetsPath + "/" + p;

            string str = File.ReadAllText(path, Encoding.UTF8);
            Debug.Log(str);
            return str;
        }
    }
}