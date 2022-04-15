using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace QHStudio.Game
{
    public class BaseEvent
    {
        [System.Serializable]
        public class Vector3Event : UnityEvent<Vector3> { }
        [System.Serializable]
        public class Vector2Event : UnityEvent<Vector2> { }
        [System.Serializable]
        public class FloatEvent : UnityEvent<float> { }
        [System.Serializable]
        public class IntEvent : UnityEvent<int> { }
        [System.Serializable]
        public class StingEvent : UnityEvent<string> { }
        [System.Serializable]
        public class BoolEvent : UnityEvent<bool> { }
        [System.Serializable]
        public class GameObjEvent : UnityEvent<GameObject> { }
        [System.Serializable]
        public class TransformEvent : UnityEvent<Transform> { }
        [System.Serializable]
        public class EmptyEvent : UnityEvent { }
        [System.Serializable]
        public class RayEvent : UnityEvent<RaycastHit> { };
    }
}