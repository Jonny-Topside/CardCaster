using UnityEngine;
using System.Collections;

public class ambushSpawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawn1;
    public Transform spawn2;
    bool wasUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!wasUsed)
            {
                Instantiate(enemy, spawn1.position, spawn1.rotation);
                Instantiate(enemy, spawn2.position, spawn2.rotation);
                wasUsed = true;
            }
        }
    }


}
