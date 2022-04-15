using UnityEngine;
using System.Collections.Generic;

namespace QHStudio.Game
{
    public class FindGameObject
    {
        public static bool findGameObject(string name)
        {
            return GameObject.Find(name);
        }

        public static bool findGameObjectTag(string tag)
        {
            return GameObject.FindGameObjectWithTag(tag);
        }

        public static GameObject findGameObjName(string name)
        {
            return GameObject.Find(name);
        }
        public static List<GameObject> findGameObjTag(string tag)
        {
            List<GameObject> find = new List<GameObject>();
            try
            {
                find.AddRange(GameObject.FindGameObjectsWithTag(tag));
            }
            catch (UnityException e)
            {
                Debug.Log(e.ToString());
                //  return null;
            }
            return find;
        }

        public static List<GameObject> findGameName(string name, bool Imprecise)
        {
            List<GameObject> find = new List<GameObject>();
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (!isWildcard(name))
                {
                    if (Imprecise)
                    {
                        if (!(temp[i].name.IndexOf(name) < 0)) find.Add(temp[i]);
                    }
                    else
                    {
                        if (temp[i].name.CompareTo(name) == 0) find.Add(temp[i]);
                    }
                }
                else
                {
                    find.Add(temp[i]);
                }
            }

            return find;
        }

        public static List<GameObject> findGameTag(string tag, bool Imprecise)
        {
            List<GameObject> find = new List<GameObject>();
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (!isWildcard(tag))
                {
                    if (Imprecise)
                    {
                        if (!(temp[i].tag.IndexOf(tag) < 0)) find.Add(temp[i]);
                    }
                    else
                    {
                        if (temp[i].tag.CompareTo(tag) == 0) find.Add(temp[i]);
                    }
                }
                else
                {
                    find.Add(temp[i]);
                }
            }
            return find;
        }
        public static List<GameObject> findGameLayer(LayerMask layer)
        {
            List<GameObject> find = new List<GameObject>();
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
            }
            return find;
        }

        public static List<GameObject> findGameNameLayer(string name, LayerMask layer, bool Imprecise)
        {
            List<GameObject> find = new List<GameObject>();
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (!isWildcard(name))
                {
                    if (Imprecise)
                    {
                        if (!(temp[i].name.IndexOf(name) < 0) && GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                    }
                    else
                    {
                        if (temp[i].name.CompareTo(name) == 0 && GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                    }
                }
                else
                {
                    if (GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                }
            }
            return find;
        }

        public static List<GameObject> findGameTagLayer(string tag, LayerMask layer, bool Imprecise)
        {
            List<GameObject> find = new List<GameObject>();
            GameObject[] temp = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (!isWildcard(tag))
                {
                    if (Imprecise)
                    {
                        if (!(temp[i].tag.IndexOf(tag) < 0) && GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                    }
                    else
                    {
                        if (temp[i].tag.CompareTo(tag) == 0 && GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                    }
                }
                else
                {
                    if (GameObjectLayer.isInLayer(temp[i], layer)) find.Add(temp[i]);
                }
            }
            return find;
        }

        public static GameObject findGamePath(string path)
        {
            GameObject temp = GameObject.Find(path);
            return temp;
        }


        public static bool isWildcard(string info)
        {
            if (info.CompareTo("?") == 0 || info.CompareTo("*") == 0) return true;
            return false;
        }
    }
}