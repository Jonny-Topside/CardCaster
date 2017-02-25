using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour
{
    AudioSource gunsound;
    public GameObject mechanics;
    public eventManager eM;
    void Start()
    {
        eM = GameObject.FindObjectOfType<eventManager>();
         gunsound = GetComponent<AudioSource>();
    }



    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {


            if (mechanics.GetComponent<HandgunDamage>().currentGun == 1)
            {
                if (Input.GetButton("Fire1") && !Input.GetButton("Sprint") && mechanics.GetComponent<HandgunDamage>().ready == true)
                {
                    if (eM.sliderVal != 0)
                    {
                        gunsound.Play();
                    }
                    GetComponent<Animation>().Play("GunShotNew");
                    //mechanics.GetComponent<HandgunDamage>().ready = false;
                }
            }

            if (mechanics.GetComponent<HandgunDamage>().currentGun == 2)
            {
                if (Input.GetButton("Fire1") && !Input.GetButton("Sprint") && mechanics.GetComponent<HandgunDamage>().ready == true)
                {

                    if (eM.sliderVal != 0)
                    {
                        gunsound.Play();
                    }
                    GetComponent<Animation>().Play("GunShotRev");
                    //mechanics.GetComponent<HandgunDamage>().ready = false;
                }
            }

            if (mechanics.GetComponent<HandgunDamage>().currentGun == 3)
            {
                if (Input.GetButton("Fire1") && !Input.GetButton("Sprint") && mechanics.GetComponent<HandgunDamage>().ready == true)
                {

                    if (eM.sliderVal != 0)
                    {
                        gunsound.Play();
                    }
                    GetComponent<Animation>().Play("GunShotUzi");
                    //mechanics.GetComponent<HandgunDamage>().ready = false;
                }
            }
        }
    }
}
