using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bossDoorOpeningScript : MonoBehaviour {
    GameObject bossDoor;
    GameObject redKeyPic, greenKeyPic, blackKeyPic;

    // Use this for initialization
    void Start () {
        redKeyPic = GameObject.FindGameObjectWithTag("redKeyPic");
        greenKeyPic = GameObject.FindGameObjectWithTag("greenKeyPic");
        blackKeyPic = GameObject.FindGameObjectWithTag("blackKeyPic");

        bossDoor = GameObject.FindGameObjectWithTag("bossDoor");
          }
	
	// Update is called once per frame
	void Update ()
    {
        if (redKeyPic.GetComponent<RawImage>().enabled == true && greenKeyPic.GetComponent<RawImage>().enabled == true && blackKeyPic.GetComponent<RawImage>().enabled == true)
            bossDoor.SetActive(false);

    }
    public void bossDoorOpening()
    {

       
    }
}
