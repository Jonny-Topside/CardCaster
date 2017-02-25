using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour {
    public eventManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<eventManager>();
	}
	
	// Update is called once per frame
	void Update () {
        manager.onPause();
	}
}
