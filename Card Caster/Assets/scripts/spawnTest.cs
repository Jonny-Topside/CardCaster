using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTest : MonoBehaviour {

    [SerializeField]
    public Transform spawn1, spawn2, spawn3;
    [SerializeField]
    public GameObject enemy1, enemy2, enemy3;
    bool once;
	// Use this for initialization
	void Start () {
        once = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!once)
        {
            once = true;
            Instantiate(enemy1, spawn1.transform.position, transform.rotation);
            Instantiate(enemy2, spawn2.transform.position, transform.rotation);
            Instantiate(enemy3, spawn3.transform.position, transform.rotation);
        }
    }
}
