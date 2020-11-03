using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;

    // Start is called before the first frame update
    void Start()
    {
        if (mScreen != null && mVideoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator PrepareVideo()
    {
        mVideoPlayer.Prepare();

        while (!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        mScreen.texture = mVideoPlayer.texture;
    }

    public void PlayVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Stop();
        }
    }
}
