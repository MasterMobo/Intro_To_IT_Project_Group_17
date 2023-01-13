using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    void Awake ()
    {
        instance = this;
    }
    public GameObject defaultTurretPrefab;
    void Start()
    {
        turretToBuild = defaultTurretPrefab;
    }
    private GameObject turretToBuild;
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}