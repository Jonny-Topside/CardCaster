using UnityEngine;
using System.Collections;

public class playerJump : MonoBehaviour
{
    public float jumpSpeed = 5.0f;
    public float gravity = 20.0f;
    public float inAirSpeed = 10;
    public CharacterController cc;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cc.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            { 
                moveDirection.x = Input.GetAxis("Horizontal") * inAirSpeed;
                moveDirection.z = Input.GetAxis("Vertical") * inAirSpeed;
                Vector3 inputVec = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                inputVec = transform.TransformDirection(inputVec);
                Vector2 clampedHorizontalVelocity = Vector2.ClampMagnitude(new Vector2(cc.velocity.x, cc.velocity.z), jumpSpeed);
                moveDirection = new Vector3(clampedHorizontalVelocity.x, cc.velocity.y, clampedHorizontalVelocity.y) + (inputVec * Time.deltaTime * jumpSpeed);


            }
            moveDirection.y -= gravity * Time.deltaTime;
            cc.Move(moveDirection * Time.deltaTime);


            //cc.transform.up = jumpSpeed;
        }
    }
}
