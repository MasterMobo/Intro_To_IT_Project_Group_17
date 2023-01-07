using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    private Color startColor;
    private  SpriteRenderer rend;

    private GameObject turret;
    public Vector3 positionOffset;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }
    void OnMouseEnter()
    {
        rend.material.SetColor("_Color", hoverColor);
    }
    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build here!");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
    void OnMouseExit()
    {
        rend.material.SetColor("_Color", startColor);
    }
}
