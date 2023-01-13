using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button buttonELement;
    float t = 0;

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
/*        buttonELement.alpha = 0;
        t += Time.deltaTime;
        if (t > 1)
        {
            t = 1;
        }*/
    }
}
