using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(0,4)]
    public int defence = 0;

    void Start()
    {
        
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

}
