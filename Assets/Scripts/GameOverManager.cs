namespace DoofusGame
{
    using UnityEngine;

    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance;

        private bool isGameOver = false;

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

        public void TriggerGameOver()
        {
            if (!isGameOver)
            {
                isGameOver = true;

                int finalScore = ScoreManager.Instance.GetScore();

                // Show EndPanel and score via UI Manager
                var ui = FindObjectOfType<GameUIManager>();
                if (ui != null)
                {
                    ui.ShowEndPanel(finalScore);
                }

                Time.timeScale = 0f; // Stop the game
                Debug.Log("GAME OVER! Final Score: " + finalScore);
            }
        }


        public bool IsGameOver()
        {
            return isGameOver;
        }
    }
}
