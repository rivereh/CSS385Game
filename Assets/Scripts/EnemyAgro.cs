using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    Skeleton enemyParent;
    bool inRange;
    Animator anim;

    void Awake()
    {
        enemyParent = GetComponentInParent<Skeleton>();
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_Attack"))
        {
            enemyParent.Flip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
        }
    }
}
