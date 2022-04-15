using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class VideoControl : NetworkBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

        CmdLeader();
        }
    
        }
    [Command]
    private void CmdLeader()
    {
        Debug.Log(1);
        if (Input.GetKeyDown(KeyCode.A))
        {

        RpcPlay();
        }        if (Input.GetKeyDown(KeyCode.S))
        {

        RpcPause();
        }        if (Input.GetKeyDown(KeyCode.D))
        {

        RpcNext();
        }        if (Input.GetKeyDown(KeyCode.F))
        {

        RpcSpeedUp();
        }

    }
    [ClientRpc]
    private void RpcPlay()
    {
        //
        Debug.Log("play");
    }    [ClientRpc]
    private void RpcPause()
    {
        //
        Debug.Log("RpcPause");
    }    [ClientRpc]
    private void RpcNext()
    {
        //
        Debug.Log("RpcNext");
    }    [ClientRpc]
    private void RpcSpeedUp()
    {
        //
        Debug.Log("RpcSpeedUp");
    }
}
