using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    BoxCollider2D BoxColliderWithTrigger;
    BoxCollider2D BoxColliderNoTrigger;

    void Awake()
    {
        BoxColliderWithTrigger = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        BoxColliderWithTrigger.isTrigger = true;
        BoxColliderNoTrigger = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        Vector2 ColliderSize;
        ColliderSize = new Vector2(0.09f, 0.001f);
        BoxColliderNoTrigger.size = ColliderSize;
    }

    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.CompareTag("Player"))
        {
            Collider.GetComponent<PlayerController>().TakeDamage(25);
        }
    }
}
