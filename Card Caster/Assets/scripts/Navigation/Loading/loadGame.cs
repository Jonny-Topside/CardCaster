using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class loadGame : MonoBehaviour {

	public void LoadScene()
    {
        PlayerPrefs.SetInt("GreenKeyBool", 0);
        PlayerPrefs.SetInt("RedKeyBool", 0);
        PlayerPrefs.SetInt("BlackKeyBool", 0);
        SceneManager.LoadScene("Sprint3");
        Time.timeScale = 1;
    }
}
