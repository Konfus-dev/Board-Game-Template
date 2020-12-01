using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    #region globals
    public AudioClip[] playlist;
    private AudioSource speakers;
    private int playlistPos = 0;
    #endregion

    private void Start()
    {
        speakers = this.GetComponent<AudioSource>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            speakers.Stop();
            if (playlistPos < playlist.Length) playlistPos++;
            else playlistPos = 0;
            speakers.clip = playlist[playlistPos];
            speakers.Play();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            speakers.Stop();
            if (playlistPos > 0) playlistPos--;
            else playlistPos = playlist.Length - 1;
            speakers.clip = playlist[playlistPos];
            speakers.Play();
        }
    }
}
