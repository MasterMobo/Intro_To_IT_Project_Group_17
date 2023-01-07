using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    float t = 0;
    private void Start()
    {
        gameObject.SetActive(false);
        textElement.alpha = 0;
    }
    private void FixedUpdate()
    {

        textElement.alpha = t;
        t += Time.fixedDeltaTime;
        if (t > 1)
        {
            t = 1;
        }
    }
}
