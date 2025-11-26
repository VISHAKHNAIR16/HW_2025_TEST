namespace DoofusGame
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;

    public class GameUIManager : MonoBehaviour
    {
        public GameObject startPanel;
        public GameObject endPanel;
        public TextMeshProUGUI finalScoreText;

        private CanvasGroup startPanelGroup;
        private CanvasGroup endPanelGroup;

        void Awake()
        {
            startPanelGroup = startPanel.GetComponent<CanvasGroup>();
            endPanelGroup = endPanel.GetComponent<CanvasGroup>();
        }

        public void OnStartPressed()
        {
            StartCoroutine(FadeOut(startPanelGroup));
            Time.timeScale = 1f;
        }

        public void ShowEndPanel(int score)
        {
            endPanel.SetActive(true); // Ensure active before fading in
            finalScoreText.text = score.ToString();
            StartCoroutine(FadeIn(endPanelGroup));
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
            startPanelGroup.alpha = 1f;
            endPanelGroup.alpha = 0f;
            Time.timeScale = 0f;
        }

        IEnumerator FadeIn(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.gameObject.SetActive(true);
            while (cg.alpha < 1)
            {
                cg.alpha += Time.unscaledDeltaTime * 2f; // speed: 0.5 seconds
                yield return null;
            }
            cg.alpha = 1f;
        }

        IEnumerator FadeOut(CanvasGroup cg)
        {
            while (cg.alpha > 0)
            {
                cg.alpha -= Time.unscaledDeltaTime * 2f;
                yield return null;
            }
            cg.alpha = 0f;
            cg.gameObject.SetActive(false);
        }
    }
}
