using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Gestore del respawn del player
/// </summary>
public class RespawnAgent : MonoBehaviour {

    List<AgentSpawn> AgentsSpawn = new List<AgentSpawn>();
    List<GameObject> AgentsPrefabs;

    private void Start()
    {
        AgentsPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<Agent>("Prefabs");
    }

    public void Respawn(PlayerIndex _playerIndex)
    {
        GameObject agent = SearchAgent(_playerIndex);
        Transform spawn = SearchSpawn(_playerIndex);
        Instantiate(agent, spawn.position, spawn.rotation);
    }

    GameObject SearchAgent(PlayerIndex _playerIndex)
    {
        foreach (GameObject item in AgentsPrefabs)
        {
            if(item.GetComponent<Agent>().playerIndex == _playerIndex)
            {
                return item;
            }
        }
        return null;
    }


    Transform SearchSpawn(PlayerIndex _playerIndex)
    {
        foreach (AgentSpawn item in AgentsSpawn)
        {
            if (item.playerIndex == _playerIndex)
            {
                return item.spawnPoint;
            }
        }
        return null;
    }

    public void SetSpawnPoint(PlayerIndex _playerIndex, Transform _spawnpoint)
    {
        GameObject SpawnPoint = new GameObject();
        SpawnPoint.transform.position = _spawnpoint.position;
        SpawnPoint.name = ("Spawnpoint" + _playerIndex);
        AgentsSpawn.Add(new AgentSpawn(_playerIndex, SpawnPoint.transform));
    }
}

public struct AgentSpawn{

    public PlayerIndex playerIndex;
    public Transform spawnPoint;

    public AgentSpawn(PlayerIndex _playerIndex, Transform _spawnpoint)
    {
        playerIndex = _playerIndex;
        spawnPoint = _spawnpoint;
    }
}
