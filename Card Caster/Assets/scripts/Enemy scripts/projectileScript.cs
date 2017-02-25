using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {

    float time;
	void Start () {
        time = Time.deltaTime;
    }
	
	void Update () {
        time += 1 * Time.deltaTime;

        if (time >= 1000 * Time.deltaTime)
        {
            time = Time.deltaTime;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag != "Player")
        //    Destroy(this.gameObject);
        if (other.tag == "Player")
        {
            other.SendMessage("takeDamage", 1);


            Destroy(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
