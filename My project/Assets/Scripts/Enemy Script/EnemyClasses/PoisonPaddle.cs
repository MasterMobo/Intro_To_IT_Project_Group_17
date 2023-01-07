using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPaddle : MonoBehaviour {

   
   public float damage;
   public float poisonKnockbackForce;
   public GameObject hitFX;
   public float appear_time;
   private Transform player;

   
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
       

       if (appear_time <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            appear_time -= Time.deltaTime;
        }
    }
}
