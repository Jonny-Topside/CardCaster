using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffRicochetAudio : MonoBehaviour {
    public eventManager eve;
	// Use this for initialization
	void Start () {
        eve = GameObject.FindObjectOfType<eventManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (eve)
        {


            if (eve.sliderVal == 0)
                this.GetComponent<AudioSource>().mute = true;
            else
                this.GetComponent<AudioSource>().mute = false;
        }
        // GetComponent<AudioSource>().volume = eve.sliderVal;
    }
    public void audioAdjuster()
    {
        this.GetComponent<AudioSource>().volume = eve.sliderVal;
    }
}
