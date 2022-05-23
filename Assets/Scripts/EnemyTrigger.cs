using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{

    Enemy enemyParent;

    void Awake()
    {
        enemyParent = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = other.transform;
            enemyParent.inRange = true;
            enemyParent.agroZone.SetActive(true);
        }
    }
}
