using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CreepSpawnPoint> _spawnPoints;
    [SerializeField] private float _laneSpawnInterval;

    [SerializeField] private Character _radiantTower;
    [SerializeField] private Character _direTower;

    [Header("Debug")]
    [SerializeField] private bool _spawnCreepsEnabled;

    private void Awake()
    {
        _radiantTower.OnDeath += GameOver;
        _direTower.OnDeath += GameOver;
    }

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


    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
