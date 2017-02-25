using UnityEngine;
using System.Collections;

public class HandgunDamage : MonoBehaviour
{
    public GameObject cardStuff;

    public int damageAmount;

    int gun1Damage;
    int gun2Damage;
    int gun3Damage;
    public bool bonusDam = false;

    float gun1Wait;
    float gun2Wait;
    float gun3Wait;
    public float readyTimer = 0;
    public bool ready;


    public float targetDist;
    public float allowedRange = 15.0f;
    public GameObject bulletSpark, muzzleFlash, muzzleFlash2, muzzleFlash3, bloodParticles;
    // public ParticleSystem part;
    public GameObject gun;
    public GameObject gun2;
    public GameObject gun3;

    public int currentGun;

    private LineRenderer lineRenderer;
    void Start()
    {
        //   part = GetComponent<ParticleSystem>();
        lineRenderer = GetComponent<LineRenderer>();
        currentGun = 1;
        gun.SetActive(true);
        gun2.SetActive(false);
        gun3.SetActive(false);
        ready = true;

        gun1Damage = 5;
        gun2Damage = 20;
        gun3Damage = 3;

        gun1Wait = 0.1666f;
        gun2Wait = 1.15f;
        gun3Wait = 0.05f;

    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {


            if (cardStuff.GetComponent<CardStuff>().drawPist)
            {
                gun.SetActive(true);
                gun2.SetActive(false);
                gun3.SetActive(false);
                currentGun = 1;
                cardStuff.GetComponent<CardStuff>().drawPist = false;
            }
            if (cardStuff.GetComponent<CardStuff>().drawRev)
            {
                gun.SetActive(false);
                gun2.SetActive(true);
                gun3.SetActive(false);
                currentGun = 2;
                cardStuff.GetComponent<CardStuff>().drawRev = false;
            }
            if (cardStuff.GetComponent<CardStuff>().drawSMG)
            {
                gun.SetActive(false);
                gun2.SetActive(false);
                gun3.SetActive(true);
                currentGun = 3;
                cardStuff.GetComponent<CardStuff>().drawSMG = false;
            }

            if (!bonusDam)
            {
                if (currentGun == 1)
                {
                    damageAmount = gun1Damage;
                }
                if (currentGun == 2)
                {
                    damageAmount = gun2Damage;
                }
                if (currentGun == 3)
                {
                    damageAmount = gun3Damage;
                }
            }
            if (bonusDam)
            {
                if (currentGun == 1)
                {
                    damageAmount = gun1Damage * 2;
                }
                if (currentGun == 2)
                {
                    damageAmount = gun2Damage * 2;
                }
                if (currentGun == 3)
                {
                    damageAmount = gun3Damage * 2;
                }
            }
            if (Input.GetButton("Sprint"))
            {
                if (currentGun == 1)
                {
                    gun.GetComponent<Animation>().Stop();
                    gun.GetComponent<Animation>().Play("sprintGun");
                }
                if (currentGun == 2)
                {
                    gun2.GetComponent<Animation>().Stop();
                    gun2.GetComponent<Animation>().Play("sprintGun");
                }
                if (currentGun == 3)
                {
                    gun3.GetComponent<Animation>().Stop();
                    gun3.GetComponent<Animation>().Play("sprintGun");
                }
            }
            if (Input.GetButtonUp("Sprint"))
            {
                if (currentGun == 1)
                {
                    gun.GetComponent<Animation>().Play("idleGun");
                }
                if (currentGun == 2)
                {
                    gun2.GetComponent<Animation>().Play("idleGun");
                }
                if (currentGun == 3)
                {
                    gun3.GetComponent<Animation>().Play("idleGun");
                }
            }

            readyTimer -= 1 * Time.deltaTime;
            if (readyTimer <= 0)
            {
                readyTimer = 0;
                ready = true;
            }
            if (readyTimer > 0)
            {
                ready = false;
            }
            if (Input.GetButton("Fire1") && !Input.GetButton("Sprint") && ready)
            {
                if (currentGun == 1)
                {
                    readyTimer = gun1Wait;
                }
                else if (currentGun == 2)
                {
                    readyTimer = gun2Wait;
                }
                else if (currentGun == 3)
                {
                    readyTimer = gun3Wait;
                }
                RaycastHit shot;
                cardStuff.GetComponent<CardStuff>().ammo--;
                if (!bonusDam)
                {
                    StartCoroutine(flash());
                }
                else
                {
                    if (currentGun == 1)
                    {
                        lineRenderer.SetPosition(0, muzzleFlash.transform.position - (muzzleFlash.transform.forward * 0.05f));
                    }
                    else if (currentGun == 2)
                    {
                        lineRenderer.SetPosition(0, muzzleFlash.transform.position + (muzzleFlash.transform.forward * 0.4f));
                    }
                    else if (currentGun == 3)
                    {
                        lineRenderer.SetPosition(0, muzzleFlash.transform.position - (muzzleFlash.transform.forward * 0.05f));
                    }

                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
                    {
                        lineRenderer.SetPosition(1, shot.point);
                    }
                    else
                    {
                        lineRenderer.SetPosition(1, muzzleFlash.transform.position + (GetComponentInParent<Camera>().transform.forward * 200));
                    }
                    StartCoroutine(laserDraw());
                }

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out shot))
                {
                    if (shot.collider.tag == "Enemy")
                    {
                        shot.transform.SendMessage("deductPoints", damageAmount);
                        var q = Quaternion.FromToRotation(Vector3.up, shot.normal);
                        Instantiate(bloodParticles, shot.point, q);
                    }
                    else if (shot.collider.tag == "Headshot")
                    {
                        shot.transform.SendMessage("deductPoints", damageAmount * 4);
                        var q = Quaternion.FromToRotation(Vector3.up, shot.normal);
                        Instantiate(bloodParticles, shot.point, q);
                    }
                    else if (shot.collider.tag == "Secret")
                    {
                        shot.transform.SendMessage("openMe", 1);
                        var q = Quaternion.FromToRotation(Vector3.up, shot.normal);
                        Instantiate(bulletSpark, shot.point, q);
                    }
                    else //if (!(shot.collider.tag == "TURRET" || shot.collider.tag == "ROBOT" || shot.collider.tag == "MONSTER") && shot.collider.tag != "Trigger")
                    {
                        var q = Quaternion.FromToRotation(Vector3.up, shot.normal);
                        Instantiate(bulletSpark, shot.point, q);
                        return;
                    }
                }
            }
        }
    }

    IEnumerator flash()
    {
        if (currentGun == 1)
        {
            muzzleFlash.gameObject.SetActive(true);
            yield return new WaitForSeconds(.01f);
            muzzleFlash.gameObject.SetActive(false);
        }
        if (currentGun == 2)
        {
            muzzleFlash2.gameObject.SetActive(true);
            yield return new WaitForSeconds(.01f);
            muzzleFlash2.gameObject.SetActive(false);
        }
        if (currentGun == 3)
        {
            muzzleFlash3.gameObject.SetActive(true);
            yield return new WaitForSeconds(.01f);
            muzzleFlash3.gameObject.SetActive(false);
        }
    }

    IEnumerator laserDraw()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(.07f);
        lineRenderer.enabled = false;
    }
}
