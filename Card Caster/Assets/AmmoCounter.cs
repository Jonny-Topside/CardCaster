using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {
    
    public Text ammoText;
    public int ammoGet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ammoGet = GetComponent<CardStuff>().ammo;
        if(ammoGet > 0)
        {
            ammoText.text = "Ammo: " + ammoGet.ToString();
        }
        else
        {
            ammoText.text = "";
        }
		
	}
}
