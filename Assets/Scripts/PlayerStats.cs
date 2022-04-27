using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(0,4)]
    public int defence = 0;

    public enum Weapon {Sword, Axe, Scepter};
    [HideInInspector] public Weapon weapon;

    void Start()
    {
        weapon = Weapon.Sword;
    }

    void Update()
    {
        
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

}
