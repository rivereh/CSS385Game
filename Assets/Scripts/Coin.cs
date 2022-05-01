using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int value;


    public void setValue(int newValue)
    {
        value = newValue;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {   
            PlayerStats ps = other.transform.GetComponent<PlayerStats>();
            ps.AddCoins(value);
            Destroy(gameObject);
        }
    }

}
