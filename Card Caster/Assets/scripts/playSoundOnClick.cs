using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playSoundOnClick : MonoBehaviour {
    public AudioSource buttonSounds;
	// Use this for initialization
	

    public void oClick()
    {
        if(buttonSounds)
        buttonSounds.Play();
    }
}
