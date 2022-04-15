using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

public class TimeStep : MonoBehaviour
{
  
    // NOTE: the Marker # is not necessarily sequential... Marker # may have to do with the order in which they are created
    public PlayableDirector playableDirector;
    // The # of the Marker you want to go to
    public int markerNum;

    void Start()
    {
        // THIS WOULD GRAB THE TIMELINE ON THIS OBJECT but I rather call a MASTER timeline!
        // playableDirector = GetComponent<PlayableDirector>();
    }

    public void setmarker(int value)
    {
        markerNum = value;
    }

  public void next()
    {

        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var markers = timelineAsset.markerTrack.GetMarkers().ToArray();
        playableDirector.time = markers[markerNum+1].time;
        playableDirector.Play();
       
    }

    public void prev()
    {

        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var markers = timelineAsset.markerTrack.GetMarkers().ToArray();
        playableDirector.time = markers[markerNum - 1].time;
        playableDirector.Play();

    }

}
