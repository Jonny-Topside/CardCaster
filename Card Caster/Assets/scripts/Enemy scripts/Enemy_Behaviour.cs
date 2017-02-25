using UnityEngine;
using System.Collections;


public class Enemy_Behaviour : MonoBehaviour
{

    //enemy script
    [SerializeField]
    int enemyHealth;
    float bulletSpeed;
    int fireRate;
    bool bleed;
    public GameObject bloodParticles;

    //POTION
    public GameObject potion;

    //bool dead = false;


    //follow player
    //[SerializeField]
    Transform tPlayer;
    [SerializeField]
    float enemyrange;
    [SerializeField]
    float EnemySpeed;
    [SerializeField]
    float shootRange;
    Vector3 direction;

    // [SerializeField]
    // bool bPatrol;
    // [SerializeField]
    // bool bFollow;

    //code for projectile
    float time;
    public Transform Spawnpoint;
    public GameObject prefab;
    GameObject shot;

    //patrol position Waypoints

    public int randomPoint;
    public GameObject[] rightPoints;
    public Transform[] points;
    public int destPoint;
    private UnityEngine.AI.NavMeshAgent agent;
    bool line;

    //instance
    [SerializeField]
    enemyInstance eInstance;

    //some vectors
    //Vector3 playerPosition;
    float playerDistance;
    //enum enemy instances
    enum enemies
    {
        ROBOT,
        TURRET,
        MONSTER
    }

    public enum enemyInstance
    {
        STAND,
        PATROL,
        FOLLOW,
        FOLLOWNSHOOT,
        STOP,
        STOPNSHOOT,
        EXPLODE
    };

    void deductPoints(int damageAmount)
    {
        bleed = true;
        enemyHealth -=  damageAmount;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
           // if (this.tag == "ROBOT")
                Instantiate(potion, transform.position, transform.rotation);
          
        }
    }
    void EnemyHit()
    {
        Instantiate(bloodParticles, transform.position, transform.rotation);
    }
    void BloodEffect()
    {
        if (bleed)
        {
            EnemyHit();
            bleed = false;
        }


    }

    void EnemyStop()
    {
        if (playerDistance < shootRange)
        {
            this.transform.position = this.transform.position;
        }
    }


    void GoToNextPoint()
    {

        if (points.Length == 0)
        {
            return;
        }

        agent.SetDestination(points[destPoint].position);

        if (destPoint >= points.Length - 1)
        {
            line = true;
        }
        else if (destPoint <= 0)
        {
            line = false;
        }


        if (line)
        { destPoint = (destPoint - 1); }
        else
        { destPoint = (destPoint + 1); }
    }

    private void ResetEnemy()
    {
        destPoint = 1;
        agent.SetDestination(points[destPoint].position);
        line = false;
        // eInstance = 1;
    }

    void EnemyBehave(enemyInstance _es)
    {
        //distance from player

        switch (_es)
        {
            case enemyInstance.STAND:
                {
                    EnemySniper();

                    break;
                }
            case enemyInstance.PATROL:
                {
                    //Big problem with this if statement bPatrol is being reset to true and I cannot see why
                    //if (bPatrol)
                    //   {
                    //float x = Vector3.Distance(tPlayer.position, this.transform.position);
                    if (playerDistance < enemyrange)
                    {
                        // bPatrol = false;
                        // bFollow = true;
                        eInstance = enemyInstance.FOLLOWNSHOOT;


                    }
                    else
                    {
                        //THIS IS CHECKING IF PLAYERS TRANSFORM EXISTS? IT ALWAYS DOES SO THIS WILL 
                        //ALWAYS BE TRUE
                        //if (tPlayer.transform)
                        if (agent.remainingDistance < 20f)
                            GoToNextPoint();
                        //THE SECOND RUN AROUND OF THIS FUNCTION CALLS THIS
                        //GOTO FUNCTION AND IT NEVER REACHES THE POINT BECAUSE FOLLOW IS INTERFERING

                    }
                }
                break;
            //  }
            //following and attacking
            case enemyInstance.FOLLOWNSHOOT:
                {

                    EnemyFollow();
                    Shoot();
                    if (playerDistance <= 40 || playerDistance > enemyrange)
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
                    // bPatrol = false;
                    // bFollow = false;
                    EnemyStop();

                    if (playerDistance > 65)
                    {
                        eInstance = enemyInstance.FOLLOW;
                    }
                    break;

                }

            case enemyInstance.STOPNSHOOT:
                {
                    // bPatrol = false;
                    // bFollow = false;
                    EnemyStop();
                    EnemySniper();
                    if (playerDistance > 65)
                    {
                        eInstance = enemyInstance.FOLLOWNSHOOT;
                    }
                    break;
                }

            case enemyInstance.EXPLODE:
                {
                    // bPatrol = false;
                    // bFollow = false;

                    if (playerDistance > 15 && enemyHealth == 20 && this.CompareTag("MONSTER"))
                    {


                        enemyHealth = -1;
                    }
                    break;
                }

            default:
                {
                    _es = 0;
                    break;
                }
        }
    }
    void EnemyFollow()
    {

        //head system

        //don not rotate y axis
        direction.y = 0;


        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);

        this.transform.position += this.transform.forward * EnemySpeed * Time.deltaTime;
        agent.SetDestination(tPlayer.position);
        // orbit the player
        // transform.Translate(Vector3.right * Time.deltaTime);




    }

    void EnemySniper()
    {
        //  p.enabled = false;
        //getting direction

        //don not rotate y axis
        direction.y = 0;

        //enemy facing player
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
        Shoot();

    }
    void Shoot()
    {


        time += 1;
        if (time >= fireRate)
        {
            time = 0;

            //rotar el eje x en direccion al jugador



            //shoot range variable... gives an enemy a max range to shoot...
            if (playerDistance < shootRange)
            {
                Vector3 sdirection = tPlayer.position - Spawnpoint.transform.position;
                Spawnpoint.transform.rotation = Quaternion.Slerp(Spawnpoint.transform.rotation, Quaternion.LookRotation(sdirection), 0.2f);
                shot = Instantiate(prefab, Spawnpoint.position, Spawnpoint.transform.rotation) as GameObject;
                shot.GetComponent<Rigidbody>().velocity = sdirection * bulletSpeed;
            }
            //prefab = (GameObject)Instantiate(Resources.Load("EnemyProjectile"));
        }

    }

    void Start()
    {
        //enemy init
        if (this.CompareTag("ROBOT"))
        {

            EnemyInit(enemies.ROBOT);

        }
        else if (this.CompareTag("TURRET"))
        {
            EnemyInit(enemies.TURRET);
        }
        else if (this.CompareTag("MONSTER"))
        {
            EnemyInit(enemies.MONSTER);
        }
        else
        {
            EnemyInit(enemies.ROBOT);
        }

        //code for projectile
        time = 0.0f;
        //dead = false;
        //playerPosition = tPlayer.position;


        //MERGE CONFLICT OCCURED HERE, CARLOS CODE ABOVE, MINE BELOW

        //potion = GameObject.Find("Bottle_Health");
        //instance
        //eInstance = 0;
        //player Target using label
        //tPlayer = GameObject.FindWithTag("Player").transform;
        //checkpoints
        destPoint = 0;
        randomPoint = Random.Range(0, rightPoints.Length);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //Go To Next WayPoint
        line = true;


    }

    void Update()
    {
        tPlayer = GameObject.FindWithTag("SHOOTME").transform;
        direction = tPlayer.position - this.transform.position;
        playerDistance = Vector3.Distance(tPlayer.position, this.transform.position);

         if (enemyHealth <= 0)
         {
  }

        //if (eInstance == enemyInstance.FOLLOW)
        //{
        //    bPatrol = false;
        //    bFollow = true;


        //}
        //else if (eInstance == enemyInstance.PATROL)
        //{
        //    bPatrol = true;
        //    bFollow = false;
        //}
        //else
        //{
        //    bPatrol = false;
        //    bFollow = false;
        //}
        BloodEffect();
        EnemyBehave(eInstance);

    }

    void EnemyInit(enemies _en)
    {
        switch (_en)
        {
            case enemies.ROBOT:
                enemyHealth = 10;
                bulletSpeed = 3;
                fireRate = 30;
                enemyrange = 500;
                EnemySpeed = 60;
                shootRange = 400;
                eInstance = enemyInstance.PATROL;

                break;

            case enemies.TURRET:
                enemyHealth = 30;
                bulletSpeed = 5;
                fireRate = 50;
                enemyrange = 800;
                EnemySpeed = 0;
                shootRange = 800;
                eInstance = enemyInstance.STAND;
                break;

            case enemies.MONSTER:
                enemyHealth = 50;
                bulletSpeed = 2;
                fireRate = 150;
                enemyrange = 450;
                EnemySpeed = 4;
                shootRange = 340;
                eInstance = enemyInstance.FOLLOWNSHOOT;
                break;

            default:
                enemyHealth = 10;
                bulletSpeed = 6;
                fireRate = 25;
                enemyrange = 400;
                EnemySpeed = 4;
                shootRange = 390;
                eInstance = enemyInstance.PATROL;
                break;
        }

    }
}




