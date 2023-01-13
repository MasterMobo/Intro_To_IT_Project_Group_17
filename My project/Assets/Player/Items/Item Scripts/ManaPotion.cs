using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Consumable
{
    [SerializeField]
    float manaValue = 25f;

    public override void Use()
    {
        base.Use();
        player.playerState.currentMana += manaValue;
        Destroy(gameObject);
    }
}
