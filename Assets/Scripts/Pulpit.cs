namespace DoofusGame
{
    using UnityEngine;
    using TMPro;


    public class Pulpit : MonoBehaviour
    {
        [Header("Animation Settings")]
        public float spawnAnimationDuration = 0.3f;
        private float spawnStartTime;
        private bool isSpawning = true;

        private Vector3 originalScale;

        public float despawnAnimationDuration = 0.5f;
        private bool isDespawning = false;
        private float despawnStartTime;
        private Material pulpitMaterial;
        private Color originalColor;

        public float minLifetime = 3f;
        public float maxLifetime = 5f;

        private float destroyTime;
        private float lifetime;
        private bool hasSpawnedNext = false;
        private bool hasBeenSteppedOn = false;

        public PulpitManager pulpitManager;

        public TextMeshProUGUI timerText;


        private Vector3 pulpitOriginalPosition;


        void Start()
        {
            originalScale = transform.localScale;
            spawnStartTime = Time.time;
            pulpitOriginalPosition = transform.position;



            if (GameSettingsLoader.Settings != null && GameSettingsLoader.Settings.pulpit_data != null)
            {
                minLifetime = GameSettingsLoader.Settings.pulpit_data.min_pulpit_destroy_time;
                maxLifetime = GameSettingsLoader.Settings.pulpit_data.max_pulpit_destroy_time;
            }
            lifetime = Random.Range(minLifetime, maxLifetime);
            destroyTime = Time.time + lifetime;


            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                pulpitMaterial = renderer.material;
                originalColor = pulpitMaterial.color;
            }
        }


        void Update()
        {

            if (isSpawning)
            {
                float elapsed = Time.time - spawnStartTime;
                float progress = Mathf.Clamp01(elapsed / spawnAnimationDuration);


                transform.localScale = originalScale * progress;

                if (progress >= 1f)
                {
                    isSpawning = false;
                    transform.localScale = originalScale;
                }
            }

            if (GameOverManager.Instance != null && GameOverManager.Instance.IsGameOver())
            {
                return;
            }

            float spawnTriggerTime = destroyTime - (lifetime * 0.5f);

            if (!hasSpawnedNext && Time.time >= spawnTriggerTime)
            {
                hasSpawnedNext = true;
                if (pulpitManager != null)
                {
                    pulpitManager.SpawnNextPulpit();
                }
            }

            
            if (Time.time >= destroyTime && !isDespawning)
            {
                isDespawning = true;
                despawnStartTime = Time.time;

                
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayDespawnSound();
                }
            }

                        
            if (!isDespawning && (destroyTime - Time.time < 0.4f))
            {
                float shakeAmount = 0.10f;
                float shakeFrequency = 60f;
                Vector3 shakeOffset = new Vector3(
                    Mathf.Sin(Time.time * shakeFrequency) * shakeAmount,
                    0,
                    Mathf.Cos(Time.time * shakeFrequency) * shakeAmount);
                transform.position = pulpitOriginalPosition + shakeOffset;
            }
            else if (!isDespawning)
            {
                transform.position = pulpitOriginalPosition;
            }


            
            if (isDespawning)
            {
                float elapsed = Time.time - despawnStartTime;
                float progress = Mathf.Clamp01(elapsed / despawnAnimationDuration);

                // Fade out and sink down
                Vector3 sinkPosition = pulpitOriginalPosition;
                sinkPosition.y -= progress * 3f; // sink by 1 unit over time
                transform.position = sinkPosition;

                if (pulpitMaterial != null)
                {
                    Color fadeColor = originalColor;
                    fadeColor.a = 1f - progress; // Fade out
                    pulpitMaterial.color = fadeColor;
                }

                // Destroy at end of animation
                if (progress >= 1f)
                {
                    Destroy(gameObject);
                }
            }




            if (timerText != null)
            {
                float timeLeft = Mathf.Max(0, destroyTime - Time.time);
                timerText.text = timeLeft.ToString("F2");

                float lifePercentage = timeLeft / lifetime;

                if (lifePercentage > 0.5f)
                {
                    timerText.color = Color.white;
                }
                else if (lifePercentage > 0.25f)
                {
                    timerText.color = Color.yellow;
                }
                else
                {
                    timerText.color = Color.red;
                }
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
