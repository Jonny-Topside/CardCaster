using UnityEngine;
using System.Collections;

public class fallerScript : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Rigidbody body = GetComponent<Rigidbody>();
            body.useGravity = true;
        }
    }
}
