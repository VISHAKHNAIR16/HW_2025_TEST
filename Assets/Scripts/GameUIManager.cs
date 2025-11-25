namespace DoofusGame
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class GameUIManager : MonoBehaviour
    {
        public GameObject startPanel;
        public GameObject endPanel;
        public TextMeshProUGUI finalScoreText;

        public void OnStartPressed()
        {
            startPanel.SetActive(false);
            Time.timeScale = 1f;
        }

       
        public void ShowEndPanel(int score)
        {
            endPanel.SetActive(true);
            finalScoreText.text = score.ToString();
        }

        
        public void OnRestartPressed()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        void Start()
        {
            startPanel.SetActive(true);
            endPanel.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
