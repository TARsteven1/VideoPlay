using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Mirror;

public class MessageManager : NetworkBehaviour
{
    public Text CanvasStatusText;

    public PlayerC Pc;

    public VideoPlayer vp;

    public List<VideoClip> clips;

    public VideoClip Info;
    public VideoClip QA;

    [SyncVar(hook = nameof(OnStatusTextChanged))]
    public string statusText;

    public List<long> Frames;

    private  bool CD;
    public   float CDTime;

    private void Awake()
    {
        //vp = GetComponent<VideoPlayer>();
        //vp.loopPointReached += ContinuePlay;
       // vp = Pc.Cvp;
       // vp.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        //vp.gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(Pc.gameObject.GetComponent<NetworkIdentity>().connectionToClient); ;
    }
    private void Start()
    {
        ContinuePlay(vp);

    }

    private void OnStatusTextChanged(string Oldstr,string newStr)
    {
        CanvasStatusText.text = statusText;       
    }

    public void SendBtn(GameObject obj)
    {
        
        int tempInt =int.Parse( obj.name);
        if (tempInt<15)
        {

        if (Pc != null && vp.clip != clips[tempInt]/*&&!CD&& vp.isPrepared*/)
        {
                //Invoke("CDTimer", CDTime);
                //    CD = true;
            vp.Stop();
            vp.isLooping = false;
            Pc.CmdSendMessager(tempInt);
            vp.clip = clips[tempInt];
            vp.Play();
                CD = true;
        }
        }
        else
        {
            if (Pc != null)
            {

            Pc.CmdSendState(0);
            Pc.CmdSendFrameMode(0);
            Pc.CmdSendMessager(tempInt);
            vp.clip = clips[15];
            vp.isLooping = true;
            vp.Play();
            }
        }
    }

    //private void CDTimer()
    //{
    //    CD = false;
    //}

    void ContinuePlay(VideoPlayer videoPlayer)
    {
        //if (vp.isPrepared)
        //{

        CD = false;
        //Pc.CmdSendMessager(15);
        vp.isLooping = true;
       vp.clip = clips[15];
        vp.Play();
        //}
    }
    public void FrameModePlay()
    {
        Pc.CmdSendFrameMode(2);
        vp.Play();
    }   
    public void FrameModePause()
    {
        Pc.CmdSendFrameMode(1);
        vp.Pause();
        //Debug.Log(vp.frameCount);
    }
    private void Update()
    {
        
       
        if ((float)vp.frame >= vp.frameCount-1&&CD)
        {
            ContinuePlay(vp);
        }
        
    }
    public void NextFrame()
    {
        long CurrentFrame = vp.frame;
        Pc.CmdSendFrame(CurrentFrame);
        if (CurrentFrame!=0 && CurrentFrame < Frames[Frames.Count-1])
        {
           
        for (int i = 0; i < Frames.Count; i++)
        {
           // Debug.Log("g");
                if (CurrentFrame < Frames[i])
            {
             
                    //int tempFrame = i;
                vp.frame = Frames[i];
                    if (!vp.isPlaying)
                    {
                        vp.Play();
                    }


                    return;
                }
                else
                {
                    continue;
                }
               
        }
        }
        else
        {
            
            vp.frame = 0;
            if (!vp.isPlaying)
            {
                vp.Play();
            }
        }      
    }

    public void LastFrame()
    {

        long CurrentFrame = vp.frame;

        Pc.CmdSendFrame(-CurrentFrame);

        if ( CurrentFrame > Frames[1])
        {

            for (int i = 0; i < Frames.Count; i++)
            {
                if (CurrentFrame < Frames[i])
                {
                    vp.frame = Frames[i-2];
                    if (!vp.isPlaying)
                    {
                        vp.Play();
                    }

                    return;
                   
                }
                else if(CurrentFrame> Frames[Frames.Count-1])
                {
                    vp.frame = Frames[Frames.Count - 2];
                    if (!vp.isPlaying)
                    {
                        vp.Play();
                    }
                    break;
                }
             
            }
        }
        else
        {
            vp.frame = 0;
            if (!vp.isPlaying)
            {
                vp.Play();
            }
        }
    }
    public void ChangePlayVideoFrameMode()
    {
        CD = false;
        //vp.loopPointReached -= ContinuePlay;
        
            vp.Stop();
            vp.isLooping = true;
            vp.clip = Info;
        if (Pc != null)
        {

        Pc.CmdSendState(0);
            Pc.CmdSendMessager(-1);
            Pc.CmdSendFrameMode(-1);
        }
            vp.Play();
        
    }
   
    
    public void PlayStatePlay()
    {
 
            Pc.CmdSendState(2);
            vp.Play();
 
       
    }   
    public void PlayStatePause()
    {

        Pc.CmdSendState(1);
        vp.Pause();
    }
    public void ChangeVideoMode()
    {
        CD = false;
       // vp.loopPointReached -= ContinuePlay;
        vp.Stop();
        vp.isLooping = true;
        vp.clip = QA;
        if (Pc!=null)
        {

        Pc.CmdSendFrameMode(0);
        Pc.CmdSendMessager(-1);
        Pc.CmdSendState(-1);
        }
        vp.Play();

        
    
    }
    public void ChangeMutilVideoMode()
    {

        //vp.loopPointReached += ContinuePlay;
        if (Pc!=null)
        {

        Pc.CmdSendMessager(15);
        Pc.CmdSendState(0);
        Pc.CmdSendFrameMode(0);
        }
        vp.Stop();
        vp.isLooping = true;
        vp.clip = clips[15];
        vp.Play();
        

    }
    public void BackBtn()
    {
        if (Pc!=null)
        {
            Pc.CmdSendState(0);
            Pc.CmdSendFrameMode(0);
            Pc.CmdSendMessager(-1);
        }
    }
    //private void Update()
    //{
    //    if (vp.isPlaying)
    //    {
    //   // CanvasStatusText.text = vp.frame.ToString();

    //    }
    //}

}
