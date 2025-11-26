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
            if (GameSettingsLoader.Settings != null && GameSettingsLoader.Settings.pulpit_data != null)
            {
                minLifetime = GameSettingsLoader.Settings.pulpit_data.min_pulpit_destroy_time;
                maxLifetime = GameSettingsLoader.Settings.pulpit_data.max_pulpit_destroy_time;
            }
            lifetime = Random.Range(minLifetime, maxLifetime);
            destroyTime = Time.time + lifetime;
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
                // Play despawn sound before destroying
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayDespawnSound();
                }

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
