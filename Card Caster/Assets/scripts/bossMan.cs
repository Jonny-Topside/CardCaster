using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class bossMan : MonoBehaviour {

    public int enemyHealth, fireRate, multiFireRate;
    public float bulletSpeed;
    [SerializeField]
    public Transform bigFirePoint, multi1, multi2, multi3, target1, target2, target3;
    [SerializeField]
    public GameObject bigProjectile, multiProjectile;

    int target;
    float time;
    Vector3 direction;
    Transform tPlayer;
    bool startFight, multi;
    GameObject shot;
    AudioSource pain;

    void Start () {
        time = Time.deltaTime;
        pain = GetComponent<AudioSource>();
        startFight = multi = false;
        target = 0;
    }
	
	void FixedUpdate () {
        //once in boss room
        if (startFight) {
            //find and rotate
            tPlayer = GameObject.FindWithTag("SHOOTME").transform;
            direction = tPlayer.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);


            //timer
            time += 1;

            if (time >= fireRate)
                bigShot();
            if (time % multiFireRate == 0)
            {
                if (multi)
                    multiShot();
                else
                    multiShot2();
            }
        }


    }

    //take damage
    void deductPoints(int damageAmount)
    {
        enemyHealth -= damageAmount;
        if (!pain.isPlaying)
        {
            pain.Play();
        }

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("winScreen");
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //large shot
    void bigShot()
    {
        time = 0;

        Vector3 sdirection = tPlayer.position - bigFirePoint.transform.position;

        switch (target)
        {
            case 0:
                target = 2;

                bigFirePoint.transform.rotation = Quaternion.Slerp(bigFirePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(bigProjectile, bigFirePoint.position, bigFirePoint.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
                break;
            case 1:
                target = 0;

                sdirection = target1.position - bigFirePoint.transform.position;
                bigFirePoint.transform.rotation = Quaternion.Slerp(bigFirePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(bigProjectile, bigFirePoint.position, bigFirePoint.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
                break;
            case 2:
                target = 3;

                sdirection = target2.position - bigFirePoint.transform.position;
                bigFirePoint.transform.rotation = Quaternion.Slerp(bigFirePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(bigProjectile, bigFirePoint.position, bigFirePoint.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
                break;
            case 3:
                target = 1;

                sdirection = target3.position - bigFirePoint.transform.position;
                bigFirePoint.transform.rotation = Quaternion.Slerp(bigFirePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(bigProjectile, bigFirePoint.position, bigFirePoint.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
                break;
        }
    }
    //multi shot
    void multiShot()
    {
        //random
        multi = false;
        Vector3 sdirection = target1.position - multi1.transform.position;
        multi1.transform.rotation = Quaternion.Slerp(multi1.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi1.position, multi1.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;

        sdirection = target2.position - multi2.transform.position;
        multi2.transform.rotation = Quaternion.Slerp(multi2.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi2.position, multi2.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;

        sdirection = target3.position - multi3.transform.position;
        multi3.transform.rotation = Quaternion.Slerp(multi1.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi3.position, multi3.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
        
    }

    void multiShot2()
    {
        multi = true;
        Vector3 sdirection = tPlayer.position - multi1.transform.position;
        multi1.transform.rotation = Quaternion.Slerp(multi1.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi1.position, multi1.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;

        sdirection = tPlayer.position - multi2.transform.position;
        multi2.transform.rotation = Quaternion.Slerp(multi2.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi2.position, multi2.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;

        sdirection = tPlayer.position - multi3.transform.position;
        multi3.transform.rotation = Quaternion.Slerp(multi3.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
        shot = Instantiate(multiProjectile, multi3.position, multi3.transform.rotation) as GameObject;
        shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
    }

    void startFightMSG(int dumb) { startFight = true; }
}
