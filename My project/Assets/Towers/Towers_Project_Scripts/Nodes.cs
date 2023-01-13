using UnityEngine;

public class Nodes : MonoBehaviour
{   
    public int goldCost;
    public Color hoverColor;
    private Color startColor;
    private  SpriteRenderer rend;

    private GameObject turret;
    public Vector3 positionOffset;
    Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }
    void OnMouseEnter()
    {
        rend.material.SetColor("_Color", hoverColor);
    }
    void OnMouseDown()
    {
        if (turret != null || player.inventory.gold < goldCost)
        {
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        player.inventory.gold -= goldCost;
    }
    void OnMouseExit()
    {
        rend.material.SetColor("_Color", startColor);
    }
}
