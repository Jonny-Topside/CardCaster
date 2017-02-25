using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class esc2mainMenu : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("escape") || Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("mainMenu");
        }
    
}
}
