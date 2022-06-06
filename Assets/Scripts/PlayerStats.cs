using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;

    public int coins = 0;

    [Range(0,4)]
    public int defence = 0;

    public float stunChance = 0.3f;

    [HideInInspector] public int attackDamage = 20;

    public enum Weapon {Sword, Axe, Scepter};
    [HideInInspector] public Weapon weapon;

    Animator anim;

    void Awake() {
        instance = this;
        anim = GetComponent<Animator>();
    }


    void Start()
    {
        weapon = Weapon.Sword;
    }

    void Update()
    {
        
    }

    public void SetAttack(int newAttack)
    {
        attackDamage = newAttack;
    }

    public void IncreaseDefence()
    {
        if (defence >= 4)
            return;
        defence++;
    }

    public void DecreaseDefence()
    {
        if (defence <= 0)
            return;
        defence--;
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }

    public void DecreaseCoins(int coinsToAdd)
    {
        coins -= coinsToAdd;
        if (coins <= 0)
            coins = 0;
    }

    public void UpdateAnimatorController()
    {
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/Player/Player_" + weapon + "/Player_" + weapon + "_Defence" + defence);
    }

}
