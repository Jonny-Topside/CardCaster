using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class continueGame : MonoBehaviour {

	public void continueScene()
    {
        SceneManager.LoadScene("Sprint3");
        Time.timeScale = 1;
    }
}
