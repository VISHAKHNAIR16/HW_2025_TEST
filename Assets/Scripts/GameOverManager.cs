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
                
                Debug.Log("GAME OVER! Final Score: " + finalScore);
                
                
                Time.timeScale = 0f;
            }
        }
        
        public bool IsGameOver()
        {
            return isGameOver;
        }
    }
}
