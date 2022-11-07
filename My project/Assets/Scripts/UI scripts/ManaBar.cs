using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    Slider healthSlider;
    Player player;
    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        healthSlider.maxValue = player.maxMana;
        healthSlider.value = healthSlider.maxValue;
    }

    private void Update()
    {
        healthSlider.value = player.currentMana;
    }
}
