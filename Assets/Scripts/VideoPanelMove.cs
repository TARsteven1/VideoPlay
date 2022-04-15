using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPanelMove : MonoBehaviour
{
    public RectTransform VideoPanel;
    public RectTransform  BackBtn;

    private float vpx;
    private float vpy;
    private float bbx;
    private float bby;

    public float Vpx { get => vpx; set => vpx = value; }
    public float Vpy { get => vpy; set => vpy = value; }
    public float Bbx { get => bbx; set => bbx = value; }
    public float Bby { get => bby; set => bby = value; }

    // Start is called before the first frame update
    //void Start()
    //{
    //    //VideoPanel.offsetMax = new Vector2(0, 500);
    //   // BackBtn.anchoredPosition = new Vector2(0, 1000);
    //}

    // Update is called once per frame
    void Update()
    {
        MoveUp();
    }
    void MoveUp()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            VideoPanel.anchoredPosition += new Vector2(0, 100);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            VideoPanel.anchoredPosition -= new Vector2(0, 100);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            VideoPanel.anchoredPosition -= new Vector2(100, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            VideoPanel.anchoredPosition += new Vector2(100, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            BackBtn.anchoredPosition += new Vector2(0, 100);
        }       
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BackBtn.anchoredPosition -= new Vector2(0, 100);
        }        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackBtn.anchoredPosition -= new Vector2(100, 0);
        }        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BackBtn.anchoredPosition += new Vector2(100, 0);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            VideoPanel.offsetMax = new Vector2(0, 500);
            VideoPanel.offsetMin = new Vector2(0, 0);
        }        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            BackBtn.anchoredPosition = new Vector2(0, 1000);
        }
    }
    void LoadRectTransformValue()
    {
        if (PlayerPrefs.HasKey("Vpx"))
            Vpx = PlayerPrefs.GetFloat("Vpx", Vpx);        
        if (PlayerPrefs.HasKey("Vpy"))
            Vpy = PlayerPrefs.GetFloat("Vpy", Vpy);        
        if (PlayerPrefs.HasKey("Bbx"))
            Bbx = PlayerPrefs.GetFloat("Bbx", Bbx);        
        if (PlayerPrefs.HasKey("Bby"))
            Bby = PlayerPrefs.GetFloat("Bby", Bby);

        VideoPanel.offsetMax = new Vector2(0, Vpy);
        VideoPanel.offsetMin = new Vector2(Vpx, 0);
        BackBtn.anchoredPosition= new Vector2(Bbx, Bby);
        Debug.Log("L:"+Vpx + "H" + Vpy + "H" + Bbx + "H" + Bby);
    }
    public void SaveRectTransformValue()
    {
        Vpx = VideoPanel.offsetMin.x;
        Vpy = VideoPanel.offsetMax.y;
        Bbx = BackBtn.anchoredPosition.x;
        Bby = BackBtn.anchoredPosition.y;

        PlayerPrefs.SetFloat("Vpx", Vpx);
        PlayerPrefs.SetFloat("Vpy", Vpy);
        PlayerPrefs.SetFloat("Bbx", Bbx);
        PlayerPrefs.SetFloat("Bby", Bby);
        Debug.Log("s:"+Vpx+"H" +Vpy+ "H" + Bbx + "H" + Bby);
    }
    private void OnEnable()
    {
        LoadRectTransformValue();
    }
    private void OnDisable()
    {
        SaveRectTransformValue();
    }
}
