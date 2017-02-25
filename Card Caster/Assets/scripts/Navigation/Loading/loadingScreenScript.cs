using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingScreenScript : MonoBehaviour {

   public RawImage crossFader;
   // Use this for initialization
	void Start () {
       crossFader.enabled = false;
     
       // fadeIn();
       // wait();
       // fadeOut();
	}
	
	// Update is called once per frame
	void Update () {
    }
   public IEnumerator wait()
    {
        yield return new WaitForSeconds(2);

    }
   public void fadeIn()
    {
        crossFader.CrossFadeAlpha(0, 0.25f, true);

    }

    public void fadeOut()
    {
        crossFader.CrossFadeAlpha(1, 0.25f, true);
        
    }
    public void startLoading()
    {
        GameObject.FindGameObjectWithTag("mainPanel").SetActive(false);
       crossFader.enabled = true;
        wait();
   }
}
