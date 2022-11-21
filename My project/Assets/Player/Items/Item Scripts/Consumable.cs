using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    public Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public virtual void Use()
    {
        player.inventory.RemoveCurrent();
    }
}
