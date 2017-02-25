using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class creditsMoving : MonoBehaviour {
    Vector3 movement;
     void Start()
    {
         movement = new Vector3(0, -0.2f, 0);
    }
    
      //BROKEN THINGS:
      //GUN FIRE SOUNDS
     
      
    private void FixedUpdate()
    {
        Text[] creditText = FindObjectsOfType<Text>();
        for (int i = 0; i < creditText.Length; ++i)
        {
            creditText[i].transform.Translate(movement);// * Time.deltaTime);
        }
    }
    private void credits()
    {
     
      //  GameObject.FindGameObjectWithTag("created").transform.Translate;
      //  GameObject.FindGameObjectWithTag("sound").transform.Translate(0, -0.25f, 0);
      //  GameObject.FindGameObjectWithTag("art").transform.Translate(0, -0.25f, 0);
      //  GameObject.FindGameObjectWithTag("addHelp").transform.Translate(0, -0.25f, 0);
      //  GameObject.FindGameObjectWithTag("title").transform.Translate(0, -0.25f, 0);

    }
}
