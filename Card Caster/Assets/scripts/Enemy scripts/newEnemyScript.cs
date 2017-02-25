using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newEnemyScript : MonoBehaviour {

    public Transform bulletSpawn;
    public int enemyHealth, speed, numberOfTeleports, bulletSpeed;
    public GameObject projectile;

    float playerDist, range, time;
    bool teleporting;
    Vector3 facingDirection, right, left;
    Transform playerPos;
    GameObject newShot;
    RaycastHit rayShot;
    // Use this for initialization
    void Start () {
        right = new Vector3(50, 0, 0);
        left = new Vector3(-50, 0, 0);
        time = 0.0f;
        speed = 40;
        range = 300;
        enemyHealth = 10;
        teleporting = true;
        numberOfTeleports = 0;
	}
	
    //THINGS I NEED:
    //ENEMY HAS TO SHOOT IN BURSTS THEN WAIT
    //ENEMY HAS TO BE ABLE TO GET OUT OF A WALL IF IT GETS STUCK IN ONE
    //

        public IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
       // Destroy(newShot);
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        
        playerPos = GameObject.FindGameObjectWithTag("SHOOTME").transform;
        facingDirection = playerPos.position - this.transform.position;
        playerDist = Vector3.Distance(this.transform.position, playerPos.position);
        time += 5;
        if (playerDist < range)
        {
            stop();
        }
        else
            follow();

        if (Physics.Raycast(transform.position, facingDirection, out rayShot))
        {
            if (rayShot.collider.tag == "Player" && Time.timeScale == 1)
                shoot();
        }
	}


    private void follow()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(facingDirection), 0.2f);

        //  if (playerDist <= range)
        //  {
        //      //SHOOT AT AND FOLLOW THE PLAYER
        //  }
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }

     void deductPoints(int damageAmount)
    {
        ++numberOfTeleports;
        int tempAmount = damageAmount;
        if (numberOfTeleports <= 3)
        {
            if (Random.value * 10 % 2 <= 1)
                gameObject.transform.Translate(left);
            else if(Random.value * 10 % 2 >= 0)
                gameObject.transform.Translate(right);

            teleporting = true;
            // gameObject.transform.Translate(50, 0, 0);
            
            damageAmount = 0;
        }
        else if(numberOfTeleports > 3)
        { 
            teleporting = false;
        }

        if(teleporting == false)
        {
            enemyHealth -= damageAmount;
        }


        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void stop()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(facingDirection), 0.2f);
        this.transform.position = this.transform.position;
    }

    void shoot()
    {
        if(time % 100 == 0)
        {
            if(!(time > 300))
            {
        Vector3 sDirection = playerPos.position - bulletSpawn.transform.position;
        bulletSpawn.transform.rotation = Quaternion.Slerp(bulletSpawn.transform.rotation, Quaternion.LookRotation(sDirection), 0.2f);
        newShot = Instantiate(projectile, bulletSpawn.position, bulletSpawn.transform.rotation) as GameObject;
        newShot.GetComponent<Rigidbody>().velocity = sDirection * bulletSpeed;
            }

            else
            {
                wait();
                time = 0;
            }
       // wait();
        }


    }


   // private void OnCollisionEnter(Collision collision)
   // {
   //     if(collision.transform.tag == "Wall")
   //     {
   //         this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(facingDirection), 0.2f);
   //         this.transform.position = Vector3.forward * 50;
   //
   //     }
   // }
}
