using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour
{
    public Vector2 look;
    public Vector2 smoothV;
    public float sensitivity = 500.0f;
    public float smoothing = 2.0f;
    GameObject character;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Input.GetButton("Crouch") && !Input.GetButton("Sprint"))
        {
            transform.localPosition = new Vector3(0, 6, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, 8, 0);
        }
        Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        look += smoothV;
        look.y = Mathf.Clamp(look.y, -90f, 90f);
        transform.localRotation = Quaternion.AngleAxis(-look.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(look.x, character.transform.up);

        //check for right click
        if (Input.GetButtonDown("Fire2"))
        {
            if (GetComponent<Camera>().fieldOfView == 70)
            {
                GetComponent<Camera>().fieldOfView = 50;
            }
            else
            {
                GetComponent<Camera>().fieldOfView = 70;
            }
        }
    }
}
