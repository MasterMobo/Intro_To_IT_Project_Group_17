using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Consumable
{
    [SerializeField]
    float healValue = 25f;


    public override void Use()
    {
        base.Use();
        player.currentHealth += healValue;
        Destroy(gameObject);
    }
}
