namespace DoofusGame
{
    using UnityEngine;
    using TMPro;

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public TextMeshProUGUI scoreText;

        private int score = 0;
        
        private int currentLevel = 1;
        
        private int difficultyStep = 10; 
        private float difficultyMultiplier = 0.9f; 
        private float minClampTime = 1.5f; 

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            UpdateScoreUI();
            currentLevel = 1; 
        }

        public void IncrementScore()
        {
            score++;
            Debug.Log("Score: " + score);
            UpdateScoreUI();

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayScoreSound();
            }

            
            if (score > 0 && score % difficultyStep == 0)
            {
                IncreaseDifficulty();
            }
        }

        void IncreaseDifficulty()
        {
            
            currentLevel++;

            
            if(GameUIManager.Instance != null)
            {
                GameUIManager.Instance.ShowLevelUp(currentLevel);
            }

            
            if (GameSettingsLoader.Settings != null && GameSettingsLoader.Settings.pulpit_data != null)
            {
                var data = GameSettingsLoader.Settings.pulpit_data;
                data.min_pulpit_destroy_time *= difficultyMultiplier;
                data.max_pulpit_destroy_time *= difficultyMultiplier;
                data.min_pulpit_destroy_time = Mathf.Max(data.min_pulpit_destroy_time, minClampTime);
                data.max_pulpit_destroy_time = Mathf.Max(data.max_pulpit_destroy_time, minClampTime + 0.5f);
            }
        }

        public int GetScore()
        {
            return score;
        }

        public void ResetScore()
        {
            score = 0;
            currentLevel = 1;
            UpdateScoreUI();
        }

        void UpdateScoreUI()
        {
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
    }
}