using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class keyScript : MonoBehaviour {
    Transform playerPos, greenKeyPos, redKeyPos, blackKeyPos;
    //Canvas keyCanvas;

    public bool greenKeyGet, redKeyGet, blackKeyGet;
    public int totalKeysCollected;
    

    GameObject redKeyPic, greenKeyPic, blackKeyPic;
    GameObject redDoor, greenDoor, blackDoor, bossDoor;
    // Use this for initialization
    void Start ()
    {
        redKeyPic = GameObject.FindGameObjectWithTag("redKeyPic");
        greenKeyPic = GameObject.FindGameObjectWithTag("greenKeyPic");
        blackKeyPic = GameObject.FindGameObjectWithTag("blackKeyPic");
        redDoor = GameObject.FindGameObjectWithTag("redDoor");
        greenDoor = GameObject.FindGameObjectWithTag("greenDoor");
        blackDoor = GameObject.FindGameObjectWithTag("blackDoor");
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        bossDoor = GameObject.FindGameObjectWithTag("bossDoor");
        totalKeysCollected = 0;
        greenKeyGet = false;
        redKeyGet = false;
        blackKeyGet = false;
        // redDoorTest = GameObject.FindGameObjectWithTag("redDoor");
       // FindObjectsOfType<Text>().Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            totalKeysCollected++;

            if (gameObject.tag == "greenKey")
            {
                gameObject.SetActive(false);
                greenKeyGet = true;
                greenKeyPic.GetComponent<RawImage>().enabled = true;
               // greenDoor.SetActive(false);
                PlayerPrefs.SetInt("GreenKeyBool", 1);
            }

            else if (gameObject.tag == "redKey")
            {
                gameObject.SetActive(false);
                redKeyGet = true;
                redKeyPic.GetComponent<RawImage>().enabled = true;
                //redDoor.SetActive(false);
                PlayerPrefs.SetInt("RedKeyBool", 1);

                //   redKeyPic.SetActive(true);
            }
            else //tag == "black"
            {
                gameObject.SetActive(false);
                blackKeyGet = true;
                blackKeyPic.GetComponent<RawImage>().enabled = true;
                //blackDoor.SetActive(false);
                PlayerPrefs.SetInt("BlackKeyBool", 1);

            }


        }
    }

    public void respawn()
    {
        if((FindObjectOfType<PlayerHealth>().currHP == 0 && greenKeyGet == true))
        {
            playerPos.transform.position = greenKeyPos.transform.position;
        }

        else if ((FindObjectOfType<PlayerHealth>().currHP == 0 && redKeyGet == true))
        {
            playerPos.transform.position = redKeyPos.transform.position;
        }

        else
            playerPos.transform.position = blackKeyPos.transform.position;
        


    }


  
}
