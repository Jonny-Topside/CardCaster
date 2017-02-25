using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTimer : MonoBehaviour {

    public Text timerText;
    public float t;
    // Use this for initialization
    void Start () {
        t = 6;
	}

    // Update is called once per frame
    void Update()
    {
        t = t - Time.deltaTime;
        int textInt = (int)t;

        string seconds = textInt.ToString();

        timerText.text = "damage bonus: " + seconds;

        if (t <= 1)
        {
            t = 6;
            timerText.text = "";
            this.enabled = false;
        }

    }
}
