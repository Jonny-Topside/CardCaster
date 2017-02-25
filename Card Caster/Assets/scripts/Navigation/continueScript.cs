using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class continueScript : MonoBehaviour {

    public void LoadScene()
    {
        SceneManager.LoadScene("Sprint3");
        Time.timeScale = 1;
    }
}
