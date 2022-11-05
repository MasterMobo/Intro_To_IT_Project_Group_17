using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    public GameObject source; // Remember to add manually
    void Dissapate()
    {
        Destroy(gameObject);
    }
}
