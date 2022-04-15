using UnityEngine;
namespace QHStudio.Game
{
    [System.Serializable]
    public class RangeFloat
    {

        public float min;
        public float max;

        public float getValue()
        {
            if (min < 0 || max < 0) return -1;
            if (min > max) return -1;
            return Random.Range(min, max);
        }
    }
}