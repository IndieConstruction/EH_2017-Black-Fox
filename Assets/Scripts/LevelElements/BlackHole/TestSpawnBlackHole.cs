using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnBlackHole : MonoBehaviour {

    public float minRandomX;
    public float maxRandomX;
    public float minRandomZ;
    public float maxRandomZ;
    public GameObject BlackHolePrefab;
    Vector3 randomPos;
    public int BlackHoleToSpawn = 3;
    int BlackHoleSpawned = 0;

    public float TimerToSpawn = 10;
    float Timer;
    State _currentState;
    
    public State CurrentState
    {
        get { return _currentState; }
        set {
            _currentState = value;
        }
    }

    public enum State
    {
        Timer,
        Spawn,
        Stop,
    }

	// Use this for initialization
	void Start () {
        Timer = TimerToSpawn;
        CurrentState = State.Timer;
    }
	
	// Update is called once per frame
	void Update () {
        switch (CurrentState)
        {
            case State.Timer:
                Timer -= Time.deltaTime;
                if (Timer <= 0 && BlackHoleSpawned <= BlackHoleToSpawn)
                {
                    CurrentState = State.Spawn;
                }
                if (BlackHoleSpawned == BlackHoleToSpawn)
                {
                    CurrentState = State.Stop;
                }
                break;

            case State.Spawn:
                SpawnBlackHole();
                BlackHoleSpawned++;
                Timer = TimerToSpawn;
                CurrentState = State.Timer;
                break;
            case State.Stop:
                enabled = false;
                break;
            default:
                break;
        }
    }

    

    void SpawnBlackHole()
    {
        randomPos = new Vector3(Random.Range(minRandomX, maxRandomX), 0, Random.Range(minRandomZ, maxRandomZ));
        Instantiate(BlackHolePrefab, randomPos, Quaternion.identity);
        
    }
}