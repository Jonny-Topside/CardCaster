using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public GameObject cardCom;
    public GameObject gunCom;
    public GameObject knifeCom;
    //public GameObject camCom;
    //public GameObject cam;

    public CharacterController cc;
    public float speed = 6.0f;
    public int normSpeed = 125;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Vector3 moveDirection = Vector3.zero;
    AudioSource gotHit;
    public AudioSource jumpSound, landSound, walk1, walk2, walk3, walk4;
    bool jumping, one, two, three, four;
    int timer;
    CharacterController me;

    //for dashing
    public int counting = 0;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<CharacterController>().radius = 4;
        gameObject.GetComponent<CharacterController>().height = 16;

        //Makes sure the cursor stays within the window of the game and is not visible
        Cursor.lockState = CursorLockMode.Locked;
        cc = GetComponent<CharacterController>();

        gotHit = GetComponent<AudioSource>();

        jumping = one = two = three = four = false;
        timer = 0;
        me = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check to see if the player has fallen off the map
        if (transform.position.y <= -5000)
            SceneManager.LoadScene("GameOver");

        if (Input.GetButtonUp("Crouch"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        }
        if (Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
        {
            cc.height = 4;
        }
        else
        {
            cc.height = 16;
        }

        if (Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
        {
            speed = normSpeed * 0.2f;
        }
        else if (Input.GetButton("Sprint"))
        {
            speed = normSpeed * 2;
        }
        else
        {
            speed = normSpeed;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        //if on the ground
        if (cc.isGrounded)
        {
            //make a new vector that will determine the players movement direction
            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
            //and then move them across it
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (me.velocity != Vector3.zero)
            {
                timer += 1;
                if (timer == 20)
                {
                    walk1.Play();
                }
                else if (timer == 40)
                {
                    walk2.Play();
                }
                else if (timer == 60)
                {
                    walk3.Play();
                }
                else if (timer == 80)
                {
                    timer = 0;
                    walk4.Play();
                }
            }
            if (jumping)
            {
                jumping = false;
                landSound.Play();
            }
            //if space is pressed
            if (Input.GetButton("Jump"))
            {
                //jump up
                moveDirection.y = jumpSpeed;
                jumpSound.Play();
                jumping = true;
            }
        }
        else
        {
            // moveDirection.x = Input.GetAxis("Horizontal") * speed;
            // moveDirection.z = Input.GetAxis("Vertical") * speed;
            Vector3 inputVector = new Vector3(moveHorizontal, 0, moveVertical);
            inputVector = transform.TransformDirection(inputVector);
            Vector2 clampedHorizontalVelocity = Vector2.ClampMagnitude(new Vector2(cc.velocity.x, cc.velocity.z), speed);
            moveDirection = new Vector3(clampedHorizontalVelocity.x, cc.velocity.y, clampedHorizontalVelocity.y) + (inputVector * Time.deltaTime * speed);
        }
        
        if (cardCom.GetComponent<CardStuff>().dashing == false)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);
        } else
        {
            knifeCom.SetActive(true);
            cc.Move(((transform.forward * 2500 * Time.deltaTime))); /*+ moveDirection)*/ // * Time.deltaTime
            counting++;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            gotHit.Play();
        }
    }
}


