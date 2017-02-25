using UnityEngine;
using System.Collections;

public class faller : MonoBehaviour {
    

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.useGravity = true;
        }
    }
}
