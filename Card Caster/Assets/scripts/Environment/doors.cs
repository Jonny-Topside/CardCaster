using UnityEngine;
using System.Collections;

public class doors : MonoBehaviour {

    Animator anim;
    bool doorOpen;

    void Start()
    {
        doorOpen = false;
        anim = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            doorOpen = true;
            Doors("Open");
        }
    }

    void Doors(string _string)
    {
        anim.SetTrigger(_string);

    }
}
