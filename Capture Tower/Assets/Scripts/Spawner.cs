using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Which entity to spawn.
    public GameObject SpawnPrefab;

    // Where to spawn the entity.
    public GameObject SpawnWaypoint;
    
    // When this game object is activated, it spawns the entity at the specified waypoint.
    public void OnEnable()
    {
        if (SpawnPrefab == null)
        {
            Debug.LogError(string.Format("Spawner: {0} has no spawn prefab assigned!", this.name));
            return;
        }
        if (SpawnWaypoint == null)
        {
            Debug.LogError(string.Format("Spawner: {0} has no spawn waypoint assigned!", this.name));
            return;
        }

        Vector3 position = SpawnWaypoint.transform.position;
        GameObject.Instantiate<GameObject>(SpawnPrefab, position, Quaternion.identity);
    }
}
