using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExceptionHandleWin : EditorWindow
{
   [MenuItem("TARTools/ExceptionHandle")]
   public static void CreateEH()
    {
        if (!GameObject.Find("_ExceptionHandle"))
        {

        var TARTools_ExceptionHandle =Instantiate(Resources.Load("ExceptionHandle"));
        TARTools_ExceptionHandle.name = "_ExceptionHandle";
        }
        //ExceptionHandleWin winEH = (ExceptionHandleWin)GetWindow(typeof(ExceptionHandleWin));
    }
}
