using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace QHStudio.Game
{
    [System.Serializable]
    public class GameObjectInfoBase
    {
        public LayerMask layer;
        public ReadInfoType readType = ReadInfoType.Arbitrarily;
        public string read;
        public bool imprecise = false;  //模糊匹配
        char code = ';';

        public GameObject getGameObject()
        {
            List<GameObject> temp = getGameObjects();
            if (temp.Count > 0)
            {
                int a = Random.Range(0, temp.Count);
                return temp[a];
            }
            return null;
        }

        public List<GameObject> getGameObjects()
        {
            List<GameObject> temp = new List<GameObject>();
            string[] rs = read.Split(code);
            switch (readType)
            {
                case ReadInfoType.Arbitrarily:
                    for (int i = 0; i < rs.Length; i++)
                    {
                        temp.AddRange(FindGameObject.findGameNameLayer(rs[i], layer, imprecise));
                        temp.AddRange(FindGameObject.findGameTagLayer(rs[i], layer, imprecise));
                    }
                    break;
                case ReadInfoType.Name:
                  //  temp = FindGameObject.findGameNameLayer(read, layer, imprecise);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        temp.AddRange(FindGameObject.findGameNameLayer(rs[i], layer, imprecise));
                    }

                    break;
                case ReadInfoType.Tag:
                 //   temp = FindGameObject.findGameTagLayer(read, layer, imprecise);
                    for (int i = 0; i < rs.Length; i++)
                    {
                        temp.AddRange(FindGameObject.findGameTagLayer(rs[i], layer, imprecise));
                    }
                    break;
                case ReadInfoType.Both:

                    break;
            }
            return temp;
        }


        public bool compareInfo(GameObject obj)
        {
            if (!obj) return false;
            if (!GameObjectLayer.isInLayer(obj.layer, layer)) return false;

            if (read.CompareTo("*") == 0 || read.CompareTo("?") == 0) return true;

            string[] rs = read.Split(code);


            switch (readType)
            {
                case ReadInfoType.Arbitrarily:
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!imprecise && (obj.tag.CompareTo(rs[i]) == 0 || obj.name.CompareTo(rs[i]) == 0)) return true;
                        if (imprecise)
                        {
                            if (obj.tag.IndexOf(rs[i]) >= 0 || obj.name.IndexOf(rs[i]) >= 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case ReadInfoType.Name:
                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!imprecise && obj.name.CompareTo(rs[i]) == 0) return true;
                        if (imprecise)
                        {
                            if (obj.name.IndexOf(rs[i]) >= 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case ReadInfoType.Tag:

                    for (int i = 0; i < rs.Length; i++)
                    {
                        if (!imprecise && obj.tag.CompareTo(rs[i]) == 0) return true;
                        if (imprecise)
                        {
                            if (obj.tag.IndexOf(rs[i]) >= 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case ReadInfoType.Both:
                    for (int i = 0; i < rs.Length; i++)
                    {

                        if (!imprecise && (obj.tag.CompareTo(rs[i]) == 0 && obj.name.CompareTo(rs[i]) == 0)) return true;
                        if (imprecise)
                        {
                            if (obj.tag.IndexOf(rs[i]) >= 0 && obj.name.IndexOf(rs[i]) >= 0)
                            {
                                return true;
                            }
                        }
                    }
                    break;
            }
            return false;
        }
    }
}