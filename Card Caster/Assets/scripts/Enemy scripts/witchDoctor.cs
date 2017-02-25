using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witchDoctor : MonoBehaviour
{

    public int enemyHealth, bulletSpeed;
    public float detectionRange, attackRange;
    [SerializeField]
    public GameObject potion;
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public GameObject exclamation;
    public bool exBool;
    [SerializeField]
    public Transform firePoint;

    [SerializeField]
    witchInstance wInstance;

    float playerDistance, time;
    Vector3 direction;
    Transform tPlayer;
    GameObject shot;
    AudioSource pain;
    RaycastHit rayShot;

    //private UnityEngine.AI.NavMeshAgent agent;

    public enum witchInstance
    {
        STAND,
        HIT,
        FIRE,
        EXPLODE
    };

    // Use this for initialization
    void Start()
    {
        time = 0.0f;
        pain = GetComponent<AudioSource>();
        exBool = false;
        tPlayer = GameObject.FindWithTag("SHOOTME").transform;
    }

    // Update is called once per frame
    void Update()
    {

        
        direction = tPlayer.position - this.transform.position;
        playerDistance = Vector3.Distance(tPlayer.position, this.transform.position);

        if (exBool)
        {
            exclamation.SetActive(true);
        }
        else
        {
            exclamation.SetActive(false);
        }

        //time += 1;

        switch (wInstance)
        {
            case witchInstance.STAND:
                {
                    GetComponent<Animation>().Play("witchIdle");
                }
                break;
            case witchInstance.HIT:
                { 
                    GetComponent<Animation>().Play("witchHurt");
                }
                break;
            case witchInstance.FIRE:
                {
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
                    GetComponent<Animation>().Play("attackAnim");
                    /*
                    if (time >= fireRate)
                    {
                        time = 0;

                        //shoot range variable... gives an enemy a max range to shoot...
                        if (playerDistance < attackRange)
                        {
                            Vector3 sdirection = tPlayer.position - firePoint.transform.position;
                            firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                            shot = Instantiate(projectile, firePoint.position, firePoint.transform.rotation) as GameObject;
                            shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
                        }
                    }
                    */
                }
                break;
            case witchInstance.EXPLODE:
                {
                    break;
                }
        }



    }
    void deductPoints(int damageAmount)
    {
        enemyHealth -= damageAmount;
        exBool = false;

        if (!pain.isPlaying)
        {
            pain.Play();
        }

        //stop when hit
        GetComponent<Animation>().Stop();
        wInstance = witchInstance.HIT;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(potion, transform.position, transform.rotation);
        }
        StartCoroutine(waiting());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "knife")
        {
            deductPoints(9999);
        }
    }

    IEnumerator waiting()
    {
        yield return new WaitForSeconds(0.5f);
        wInstance = witchInstance.FIRE;
        //GetComponent<Animation>().Stop();
    }

    private void fireShot()
    {
        if (Physics.Raycast(transform.position, direction, out rayShot))
        {
            if (rayShot.collider.tag == "Player")
            {
                Vector3 sdirection = tPlayer.position - firePoint.transform.position;
                firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(projectile, firePoint.position, firePoint.transform.rotation);
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
            }
        }
    }

    private void exOn()
    {
        exBool = true;
    }
    private void exOff()
    {
        exBool = false;
    }
}
