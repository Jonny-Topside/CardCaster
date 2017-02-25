using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleScript : MonoBehaviour {

    public GameObject porter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Transform>().position = new Vector3(porter.transform.position.x, porter.transform.position.y, porter.transform.position.z);
        }
    }
}
