using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCounter : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    int goldCount;
    public TextMeshProUGUI textElement;
    void Start()
    {
        textElement = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = player.inventory.gold.ToString();
    }
}
