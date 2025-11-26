namespace DoofusGame
{
    using UnityEngine;
    using System.Collections.Generic;

    public class PulpitManager : MonoBehaviour
    {
        public GameObject pulpitPrefab;

        private List<GameObject> activePulpits = new List<GameObject>();
        private Vector3 lastPulpitPosition = Vector3.zero;

        void Start()
        {
            GameObject firstPulpit = Instantiate(pulpitPrefab, Vector3.zero, Quaternion.identity);

            Pulpit pulpitScript = firstPulpit.GetComponent<Pulpit>();
            if (pulpitScript != null)
            {
                pulpitScript.pulpitManager = this;
            }

            activePulpits.Add(firstPulpit);
            lastPulpitPosition = Vector3.zero;
        }

        void Update()
        {
            if (GameOverManager.Instance != null && GameOverManager.Instance.IsGameOver())
            {
                return;
            }


            RemoveDestroyedPulpits();
        }

        public void SpawnNextPulpit()
        {
            if (GameOverManager.Instance != null && GameOverManager.Instance.IsGameOver())
            {
                return;
            }

            Vector3 newPosition = GetRandomAdjacentPosition(lastPulpitPosition);
            GameObject newPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);

            Pulpit pulpitScript = newPulpit.GetComponent<Pulpit>();
            if (pulpitScript != null)
            {
                pulpitScript.pulpitManager = this;
            }

            activePulpits.Add(newPulpit);
            lastPulpitPosition = newPosition;
            Debug.Log("Spawned pulpit at: " + newPosition);

            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlaySpawnSound();
            }
        }


        Vector3 GetRandomAdjacentPosition(Vector3 currentPosition)
        {
            Vector3[] directions = new Vector3[]
            {
                new Vector3(9, 0, 0),
                new Vector3(-9, 0, 0),
                new Vector3(0, 0, 9),
                new Vector3(0, 0, -9)
            };

            int randomIndex = Random.Range(0, directions.Length);
            Vector3 randomDirection = directions[randomIndex];
            Vector3 newPosition = currentPosition + randomDirection;

            return newPosition;
        }

        void RemoveDestroyedPulpits()
        {
            activePulpits.RemoveAll(pulpit => pulpit == null);
        }
    }
}
