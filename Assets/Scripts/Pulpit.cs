namespace DoofusGame
{
    using UnityEngine;

    public class Pulpit : MonoBehaviour
    {
        public float minLifetime = 3f;
        public float maxLifetime = 5f;
        
        private float destroyTime;
        private float lifetime;
        private bool hasSpawnedNext = false;
        
        public PulpitManager pulpitManager;

        void Start()
        {
            lifetime = Random.Range(minLifetime, maxLifetime);
            destroyTime = Time.time + lifetime;
            
            Debug.Log("Pulpit will disappear in " + lifetime + " seconds");
        }

        void Update()
        {
            
            float spawnTriggerTime = destroyTime - (lifetime * 0.5f);
            
            
            if (!hasSpawnedNext && Time.time >= spawnTriggerTime)
            {
                hasSpawnedNext = true;
                if (pulpitManager != null)
                {
                    pulpitManager.SpawnNextPulpit();
                }
            }
            
           
            if (Time.time >= destroyTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
