namespace DoofusGame
{
    using UnityEngine;
    using TMPro;

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        public TextMeshProUGUI scoreText;

        private int score = 0;

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
        }

        void Start()
        {
            UpdateScoreUI();
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
        }

        public int GetScore()
        {
            return score;
        }

        public void ResetScore()
        {
            score = 0;
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
