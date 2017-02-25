using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretDoor : MonoBehaviour {

    bool timer;
    float time;

	// Use this for initialization
	void Start () {
        timer = false;
        time = Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer)
        {
            time += 1 * Time.deltaTime;
        }
        if (time >= 10 * Time.deltaTime)
        {
            timer = false;
            time = 0;
            this.gameObject.SetActive(true);
        }
    }

    void openMe(int useless)
    {
        timer = true;
        this.gameObject.SetActive(false);
    }
}
