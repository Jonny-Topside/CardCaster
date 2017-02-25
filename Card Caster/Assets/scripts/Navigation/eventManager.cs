using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class eventManager : MonoBehaviour
{
    // Slider volSlider;
    bool paused;
    public Slider sfxSlider;
    CameraLook cam;
    pauseScript pause;
    [SerializeField]
    public float sliderVal;
    AudioListener playerListener;
    GameObject player;
    AudioSource[] volume;
    // AudioListener vol;
    private void Awake()
    {
        sliderVal = PlayerPrefs.GetFloat("value");
        PlayerPrefs.SetFloat("value", sliderVal);
        if (sfxSlider)
            sfxSlider.value = sliderVal;
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("value", sliderVal);
    }
    // Use this for initialization
    void Start()
    {
        paused = false;
        cam = GameObject.FindObjectOfType<CameraLook>();
        pause = GameObject.FindObjectOfType<pauseScript>();
        AudioListener.volume = sliderVal;


        if (pause)
        {
            pause.GetComponent<Canvas>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        onPause();
        changeAudio();

    }


    public void changeAudio()
    {
        //AudioListener.volume = 0;
        if (sfxSlider)
            sliderVal = sfxSlider.value;
        else sliderVal = PlayerPrefs.GetFloat("value");

        volume = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < volume.Length; ++i)
        {

            volume[i].volume = sliderVal;
        }
    }



    public void onPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (paused == true)
            {
               // Cursor.lockState = CursorLockMode.Locked;
                resume();
                //   Time.timeScale = 1;
                //    Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Time.timeScale = 0;
                if (pause)
                {
                    pause.GetComponent<Canvas>().enabled = true;
                    GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>().enabled = true;
                    GameObject.FindGameObjectWithTag("Resume").GetComponent<Button>().enabled = true;
                        Cursor.lockState = CursorLockMode.Confined;
                    paused = true;
                }

            }


        }

    }

    public void resume()
    {
        if (pause)
        {
            GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>().enabled = false;
            GameObject.FindGameObjectWithTag("Resume").GetComponent<Button>().enabled = false;
            pause.GetComponent<Canvas>().enabled = false;
            pause.GetComponent<pauseScript>().enabled = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            paused = false;
        }



    }
    public void pauseQuit()
    {
        Time.timeScale = 1;
        //will go to menu
        SceneManager.LoadScene("mainMenu");
    }
}
