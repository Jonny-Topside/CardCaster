using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        this.gameObject.SetActive(true);
        Time.timeScale = 0;
      //  Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            //Cursor.lockState = CursorLockMode.None;
        }
	}
}
