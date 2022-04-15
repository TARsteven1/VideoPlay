using UnityEngine;
using System.Collections;

namespace QHStudio.Game
{
    public class CreatOrDestroy : MonoBehaviour
    {

        // Use this for initialization
        public enum TYPE { CREAT, DESTROY };
        public TYPE type = TYPE.CREAT;
        public GameObject mObj;
        public GameObject ObjPosition;
        public GameObject creatParent;
        public string creatName = "_auto";
        public bool enableAwak = true;
        void Awake()
        {
            if (enableAwak) enabled = true;
        }
        void OnEnable()
        {
            switch (type)
            {
                case TYPE.CREAT:
                    if (mObj)
                    {
                        GameObject temp = Instantiate(mObj, mObj.transform.position, mObj.transform.rotation) as GameObject;
                        temp.name = creatName;
                        temp.SetActive(true);
                        if (creatParent) temp.transform.parent = creatParent.transform;
                        temp.transform.localScale = mObj.transform.lossyScale;
                        if (ObjPosition)
                        {
                            temp.transform.position = ObjPosition.transform.position;
                            temp.transform.rotation = ObjPosition.transform.rotation;
                        }
                    }
                    enabled = false;
                    break;
                case TYPE.DESTROY:
                    if (mObj) Destroy(mObj);
                    else Destroy(gameObject);
                    break;
            }
        }

        public void toCreatOrDestroy()
        {
            switch (type)
            {
                case TYPE.CREAT:
                    if (mObj)
                    {
                        GameObject temp = Instantiate(mObj, mObj.transform.position, mObj.transform.rotation) as GameObject;
                        temp.name = creatName;
                       
                        if (creatParent) temp.transform.parent = creatParent.transform;
                        temp.transform.localScale = mObj.transform.lossyScale;

                        if (ObjPosition)
                        {
                            temp.transform.position = ObjPosition.transform.position;
                            temp.transform.rotation = ObjPosition.transform.rotation;
                        }

                        temp.SetActive(true);
                    }
                    enabled = false;
                    break;
                case TYPE.DESTROY:
                    if (mObj) Destroy(mObj);
                    else Destroy(gameObject);
                    break;
            }
        }

        /// <summary>
        /// 删除或创建
        /// </summary>
        /// <param name="creat"></param>
        public void isCreat(bool creat)
        {
            if (creat) type = TYPE.CREAT;
            else type = TYPE.DESTROY;
        }
        /// <summary>
        /// 对象
        /// </summary>
        /// <param name="obj"></param>
        public void setObj(Transform obj)
        {
            this.mObj = obj.gameObject;
        }
        /// <summary>
        /// 重置对象
        /// </summary>
        /// <param name="obj"></param>
        public void setObj(GameObject obj)
        {
            this.mObj = obj;
        }
        /// <summary>
        /// 设置父对象
        /// </summary>
        /// <param name="obj"></param>
        public void setObjParent(GameObject obj)
        {
            creatParent = obj;
        }
        /// <summary>
        /// 设置父对象
        /// </summary>
        /// <param name="obj"></param>
        public void setOjbParent(Transform obj)
        {
            creatParent = obj.gameObject;
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void toDestroyObj(Transform obj)
        {
            if (obj)
                Destroy(obj.gameObject);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void toDestroyObj(GameObject obj)
        {
            Destroy(obj);
        }
        /// <summary>
        /// 创建物体
        /// </summary>
        /// <param name="obj"></param>
        public void toCreatObj(Transform obj)
        {
            if (obj)
            {
                GameObject temp = Instantiate(obj.gameObject, obj.transform.position, obj.transform.rotation) as GameObject;
                if (creatName != null && creatName.Length > 1) temp.name = creatName;
                else temp.name = obj.name;
                temp.SetActive(true);
                if (creatParent) temp.transform.parent = creatParent.transform;
                temp.transform.localScale = obj.lossyScale;

                if (ObjPosition)
                {
                    temp.transform.position = ObjPosition.transform.position;
                    temp.transform.rotation = ObjPosition.transform.rotation;
                }

                // temp.transform.position = obj.position;

            }
        }
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="obj"></param>
        public void toCreatObj(GameObject obj)
        {
            if (obj)
            {
                toCreatObj(obj.transform);
            }
        }
        /// <summary>
        /// 创建为子对象
        /// </summary>
        /// <param name="obj"></param>
        public void toCreatChildSelf(GameObject obj)
        {
            if (obj)
            {
                GameObject temp = Instantiate(obj, obj.transform.position, obj.transform.rotation) as GameObject;
                temp.name = creatName;
                temp.SetActive(true);
                //if (creatParent) temp.transform.parent = creatParent.transform;
                //else
                temp.transform.parent = transform;
                temp.transform.localScale = mObj.transform.lossyScale;

                if (ObjPosition)
                {
                    temp.transform.position = ObjPosition.transform.position;
                    temp.transform.rotation = ObjPosition.transform.rotation;
                }
            }
        }

        public void creatToPosition(Transform position)
        {
            if (!mObj || !position) return;
            GameObject temp = Instantiate(mObj, position.transform.position, mObj.transform.rotation) as GameObject;
            temp.name = creatName;
            temp.SetActive(true);
            if (creatParent)
            {
                temp.transform.parent = creatParent.transform;
            }
        }

        /// <summary>
        /// 删除子对象
        /// </summary>
        /// <param name="info"></param>
        public void DestroySelfChild(string info)
        {
            destoryChild(transform, info);
        }
        /// <summary>
        /// 删除子对象
        /// </summary>
        /// <param name="info"></param>
        public void DestroyChild(string info)
        {
            destoryChild(creatParent ? creatParent.transform : transform, info);
        }

        void destoryChild(Transform p, string info)
        {
            foreach (Transform item in p)
            {
                if (item.name.Equals(info) || item.tag.Equals(info))
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}