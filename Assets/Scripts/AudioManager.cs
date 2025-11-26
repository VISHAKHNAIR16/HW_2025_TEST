namespace DoofusGame
{
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Sound Effects")]
        public AudioClip stepSound;      
        public AudioClip scoreSound;     
        public AudioClip spawnSound;     
        public AudioClip despawnSound;   
        public AudioClip gameOverSound;  
        
        [Header("Music")]
        public AudioClip backgroundMusic;
        
        private AudioSource sfxSource;
        private AudioSource musicSource;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            
            musicSource.loop = true;
            musicSource.volume = 0.6f;
        }

        void Start()
        {
            PlayMusic();
        }

        public void PlayStepSound()
        {
            if (stepSound != null)
                sfxSource.PlayOneShot(stepSound, 0.5f);
        }

        public void PlayScoreSound()
        {
            if (scoreSound != null)
                sfxSource.PlayOneShot(scoreSound, 0.7f);
        }

        public void PlaySpawnSound()
        {
            if (spawnSound != null)
                sfxSource.PlayOneShot(spawnSound, 0.4f);
        }

        public void PlayDespawnSound()
        {
            if (despawnSound != null)
                sfxSource.PlayOneShot(despawnSound, 0.3f);
        }

        public void PlayGameOverSound()
        {
            if (gameOverSound != null)
                sfxSource.PlayOneShot(gameOverSound);
        }

        void PlayMusic()
        {
            if (backgroundMusic != null)
            {
                musicSource.clip = backgroundMusic;
                musicSource.Play();
            }
        }
    }
}
