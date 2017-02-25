using UnityEngine;
using System.Collections;

public class doors2 : MonoBehaviour {

    Animator anim;
    bool doorOpen;

    void Start()
    {
        doorOpen = false;
        anim = GetComponent<Animator>();
        anim.SetBool("Open Door", false);
        anim.SetBool("Close Door", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorOpen = true;
            anim.SetBool("Open Door", true);
            anim.SetBool("Close Door", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            doorOpen = true;
            anim.SetBool("Open Door", false);
            anim.SetBool("Close Door", true);
        }
    }
}
