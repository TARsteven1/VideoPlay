using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QHStudio.Game
{
    public class FindChildSelect : MonoBehaviour
    {
        public Transform parent;
        List<CacheSelect> cache = new List<CacheSelect>();
        public BaseEvent.TransformEvent selectEvent;
        int current;

        List<string> arrs = new List<string>();

        private void Awake()
        {
            if (!parent) parent = transform;
        }
        public void findIndex(int index)
        {
            if (!parent) return;
            Transform temp = parent.GetChild(index);
            if (temp) find(temp.name);
        }


        public void find(string value)
        {
            if (!parent) return;
            if (cache.Count > 200)
            {
                cache.RemoveAt(0);
            }

            CacheSelect cacheValue = new CacheSelect(value);
            cacheValue.id = cache.Count;
            cache.Add(cacheValue);

            current = cache.Count;

            arrs.Clear();
            arrs.AddRange(value.Split(';'));
            foreach (Transform child in parent)
            {
                setChild(child);
            }
        }
        public void previous()
        {
            if (current > 0)
            {
                select(--current);
            }
        }

        public void next()
        {
            if (current < cache.Count - 1)
            {
                select(++current);
            }
        }
        public void closeAll()
        {
            foreach (Transform child in parent)
            {
                child.gameObject.SetActive(false);
            }
        }

        void select(int index)
        {
            if (index < 0 || index > cache.Count - 1) return;
            string value = cache[index].value;
            arrs.Clear();
            arrs.AddRange(value.Split(';'));
            foreach (Transform child in parent)
            {
                setChild(child);
            }
        }

        void setChild(Transform child)
        {
            if (!child) return;
            bool v = arrs.Exists(a =>
             {
                 return a.CompareTo("?") == 0 ||
                           a.CompareTo("*") == 0 ||
                               a.CompareTo(child.name) == 0 ||
                                 checkStart(a, child.name) ||
                                     checkEnd(a, child.name);
             });
            child.gameObject.SetActive(v);
            if (v && child) selectEvent.Invoke(child);
        }

        bool checkStart(string v, string name)
        {
            string tp = v.Replace("?", "");
            tp = tp.Replace("*", "");
            return (v.StartsWith("?") || v.StartsWith("*")) && name.EndsWith(tp);
        }
        bool checkEnd(string v, string name)
        {
            string tp = v.Replace("?", "");
            tp = tp.Replace("*", "");
            return (v.EndsWith("?") || v.EndsWith("*")) && name.StartsWith(tp);
        }

        [System.Serializable]
        public class CacheSelect
        {
            public string value = "";
            public int id = 0;
            public CacheSelect(string value)
            {
                this.value = value;
            }
        }
        //void childCompare(Transform child, string value)
        //{

        //    if (value.CompareTo("?") == 0 || value.CompareTo("*") == 0)
        //    {
        //        child.gameObject.SetActive(true);
        //        return;
        //    }
        //    if (value.StartsWith("?") || value.StartsWith("*"))
        //    {
        //        if (child.name.EndsWith(value))
        //        {
        //            child.gameObject.SetActive(true);
        //            return;
        //        }
        //    }
        //    if (value.EndsWith("?") || value.EndsWith("*"))
        //    {
        //        if (child.name.StartsWith(value))
        //        {
        //            child.gameObject.SetActive(true);
        //            return;
        //        }
        //    }
        //    if (child.name.CompareTo(value) == 0)
        //    {
        //        child.gameObject.SetActive(true);
        //        return;
        //    }
        //    child.gameObject.SetActive(false);
        //}
    }
}