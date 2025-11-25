namespace DoofusGame
{
    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance; 
        
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
        
        public void IncrementScore()
        {
            score++;
            Debug.Log("Score: " + score);
        }
        
        public int GetScore()
        {
            return score;
        }
        
        public void ResetScore()
        {
            score = 0;
        }
    }
}
