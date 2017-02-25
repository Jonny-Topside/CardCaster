using UnityEngine;
using System.Collections;

public class CrossAnimate : MonoBehaviour {

    public GameObject UpCurs;
    public GameObject DownCurs;
    public GameObject LeftCurs;
    public GameObject RightCurs;

    public GameObject mechanics;

    void Start ()
    {
        
	}

    void Update()
    {
        if (Time.timeScale != 0)
        {


            if (Input.GetButton("Fire1") && !Input.GetButton("Sprint") && mechanics.GetComponent<HandgunDamage>().ready)
            {

                UpCurs.GetComponent<Animation>().Stop();
                DownCurs.GetComponent<Animation>().Stop();
                LeftCurs.GetComponent<Animation>().Stop();
                RightCurs.GetComponent<Animation>().Stop();


                UpCurs.GetComponent<Animation>().Play("UpCursIdle");
                DownCurs.GetComponent<Animation>().Play("DownCursIdle");
                LeftCurs.GetComponent<Animation>().Play("LeftCursIdle");
                RightCurs.GetComponent<Animation>().Play("RightCursIdle");


                UpCurs.GetComponent<Animation>().Play("UpCursAnim");
                DownCurs.GetComponent<Animation>().Play("DownCursAnim");
                LeftCurs.GetComponent<Animation>().Play("LeftCursAnim");
                RightCurs.GetComponent<Animation>().Play("RightCursAnim");
                //StartCoroutine(waitingAnim());
                mechanics.GetComponent<HandgunDamage>().ready = false;
            }
            if (!mechanics.GetComponent<HandgunDamage>().ready)
            {
                //StartCoroutine(waitingAnim());

                //UpCurs.transform.localPosition = new Vector3(0, 25, 0);
                //DownCurs.transform.localPosition = new Vector3(0, -25, 0);
                //LeftCurs.transform.localPosition = new Vector3(-25, 0, 0);
                //RightCurs.transform.localPosition = new Vector3(25, 0, 0);

            }
        }
    }

    IEnumerator waitingAnim()
    {
       yield return new WaitForSeconds(0.06f);
        //UpCurs.GetComponent<Animator>().enabled = false;
        //DownCurs.GetComponent<Animator>().enabled = false;
        //LeftCurs.GetComponent<Animator>().enabled = false;
        //RightCurs.GetComponent<Animator>().enabled = false;

        //UpCurs.transform.localPosition = new Vector3(0, 25, 0);
        //DownCurs.transform.localPosition = new Vector3(0, -25, 0);
        //LeftCurs.transform.localPosition = new Vector3(-25, 0, 0);
        //RightCurs.transform.localPosition = new Vector3(25, 0, 0);
    }
}
