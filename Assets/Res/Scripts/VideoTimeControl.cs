using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoTimeControl : MonoBehaviour
{
    public VideoPlayer vp;
    [Range(0, 1)] public float targetTime;
    private float targetTimeCache;
    [Range(0, 1)] public float seekTime;
    [Range(0, 1)] public float actualVideoTime;
    private float velocity;
    public bool seeking = false;
    private Coroutine currentCoroutine;
    private Coroutine lockTimeout;
    public bool timeout;

    public bool whileLoopRunning = false;

    // Have a boolean that blocks time changes until one is loaded
    public bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.Prepare();
        vp.skipOnDrop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(targetTime - targetTimeCache) > 0.01f)
        {
            seeking = true;
            targetTimeCache = targetTime;
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                locked = false;
            }
        }

        if (seeking)
        {
            // I use frames not time, sorry
            seekTime = Mathf.SmoothDamp(seekTime, targetTime, ref velocity, 1f);
            long seekFrame = (long)(seekTime * vp.frameCount);
            if (!locked)
            {
                currentCoroutine = StartCoroutine(SeekVideoAtFrame(seekFrame));
            }

            if (Mathf.Abs(targetTime - seekTime) < 0.01f)
            {
                seeking = false;
            }
        }
        else
        {
            if (!vp.isPlaying)
            {
                vp.Play();
            }
        }

        actualVideoTime = (float)(vp.time / vp.length);
    }

    private IEnumerator SeekVideoAtFrame(long aFrame)
    {
        // That coroutine will lock other calls until the frame/time is set for real.
        locked = true;
        vp.frame = aFrame;
        timeout = false;
        if (lockTimeout != null)
        {
            StopCoroutine(lockTimeout);
        }
        lockTimeout = StartCoroutine(LockTimeoutRoutine(.1f));
        yield return new WaitUntil(() =>
        {
            whileLoopRunning = true;
            if (vp.frame == aFrame || timeout)
            {
                return true;
            }
            else
            {
                return false;
            }
        });
        whileLoopRunning = false;
        locked = false;
    }

    private IEnumerator LockTimeoutRoutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        timeout = true;
    }
}