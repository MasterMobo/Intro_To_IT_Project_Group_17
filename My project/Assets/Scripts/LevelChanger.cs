using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    private LevelConnection _connection;

    [SerializeField]
    private string _targetScene;

    [SerializeField]
    private Transform _spawnPoint;

}
