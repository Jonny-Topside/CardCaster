using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavingStuff : MonoBehaviour {

    Transform playerPos, greenKeyPos, redKeyPos, blackKeyPos;
    //Canvas keyCanvas;

    public bool greenKeyGet, redKeyGet, blackKeyGet;
    public int totalKeysCollected;


    GameObject redKeyPic, greenKeyPic, blackKeyPic;
    GameObject redDoor, greenDoor, blackDoor, bossDoor;

    public int green; 
    public int red;  
    public int black; 

    // Use this for initialization
    void Start () {

        redKeyPic = GameObject.FindGameObjectWithTag("redKeyPic");
        greenKeyPic = GameObject.FindGameObjectWithTag("greenKeyPic");
        blackKeyPic = GameObject.FindGameObjectWithTag("blackKeyPic");
        redDoor = GameObject.FindGameObjectWithTag("redDoor");
        greenDoor = GameObject.FindGameObjectWithTag("greenDoor");
        blackDoor = GameObject.FindGameObjectWithTag("blackDoor");
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        bossDoor = GameObject.FindGameObjectWithTag("bossDoor");

        PlayerPrefs.GetInt("GreenKeyBool");
        PlayerPrefs.GetInt("RedKeyBool");
        PlayerPrefs.GetInt("BlackKeyBool");

        green = PlayerPrefs.GetInt("GreenKeyBool");
        red = PlayerPrefs.GetInt("RedKeyBool");
        black = PlayerPrefs.GetInt("BlackKeyBool");

        if (PlayerPrefs.GetInt("GreenKeyBool") == 1)
        {
            greenKeyPic.GetComponent<RawImage>().enabled = true;
          //  greenDoor.SetActive(false);
            green = 1;
        }
        if (PlayerPrefs.GetInt("RedKeyBool") == 1)
        {
            redKeyPic.GetComponent<RawImage>().enabled = true;
            red = 1;
        }
        if (PlayerPrefs.GetInt("BlackKeyBool") == 1)
        {
            blackKeyPic.GetComponent<RawImage>().enabled = true;
            black = 1;
        }



    }
	
	// Update is called once per frame
	void Update () {

        green = PlayerPrefs.GetInt("GreenKeyBool");
        red = PlayerPrefs.GetInt("RedKeyBool");
        black = PlayerPrefs.GetInt("BlackKeyBool");
        
        if (green == 1)
        {
            greenKeyPic.GetComponent<RawImage>().enabled = true;
         //   greenDoor.SetActive(false);
        }
        else
        {
            greenKeyPic.GetComponent<RawImage>().enabled = false;
        //    greenDoor.SetActive(true);
        }
        if (red == 1)
        {
            redKeyPic.GetComponent<RawImage>().enabled = true;
        }
        else
        {
            redKeyPic.GetComponent<RawImage>().enabled = false;
        }
        if (black == 1)
        {
            blackKeyPic.GetComponent<RawImage>().enabled = true;
        } else
        {
            blackKeyPic.GetComponent<RawImage>().enabled = false;
        }

        if(Input.GetButtonDown("k"))
        {
            PlayerPrefs.SetInt("GreenKeyBool", 0);
            PlayerPrefs.SetInt("RedKeyBool", 0);
            PlayerPrefs.SetInt("BlackKeyBool", 0);
        }
        if (Input.GetButtonDown("l"))
        {
            PlayerPrefs.SetInt("GreenKeyBool", 1);
            PlayerPrefs.SetInt("RedKeyBool", 1);
            PlayerPrefs.SetInt("BlackKeyBool", 1);
        }

    }
}
