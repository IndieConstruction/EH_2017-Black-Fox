using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class BlackHoleSpawner : SpawnerBase
    {
        Vector3 randomPos;
        int BlackHoleSpawned = 0;

        new public BlackHoleSpawnerOptions Options;

        float Timer;
        State _currentState;

        public State CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
            }
        }

        public enum State
        {
            Timer,
            Spawn,
            Stop,
        }
        void Start()
        {
            Timer = Options.TimerToSpawn;
            CurrentState = State.Timer;
        }

        public override SpawnerBase OptionInit(SpawnerOptions options) {
            Options = options as BlackHoleSpawnerOptions;
            return this;
        }


        void Update()
        {
            switch (CurrentState)
            {
                case State.Timer:
                    Timer -= Time.deltaTime;
                    if (Timer <= 0 && BlackHoleSpawned <= Options.BlackHoleToSpawn)
                    {
                        if (BlackHoleSpawned == Options.BlackHoleToSpawn)
                        {
                            CurrentState = State.Stop;
                        }
                        else
                        {
                            CurrentState = State.Spawn;
                        }
                    }
                    
                    break;

                case State.Spawn:

                    SpawnBlackHole();
                    BlackHoleSpawned++;
                    Timer = Options.TimerToSpawn;
                    CurrentState = State.Timer;

                    break;

                case State.Stop:
                    enabled = false;
                    break;

                default:
                    break;
            }
        }


        /// <summary>
        /// Istanzia il buco nero in una posizione randomica
        /// </summary>
        void SpawnBlackHole()
        {
            randomPos = new Vector3(Random.Range(Options.minRandomX, Options.maxRandomX), 0, Random.Range(Options.minRandomZ, Options.maxRandomZ));
            Instantiate(Options.BlackHolePrefab, randomPos, Quaternion.identity);

        }
    }

    [System.Serializable]
    public class BlackHoleSpawnerOptions : SpawnerOptions {

        public float minRandomX;
        public float maxRandomX;
        public float minRandomZ;
        public float maxRandomZ;
        public GameObject BlackHolePrefab;
        public int BlackHoleToSpawn = 3;
        public float TimerToSpawn = 10;
    }
}