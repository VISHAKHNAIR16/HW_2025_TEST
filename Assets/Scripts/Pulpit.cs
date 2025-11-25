namespace DoofusGame
{
    using UnityEngine;
    using TMPro;


    public class Pulpit : MonoBehaviour
    {
        public float minLifetime = 3f;
        public float maxLifetime = 5f;

        private float destroyTime;
        private float lifetime;
        private bool hasSpawnedNext = false;
        private bool hasBeenSteppedOn = false;

        public PulpitManager pulpitManager;

        public TextMeshProUGUI timerText;


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


            // Update timer UI
            if (timerText != null)
            {
                float timeLeft = Mathf.Max(0, destroyTime - Time.time);
                timerText.text = timeLeft.ToString("F2");  // Shows 2 decimal places
            }

        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player" && !hasBeenSteppedOn)
            {
                hasBeenSteppedOn = true;

                if (ScoreManager.Instance != null)
                {
                    ScoreManager.Instance.IncrementScore();
                }

                Debug.Log("Doofus stepped on a new Pulpit!");
            }
        }
    }
}
