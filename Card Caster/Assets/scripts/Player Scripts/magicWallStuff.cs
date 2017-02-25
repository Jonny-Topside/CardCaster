using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicWallStuff : MonoBehaviour {

    private float t;

	// Use this for initialization
	void Start () {
        t = 11;
	}
	
	// Update is called once per frame
	void Update () {
        t = t - Time.deltaTime;

        if (t < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
