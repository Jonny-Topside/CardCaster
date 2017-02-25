using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CardStuff : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    public GameObject knife;
    public GameObject wall;

    AudioSource swing;

    //card effect bools
    bool bonusJump = false;
    bool bonusHealth = false;
    bool bonusDraw = false;
    public bool dashing = false;

    public bool drawRev = false;
    public bool drawSMG = false;
    public bool drawPist = false;

    int counting = 0;
    public int ammo;
    //end of card effects

    public Sprite[] faces;
    public GameObject selector;

    public GameObject[] oHand = new GameObject[5];

    public List<int> hand;
    public int inHand = 0;
    private int selected = 1;

    public Stack<int> deck = new Stack<int>();

    //EFFECT BOOLS
    //bool timerBool = false;

    public void drawCard()
    {
        if (inHand < 5 && (deck.Count > 0))
        {
            deck.Push((int)Random.Range(2, 9.999f));
            hand[inHand] = deck.Pop();
            inHand++;
        }
    }



    public void useCard(int _card)
    {
        if (_card == 1)
        {
            if (GetComponent<SpeedTimer>().enabled)
            {
                GetComponent<SpeedTimer>().t = 6;
            }
            else
            {
                GetComponent<SpeedTimer>().enabled = true;
            }
        }
        if (_card == 2)
        {
            if (player.GetComponent<PlayerHealth>().currHP < 10)
            {
                bonusHealth = true;
            }
        }
        if (_card == 3)
        {
            bonusJump = true;
        }
        if (_card == 4)
        {
            bonusDraw = true;
        }
        if (_card == 5)
        {
            if (GetComponent<DamageTimer>().enabled)
            {
                GetComponent<DamageTimer>().t = 6;
            }
            else
            {
                GetComponent<DamageTimer>().enabled = true;
            }
        }
        if (_card == 6)
        {
            Instantiate(wall, player.transform.position + (player.transform.forward * 40) + (player.transform.up * 13), player.transform.rotation);
        }
        if (_card == 7)
        {
            dashing = true;
            swing.Play();
        }
        if(_card == 8)
        {
            drawRev = true;
            ammo = 12;
        }
        if (_card == 9)
        {
            drawSMG = true;
            ammo = 120;
        }

    }

    
    void Awake()
    {
        swing = GetComponent<AudioSource>();
        deck.Push((int)Random.Range(2.0f, 9.99999f));

        drawCard();
        drawCard();
        drawCard();
        drawCard();
        drawCard();

    }

    private void Update()
    {
        if (Input.GetButtonDown("q"))
        {
            //for testing
            //drawCard();
        }
        //CARD EFFECTS

        //1
        if (GetComponent<SpeedTimer>().enabled == true)
        {
            player.GetComponent<playerMove>().normSpeed = 250;
        }
        else
        {
            player.GetComponent<playerMove>().normSpeed = 125;
        }

        //2
        if (bonusHealth)
        {
            player.GetComponent<PlayerHealth>().currHP += 4;
            bonusHealth = false;
        }

        //3
        if (bonusJump)
        {
            player.GetComponent<playerMove>().moveDirection.y = 700;
            player.GetComponent<playerMove>().cc.Move(player.GetComponent<playerMove>().moveDirection * Time.deltaTime);
            bonusJump = false;
        }

        //4
        if (bonusDraw)
        {
            player.GetComponent<PlayerHealth>().currHP -= 2;
            drawCard();
            drawCard();
            bonusDraw = false;
        }
        //5
        if (GetComponent<DamageTimer>().enabled == true)
        {
            gun.GetComponent<HandgunDamage>().bonusDam = true;
        }
        else
        {
            gun.GetComponent<HandgunDamage>().bonusDam = false;
        }
        //7
        if(player.GetComponent<playerMove>().counting >= 8)
        {
            player.GetComponent<playerMove>().counting = 0;
            dashing = false;
            StartCoroutine(Waiting());
        }
        //8 and 9
        if(ammo <= 0)
        {
            drawPist = true;
        }
        
        if (Input.GetKeyDown("t"))
        {
            if (Time.timeScale == 1.0f)
            {
              //  Time.fixedDeltaTime = 0;
                //time = true;
                Time.timeScale = 0;
            }
            else
            {
                
                Time.timeScale = 1.0f;
            }
            //   //  if (esc == true)
            //   //  {
            //   //      esc = false;
            //   //      Time.timeScale = 1.0f;
            //   //  }
            //
            //  }
        }
        //CARD EFFECTS END

        if (Input.GetButtonDown("1"))
        {
            selected = 1;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if (Input.GetButtonDown("2"))
        {
            selected = 2;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if (Input.GetButtonDown("3"))
        {
            selected = 3;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if (Input.GetButtonDown("4"))
        {
            selected = 4;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if (Input.GetButtonDown("5"))
        {
            selected = 5;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if ((Input.GetAxis("Mouse ScrollWheel") < 0) && selected < 5)
        {
            selected++;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }

        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && selected > 1)
        {
            selected--;
            selector.transform.localPosition = new Vector3(-193 + ((selected - 1) * 95), 0, 0);
        }
        
        
        

        if (Input.GetButtonDown("e"))
        {
            if (selected == 1)
            {
                if (inHand > 0)
                {
                    useCard(hand[0]);
                    hand.RemoveAt(0);
                    hand.Add(0);
                    inHand--;
                }
            }

            if (selected == 2)
            {
                if (inHand > 1)
                {
                    useCard(hand[1]);
                    hand.RemoveAt(1);
                    hand.Add(0);
                    inHand--;
                }
            }
            if (selected == 3)
            {
                if (inHand > 2)
                {
                    useCard(hand[2]);
                    hand.RemoveAt(2);
                    hand.Add(0);
                    inHand--;
                }
            }
            if (selected == 4)
            {
                if (inHand > 3)
                {
                    useCard(hand[3]);
                    hand.RemoveAt(3);
                    hand.Add(0);
                    inHand--;
                }
            }
            if (selected == 5)
            {
                if (inHand > 4)
                {
                    useCard(hand[4]);
                    hand.RemoveAt(4);
                    hand.Add(0);
                    inHand--;
                }
            }
        }

        for (int i = 0; i < inHand; ++i)
        {
            oHand[i].GetComponent<Image>().sprite = faces[hand[i]];
            oHand[i].GetComponent<Transform>().localScale = new Vector3(75, 110, 1);
            oHand[i].GetComponent<CanvasRenderer>().SetAlpha(1.0f);
        }

        if (inHand < 5)
        {
            for (int i = 4; i >= inHand; --i)
            {
                oHand[i].GetComponent<Image>().sprite = faces[0];
                oHand[i].GetComponent<Transform>().localScale = new Vector3(75, 110, 1);
                oHand[i].GetComponent<CanvasRenderer>().SetAlpha(1.0f);
            }
        }




    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(0.2f);
        knife.SetActive(false);
    }

}
