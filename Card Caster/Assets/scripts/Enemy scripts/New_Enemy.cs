using System.Collections;
using UnityEngine;

public class New_Enemy : MonoBehaviour
{

    //public variables
    public int enemyHealth, fireRate, bulletSpeed;
    public float detectionRange, speed, attackRange;
    [SerializeField]
    public GameObject potion;
    [SerializeField]
    public GameObject spawnPoint;
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public Transform firePoint;

    //instance
    [SerializeField]
    enemyInstance eInstance;

    //variables
    float playerDistance, time, timeHit;
    Vector3 direction;
    Transform tPlayer;
    GameObject shot;
    bool beenShot, rangedShot;
    AudioSource pain;
    RaycastHit rayShot;

    //private UnityEngine.AI.NavMeshAgent agent;

    public enum enemyInstance
    {
        STAND,
        FOLLOW,
        FOLLOWNSHOOT,
        STOP,
        STOPNSHOOT,
        EXPLODE
    };

    void Start()
    {
        //agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        time = Time.deltaTime;
        timeHit = Time.deltaTime;
        beenShot = rangedShot = false;
        pain = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //find player, rotate, and move
        tPlayer = GameObject.FindWithTag("SHOOTME").transform;
        direction = tPlayer.position - this.transform.position;
        playerDistance = Vector3.Distance(tPlayer.position, this.transform.position);

        //when back in range after being shot
        if (rangedShot && playerDistance + 100 <= detectionRange)
        {
            rangedShot = false;
        }

        //timer
        time += 1;
        timeHit += 1;
        if (timeHit >= 100)
        {
            timeHit = 0;
            beenShot = false;
        }

        
        switch (eInstance)
        {
            case enemyInstance.STAND:
                {
                    EnemySniper();
                    break;
                }
            //following and attacking
            case enemyInstance.FOLLOWNSHOOT:
                {
                    if (!rangedShot && playerDistance >= detectionRange)
                    {
                        rangedShot = true;
                        eInstance = enemyInstance.STOP;
                        break;
                    }
                    EnemyFollow();
                    Shoot();
                    if (playerDistance <= 100)
                    {
                        eInstance = enemyInstance.STOPNSHOOT;
                    }
                    break;
                }
            case enemyInstance.FOLLOW:
                {

                    EnemyFollow();

                    if (playerDistance <= 40)
                    {
                        eInstance = enemyInstance.STOP;
                    }
                    break;
                }
            case enemyInstance.STOP:
                {
                    EnemyStop();

                    if (Physics.Raycast(transform.position, direction, out rayShot))
                    {
                        if (rayShot.collider.tag == "Player")
                            eInstance = enemyInstance.FOLLOWNSHOOT;
                    }
                    break;

                }

            case enemyInstance.STOPNSHOOT:
                {
                    EnemyStop();
                    EnemySniper();
                    if (playerDistance > 200 && !beenShot)
                    {
                        eInstance = enemyInstance.FOLLOWNSHOOT;
                    }
                    break;
                }

            case enemyInstance.EXPLODE:
                {
                    break;
                }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "knife")
        {
            deductPoints(9999);
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


        //stop when hit
        beenShot = true;
        eInstance = enemyInstance.STOPNSHOOT;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            Instantiate(potion, spawnPoint.transform.position, transform.rotation);

        }
    }

    void timeStop(bool timeFlow)
    {
        if (timeFlow == true)
        {

        }
    }
    //follow code
    void EnemyFollow()
    {
        //don not rotate y axis
        //direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);

        if (Physics.Raycast(firePoint.position, direction, out rayShot))
        {
            if (rayShot.collider.tag == "Wall")
            {
                eInstance = enemyInstance.STOP;
            }
        }

        this.transform.position += this.transform.forward * speed * Time.deltaTime;
        //agent.SetDestination(tPlayer.position);

    }
    //shooting code
    void Shoot()
    {

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
    }
    //shoot long range
    void EnemySniper()
    {
        //don not rotate y axis
        //direction.y = 0;

        //enemy facing player
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        Shoot();
    }
    //code to stop
    void EnemyStop()
    {
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
    }
}

