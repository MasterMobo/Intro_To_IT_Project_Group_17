using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBase : MonoBehaviour
{
    Player player;
    public int index;
    Image image;
    Vector3 orgPos;
    float t;
    void Start()
    {
        image = GetComponent<Image>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        image.preserveAspect = true;
        orgPos = transform.localPosition;


    }

    private void FixedUpdate()
    {
        if (player.inventory.selectedItemIndex == index)
        {
            
            image.transform.localPosition = Vector3.Lerp(image.transform.localPosition, new Vector3(orgPos.x, orgPos.y + 10, orgPos.z), t);
            t += Time.deltaTime * 1.2f;
        }
        else
        {
            image.transform.localPosition = orgPos;
            t = 0;
        }
    }

}
