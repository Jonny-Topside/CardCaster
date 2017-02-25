using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headshot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void deductPoints(int damageAmount)
    {
        this.transform.parent.SendMessage("deductPoints", damageAmount);
    }
}
