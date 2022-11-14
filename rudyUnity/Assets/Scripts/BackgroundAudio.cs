using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public GameObject text;
    public AudioClip musicClipWin;
    public AudioClip musicClipLose;
    public AudioSource musicSource;
    private bool one = true ;

    void Update()
    {
        TextController textcounter = text.gameObject.GetComponent<TextController>();
        if (textcounter.win && one == true )
        {
            winSong();
            one = false;
        }
        if (textcounter.loseBool && one == true)
        {
            loseSong();
            one = false;
        }
    }
    // Update is called once per frame
    public void winSong()
    {
        musicSource.clip = musicClipWin;
        musicSource.Play();
    }

    public void loseSong()
    {
        musicSource.clip = musicClipLose;
        musicSource.Play();
    }

}
