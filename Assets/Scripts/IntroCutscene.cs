using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCutscene : MonoBehaviour
{   
    public Animator camAnimator;
    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.CompareTag("Player"))
        {
            camAnimator.SetBool("Cutscene1", true);
            Invoke(nameof(StopCutscene), 7f);
        }
    }

    void StopCutscene()
    {
        camAnimator.SetBool("Cutscene1", false);
        Destroy(gameObject);
    }
}
