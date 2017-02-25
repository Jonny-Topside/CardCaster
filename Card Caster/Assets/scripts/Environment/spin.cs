using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Transform>().Rotate(new Vector3(0, Time.deltaTime * 50, 0), Space.World);

    }
}
