namespace DoofusGame
{
    using UnityEngine;

    public class FallDetector : MonoBehaviour
    {
        public float fallThreshold = -5f;  // Y position below which = game over
        
        void Update()
        {
            if (transform.position.y < fallThreshold)
            {
                if (GameOverManager.Instance != null)
                {
                    GameOverManager.Instance.TriggerGameOver();
                }
            }
        }
    }
}
