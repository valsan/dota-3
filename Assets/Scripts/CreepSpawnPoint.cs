using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSpawnPoint : MonoBehaviour
{
    [SerializeField] private Team team;
    [SerializeField] private List<Transform> _wayPoints;

    [SerializeField] private CreepAI _creepPrefab;


    public void SpawnLane()
    {
        CreepAI creepAI = Instantiate(_creepPrefab, transform.position, transform.rotation);
        creepAI.WayPoints = _wayPoints;
    }
}
