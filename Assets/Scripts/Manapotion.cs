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
            // only consume mana potion if player needs it
            PlayerController pc = other.transform.GetComponent<PlayerController>();
            if (pc.mana + manaToAdd <= 100)
            {
                pc.AddMana(manaToAdd);
                Destroy(gameObject);
            }
        }
    }
}
