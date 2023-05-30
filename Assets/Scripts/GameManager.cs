using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CreepSpawnPoint> _spawnPoints;
    [SerializeField] private float _laneSpawnInterval;

    [Header("Debug")]
    [SerializeField] private bool _spawnCreepsEnabled;

    private void Start()
    {
        StartCoroutine(SpawnCreeps());
    }

    IEnumerator SpawnCreeps()
    {
        while (true)
        {
            if (_spawnCreepsEnabled)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    spawnPoint.SpawnLane();
                }
            }

            yield return new WaitForSeconds(_laneSpawnInterval);
        }
    }

}
