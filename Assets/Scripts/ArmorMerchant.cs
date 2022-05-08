using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmorMerchant : MonoBehaviour
{
    public GameObject textCanvas;
    bool playerInRange = false;
    public TMP_Text priceText;
    public Button buyButton;

    int priceToUpgrade = 100;


    public void BuyUpgrade()
    {
        if (PlayerStats.instance.coins >= priceToUpgrade)
        {
            PlayerStats.instance.DecreaseCoins(priceToUpgrade);
            priceToUpgrade += 100;
            PlayerStats.instance.IncreaseDefence();
            PlayerStats.instance.UpdateAnimatorController();
            priceText.text = priceToUpgrade + " Coins to Upgrade";
            if (PlayerStats.instance.defence == 4)
            {
                buyButton.interactable = false;
                priceText.text = "All Upgrades Bought!";
                return;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            // MenuManager.instance.PauseGame();
            MenuManager.instance.OpenMenu("armorshop");
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
            MenuManager.instance.CloseMenu("armorshop");
        }
    }
}
