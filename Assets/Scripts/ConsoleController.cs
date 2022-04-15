using System.Collections.Generic;
using UnityEngine;

public class ConsoleController : MonoBehaviour
{

    public bool show = true;
    public int fontSize = 20;
    public int maxMessageCout =10;
    static ConsoleController instance;
    public static ConsoleController Instacne
    {
        get
        {
            if (!instance)
            {
                GameObject temp = new GameObject("____ConsoleController_____");
                instance = temp.AddComponent<ConsoleController>();
            }
            return instance;
        }
    }
    public int Max
    {
        get
        {
            return (maxMessageCout > 500 || maxMessageCout < 0) ? 500 : maxMessageCout;
        }
    }
    public static bool isNull
    {
        get
        {
            return instance == null;
        }
    }
    List<string> messages = new List<string>();
    string message = "";
    Rect view;
    Vector2 p;
    GUIStyle style = new GUIStyle();
    GUIStyle infoStyle = new GUIStyle();
    GUIStyle msgStyle = new GUIStyle();
    float time = 0;
    int w, h;
    Rect rect;
    private void Awake()
    {

        instance = this;
        view = new Rect(0, Screen.height * 0.7f, Screen.width * 0.3f, Screen.height * 0.3f);
        style.fontSize = 22;
        infoStyle.normal.textColor = Color.red;
        infoStyle.fontSize = fontSize;
        infoStyle.wordWrap = true;
        infoStyle.richText = true;

        w = Screen.width;
        h = Screen.height;
        rect = new Rect(0, 0, w, h * 2 / 80);
        msgStyle.alignment = TextAnchor.UpperLeft;
        msgStyle.fontSize = h * 2 / 80;
        msgStyle.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    }

    public void debug(string info)
    {
        if (!show) return;
        if (Time.time - time < 0.12f) return;
        time = Time.time;
        if (messages == null) messages = new List<string>();
        messages.Add("<color=#fdad02>Debug : </color>" + info+ " <color=#00ff00>(" + getTime()+ ")</color>");
        if (maxMessageCout > 0 && messages.Count > Max)
        {
            messages.RemoveAt(0);
        }
    }
    public void showMessage(string value)
    {
        message = value;
    }
    static string getTime()
    {
        System.DateTime nowTime = System.DateTime.Now;
        return nowTime.ToString();
    }

    public void clean()
    {
        messages.Clear();
    }
    public void showView(bool value)
    {
        this.show = value;
    }

    public void triggerView()
    {
        this.show = !this.show;
    }


    private void OnGUI()
    {
       
        if (!show) return;
        if (message.Length > 0) GUI.Label(rect, message, msgStyle);
        view = GUI.Window(0, view, mywindowfunction, "Debug Message");
        view.x = view.x < 0 ? 0 : view.x > Screen.width - view.width ? Screen.width - view.width : view.x;
        view.y = view.y < 0 ? 0 : view.y > Screen.height - view.height ? Screen.height - view.height : view.y;
    }

    void mywindowfunction(int windowid)
    {

        p = GUILayout.BeginScrollView(p, GUILayout.Width(view.width - 20), GUILayout.Height(view.height));

        GUILayout.BeginVertical();
        for (int a = 0; a < messages.Count; a++)
        {
            GUILayout.Label(messages[a], infoStyle);
        }
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        if (GUI.Button(new Rect(view.width * 0.6f, view.height * 0.85f, view.width * 0.4f, view.height * 0.15f), "Clean"))
        {
            clean();
        }
        GUI.DragWindow(new Rect(0, 0, Screen.width - view.width, Screen.height - view.height));
    }

}
