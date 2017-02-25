using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayMusicOnChange : MonoBehaviour {
    public AudioSource music;
    
    // Use this for initialization
    void OnAwake()
    {

        music.Play();

    }
    // Update is called once per frame
    void FixedUpdate () {
		if(music.isPlaying)
        {
        }
        

	}
}
