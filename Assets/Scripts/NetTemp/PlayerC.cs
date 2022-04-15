using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Mirror;

public class PlayerC : NetworkBehaviour
{
    //public GameObject FloatingInfo;
    //public TextMesh Text3D;
    MessageManager messageManager;

    private Material PlayerMaterrial;
   // [SyncVar(hook = nameof(OnPlayVideo))]
    public VideoPlayer Cvp;

    public List<VideoClip> clips;

    public VideoClip Info;
    public VideoClip QA;

    public List<long> Frames;

    //[SyncVar(hook =nameof(OnPlayerNameChanged))]
    //public string NameString;
    //[SyncVar(hook =nameof(OnPlayerNameColorChanged))]
    //public Color NameColor;
    [SyncVar(hook = nameof(OnPlayerNumChanged))]
    public int PlayclipNum;

    [SyncVar(hook = nameof(OnVideoFrameChanged))]
    public long videoFrame;   

    [SyncVar(hook = nameof(OnVideoFrameModeChanged))]
    public int videoFrameModeBasic;

    [SyncVar(hook = nameof(OnVideoStateChanged))]
    public int isPlayVideo;

    //public  int ChlipNum;
    //public  int CompareNum=1;
    private bool canPlayclips;

    //public GameObject canvas;
    //public GameObject vpPrefab;
    private void Awake()
    {
        messageManager = FindObjectOfType<MessageManager>();
        // canvas = GameObject.Find("Canvas");
        //CmdInitVideoPlayer();
        //Cvp = GetComponent<VideoPlayer>();
        Cvp = GameObject.Find("Skybox").GetComponent<VideoPlayer>();

        //Cvp.loopPointReached += ContinuePlayC;

    }
    private void Update()
    {
        
        if ((float)Cvp.frame >= Cvp.frameCount-1&&canPlayclips)
        {
            ContinuePlayC(Cvp);
        }

    }
    private void OnPlayerNumChanged(int Oldint, int newint)
    {
 
        //Cvp.loopPointReached += ContinuePlayC;
        // Debug.Log(PlayclipNum + "GGG" + newint + "hhhh" + Oldint);
        if (newint<15)
        {
            if (newint<0)
            {
                //Cvp.Pause();
                
                return;
            }
        Cvp.Stop();
            canPlayclips = true;
            Cvp.isLooping = false;
        Cvp.clip = clips[newint];
        Cvp.Play();       

        }
        else
        {
            Cvp.Stop();
            
            ContinuePlayC(Cvp);
        }
    }
    private void OnVideoFrameChanged(long Oldlong, long newlong)
    {
       // Cvp.loopPointReached -= ContinuePlayC;
        if (newlong <= 0)
        {
            Cvp.frame = -newlong;
            long CurrentFrame = Cvp.frame;

            if (CurrentFrame > Frames[1])
            {

                for (int i = 0; i < Frames.Count; i++)
                {
                    if (CurrentFrame < Frames[i])
                    {
                        Cvp.frame = Frames[i - 2];
                        if (!Cvp.isPlaying)
                        {
                            Cvp.Play();
                        }
                        return;
                    }
                    else if (CurrentFrame > Frames[Frames.Count - 1])
                    {
                        Cvp.frame = Frames[Frames.Count - 2];
                        if (!Cvp.isPlaying)
                        {
                            Cvp.Play();
                        }
                        break;
                    }
                }
            }
            else
            {
                Cvp.frame = 0;
            }
        }
        else
        {
          //Cvp.Stop();
            //ContinuePlayC(Cvp);
            Cvp.frame = newlong;
        long CurrentFrame = Cvp.frame;

        if (CurrentFrame != 0 && CurrentFrame < Frames[Frames.Count - 1])
        {

            for (int i = 0; i < Frames.Count; i++)
            {
                if (CurrentFrame < Frames[i])
                {
                    Cvp.frame = Frames[i];
                        if (!Cvp.isPlaying)
                        {
                            Cvp.Play();
                        }
                        return;
                }

            }
        }
        else
        {
            Cvp.frame = 0;
        }
        }

    }
    private void OnVideoFrameModeChanged(int OldintChange, int newintChange)
    {
        // BackBtn();
        //Cvp.loopPointReached -= ContinuePlayC;
        canPlayclips = false;

        if (newintChange>=0)
        {
            if (newintChange==0)
            {
                return;
            }
            if (newintChange>1)
            {
                Cvp.Play();
            }
            else
            {
                Cvp.Pause();
            }
        }
        else
        {
            Cvp.Stop();
            Cvp.isLooping = true;
            Cvp.clip = Info;
            Cvp.Play();
        }
    }
        private void OnVideoStateChanged(int OldState, int newState)
    {
        //BackBtn();
        //Cvp.loopPointReached -= ContinuePlayC;
        canPlayclips = false;
        if (newState>0)
        {
            if (newState > 1)
            {

                Cvp.Play();
            }
            else
            {
                Cvp.Pause();
            }
        }
        else if (newState == 0)
        {
            return;
        }
        else
        {
            Cvp.Stop();
            Cvp.isLooping = true;
            Cvp.clip = QA;
            Cvp.Play();
        }

    }
        [Command]
    private void CmdSetPlayNum(int val)
    {
        PlayclipNum = val;
        //messageManager.statusText = $"{NameString} Joined.";
    }
    [Command]
    private void CmdSetPlaFrame(long valf)
    {
        videoFrame = valf;
        //messageManager.statusText = $"{NameString} Joined.";
    }
    [Command]
    private void CmdSetPlaFrameMode(int valfc)
    {
        videoFrameModeBasic = valfc;
        //messageManager.statusText = $"{NameString} Joined.";
    }
    [Command]
    private void CmdSetPlaState(int vals)
    {
        isPlayVideo = vals;
        //messageManager.statusText = $"{NameString} Joined.";
    }
    //[Command]
    //private void CmdSetupPlayer(string Name,Color color)
    //{
    //    NameString = Name;
    //    NameColor = color;
    //    //messageManager.statusText = $"{NameString} Joined.";
    //}
    [Command]
    public void CmdSendMessager(int Com)
    {
        if (messageManager)
        {
            RpcPlaySync(Com);
            messageManager.statusText = Com.ToString();       
            //messageManager.statusText = $"{NameString} say hello{Random.Range(1,99)}";
        }
    }
    [Command]
    public void CmdSendFrame(long Comframe)
    {
        if (messageManager)
        {
            RpcVideoFrameSync(Comframe);
            messageManager.statusText = Comframe.ToString();
            //messageManager.statusText = $"{NameString} say hello{Random.Range(1,99)}";
        }
    }
    [Command]
    public void CmdSendFrameMode(int ComframeMode)
    {
        if (messageManager)
        {
            RpcVideoFrameModeSync(ComframeMode);
            messageManager.statusText = ComframeMode.ToString();
            //messageManager.statusText = $"{NameString} say hello{Random.Range(1,99)}";
        }
    }
    [Command]
    public void CmdSendState(int ComState)
    {
        if (messageManager)
        {
            RpcVideoStateSync(ComState);
            messageManager.statusText = ComState.ToString();
            //messageManager.statusText = $"{NameString} say hello{Random.Range(1,99)}";
        }
    }
    [ClientRpc]
    public void RpcPlaySync(int num)
    {
        //PlayclipNum = num;
        CmdSetPlayNum(num);
    }
    [ClientRpc]
    public void RpcVideoFrameSync(long frame)
    {
        //PlayclipNum = num;
        CmdSetPlaFrame(frame);
    }
    [ClientRpc]
    public void RpcVideoFrameModeSync(int ComframeMode)
    {
        //PlayclipNum = num;
        CmdSetPlaFrameMode(ComframeMode);
    }
    [ClientRpc]
    public void RpcVideoStateSync(int state)
    {
        //PlayclipNum = num;
        CmdSetPlaState(state);
    }
    //[Command]
    //public void CmdInitVideoPlayer()
    //{
    //    RpcInitVideoplayer();
    //}
    //[ClientRpc]
    //public void RpcInitVideoplayer()
    //{
    //    //PlayclipNum = num;
    //    var videoplayer = Instantiate(vpPrefab/*, canvas.transform*/);
    //}


    public override void OnStartLocalPlayer()
    {
        messageManager.Pc = this;
        //base.OnStartLocalPlayer();
        //摄像机与主角绑定，实现第一视角
        //Camera.main.transform.SetParent(transform);
        //Camera.main.transform.localPosition = Vector3.zero;

        //        FloatingInfo.transform.localPosition = new Vector3(0, 3f, 6f);
        //        FloatingInfo.transform.localScale = new Vector3(1f, 1f, 1f);

        //        var tempName = $"Player {Random.Range(1, 999)}";
        //        var tempColor = new Color 
        //        (
        //            Random.Range(0f, 1f),
        //            Random.Range(0f, 1f),
        //            Random.Range(0f, 1f),
        //            1
        //);
        //        CmdSetupPlayer(tempName, tempColor);

    }
    void ContinuePlayC(VideoPlayer videoPlayer)
    {

        canPlayclips = false;
        Cvp.clip = clips[15];
        Cvp.isLooping = true;
        Cvp.Play();
    }


}
