using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponMerchant : MonoBehaviour
{
    public GameObject textCanvas;
    bool playerInRange = false;
    public int axePrice = 100;
    public int scepterPrice = 200;
    public TMP_Text axeText;
    public TMP_Text scepterText;
    public Button axeButton;
    public Button scepterButton;

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

    public void BuyAxe()
    {
        if (PlayerStats.instance.coins >= 100)
        {
            PlayerStats.instance.DecreaseCoins(100);
            PlayerStats.instance.weapon = PlayerStats.Weapon.Axe;
            axeText.text = "Bought";
            axeButton.interactable = false;
            PlayerStats.instance.UpdateAnimatorController();
            PlayerStats.instance.SetAttack(30);
        }
    }

    public void BuyScepter()
    {
        if (PlayerStats.instance.coins >= 200)
        {
            PlayerStats.instance.DecreaseCoins(200);
            PlayerStats.instance.weapon = PlayerStats.Weapon.Scepter;
            scepterText.text = "Bought";
            scepterButton.interactable = false;
            PlayerStats.instance.UpdateAnimatorController();
            PlayerStats.instance.SetAttack(35);
        }
    }
}