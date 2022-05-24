using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{   
    public GameObject textCanvas;
    bool playerInRange = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddHealth(100);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            textCanvas.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            textCanvas.SetActive(false);
        }
    }
}
