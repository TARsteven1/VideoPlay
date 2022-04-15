using UnityEngine;
using System.Collections;
using System;

namespace QHStudio.Game
{
    public class GameObjectLayer
    {

        /// <summary>
        /// 判断是否在mask层内
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool isInLayer(int layer, LayerMask mask)
        {
            string j = Convert.ToString(mask.value, 2);

            char[] arr = j.ToCharArray();
            char[] brr = new char[32];

            for (int t = 0; t < brr.Length; t++)
            {
                if (t < arr.Length) brr[t] = arr[arr.Length - t - 1];
                else brr[t] = '0';
            }

            if (brr[layer].Equals('1'))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据对象判断
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool isInLayer(GameObject obj, LayerMask mask)
        {
            return isInLayer(obj.layer, mask);
        }
        /// <summary>
        /// 根据名称判断
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static bool isInLayer(string name, LayerMask mask)
        {
            return isInLayer(LayerMask.NameToLayer(name), mask);
        }
        /// <summary>
        /// 判断是否同一层
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool sameLayer(GameObject obj, GameObject obj2)
        {
            return obj.layer == obj2.layer;
        }

    }
}