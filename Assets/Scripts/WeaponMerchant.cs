using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMerchant : MonoBehaviour
{

    public GameObject textCanvas;
    bool playerInRange = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            // MenuManager.instance.PauseGame();
            MenuManager.instance.OpenMenu("weaponshop");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            textCanvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            textCanvas.SetActive(false);
            MenuManager.instance.CloseMenu("weaponshop");
        }
    }
}
