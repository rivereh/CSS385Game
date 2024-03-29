using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manapotion : MonoBehaviour
{
    [SerializeField] int manaToAdd = 25;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {   
            PlayerController pc = other.transform.GetComponent<PlayerController>();
            pc.AddMana(manaToAdd);
            Destroy(gameObject);
        }
    }
}
