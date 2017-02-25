using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class splashScript : MonoBehaviour {

    float splashTimer;
    public RawImage splash;
	// Use this for initialization
	void Start () {
        splashTimer = 0;
        splash.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        splashTimer += 1 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) || splashTimer >= (200 * Time.deltaTime))
        {
            SceneManager.LoadScene("mainMenu");
        }

}

}
