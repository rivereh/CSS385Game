using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] int healthToAdd = 25;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {   
            PlayerController pc = other.transform.GetComponent<PlayerController>();
            pc.AddHealth(healthToAdd);
            Destroy(gameObject);
        }
    }
}
