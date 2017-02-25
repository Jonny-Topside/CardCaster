using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    //
    public int maxHealth = 10;
    public int currHP;
    //BLOOD OVERLAY STUFF
    bool takingDamage;
    Image bloodAlpha;
    public GameObject bloodOverlay;
    Color c;
    public Slider healthBar;
   // GameObject healthBar;
    public GameObject cardStuffObject;

    //THIS IS THE HEART IMAGE FOR EVERY HEART
    RawImage heartImage;
     GameObject heart1;
     GameObject heart2;
     GameObject heart3;
     GameObject heart4;
     GameObject heart5;
    // Color heart;
    static float t;

    
    void Start () {
        currHP = maxHealth;
 
        //FIND BLOOD OVERLAY
        //bloodOverlay = GameObject.Find("BloodOverlay");
        //GET BLOOD OVERLAY IMAGE 
        bloodAlpha = bloodOverlay.GetComponent<Image>();

        c = bloodAlpha.color;

        takingDamage = false;
        t = 0.0f;       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("healthPickup"))
        {
            if (currHP < 10)
            {
            other.gameObject.SetActive(false);
                currHP += 2;
            }
            //THE HEALTH HERE HAS A PROBLEM WITH THIS LOGIC CAHNGE IT
            else if(currHP >= 10)
            {
                currHP = 10;
            }
        }

        if (other.tag == "cardPickup")
        {
            

            if(cardStuffObject.GetComponent<CardStuff>().inHand <= 4)
            {
                other.gameObject.SetActive(false);
                cardStuffObject.GetComponent<CardStuff>().drawCard();
            }
        }
    }

    void Update () {
        if(healthBar)
            healthBar.value = currHP;

        //PUT HEART DAMAGE CASES INSIDE HERE
        //death tester
        if (currHP <= 0)
        {

            if (gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
            }
            load();
        }
      
        //lerp blood overlay
        if (takingDamage)
        {
            //t += 0.99f * Time.deltaTime;
            //c.a = Mathf.Lerp(1.0f, 0.0f, t);
            c.a -= .02f;
            bloodAlpha.color = c;
            if (c.a <= 0.0f)
                takingDamage = false;
        }
	}

    public void takeDamage(int amount)
    {
        //if (bloodAlpha)

        //t = 0.0f;
        takingDamage = true;
        currHP -= amount;
        c.a = 1.0f;
        bloodAlpha.color = c;
        
        //StartCoroutine(timer());
        
    }
    public void load()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");

    }

    //IEnumerator timer ()
    //{

    //    yield return new WaitForSeconds(3.5f);
    //    takingDamage = false;
    //}
    public void ExplotionDamage()
    {
        int explosionDamage = Random.Range(0, 3);
        currHP = currHP - explosionDamage;

    }
}
