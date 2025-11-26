namespace DoofusGame
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;

    public class GameUIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        public GameObject startPanel;
        public GameObject endPanel;
        public GameObject introPanel;
        public GameObject gameHUD;

        [Header("Text Elements")]
        public TextMeshProUGUI finalScoreText;
        public TextMeshProUGUI narrationText;

        [Header("Cinematics")]
        public Animator cameraAnimator;
        public MonoBehaviour playerScript;

        [Header("Cinematic Settings")]
        public float typingSpeed = 0.05f;

        [Range(0.01f, 2.0f)]
        public float cinematicSpeed = 0.1f; // Slow motion speed

        public string animationClipName = "CameraIntroAnim";

        [Header("Timing Fix")]
        [Tooltip("Uncheck this to set the exact seconds yourself!")]
        public bool useAutomaticTiming = false;

        [Tooltip("How many seconds until the game starts?")]
        public float manualDuration = 6.0f;

        // Story Lines
        private string line1 = "Legends speak of a trial no one has conquered...";
        private string line2 = "Fifty floating platforms. One impossible journey.";
        private string line3 = "But Doofus fears nothing. The adventure begins NOW!";


        private CanvasGroup startPanelGroup;
        private CanvasGroup endPanelGroup;

        void Awake()
        {
            if (startPanel != null) startPanelGroup = startPanel.GetComponent<CanvasGroup>();
            if (endPanel != null) endPanelGroup = endPanel.GetComponent<CanvasGroup>();
        }

        void Start()
        {
            // 1. Initial State
            if (startPanel != null) startPanel.SetActive(true);
            if (endPanel != null) endPanel.SetActive(false);
            if (introPanel != null) introPanel.SetActive(false);
            if (gameHUD != null) gameHUD.SetActive(false);

            // 2. Pause Physics
            Time.timeScale = 0f;

            // 3. Prepare Camera
            if (cameraAnimator != null)
            {
                cameraAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
                cameraAnimator.enabled = false;
            }
        }

        public void OnStartPressed()
        {
            StartCoroutine(PlayIntroCinematic());
        }

        IEnumerator PlayIntroCinematic()
        {
            // --- PHASE 1: TEXT ---
            if (startPanel != null) startPanel.SetActive(false);
            if (introPanel != null) introPanel.SetActive(true);
            Time.timeScale = 0f;

            // Freeze Camera at Frame 0
            if (cameraAnimator != null)
            {
                cameraAnimator.enabled = true;
                cameraAnimator.Play(animationClipName, 0, 0f);
                cameraAnimator.speed = 0f;
            }

            // Play Narration
            if (narrationText != null)
            {
                narrationText.text = "";
                yield return StartCoroutine(TypeText(line1));
                yield return new WaitForSecondsRealtime(1.5f);

                narrationText.text = "";
                yield return StartCoroutine(TypeText(line2));
                yield return new WaitForSecondsRealtime(1.5f);

                narrationText.text = "";
                yield return StartCoroutine(TypeText(line3));
                yield return new WaitForSecondsRealtime(1.0f);

                narrationText.text = "";
            }

            // --- PHASE 2: MOVIE ---
            if (introPanel != null) introPanel.SetActive(false);

            if (cameraAnimator != null)
            {
                cameraAnimator.speed = cinematicSpeed;
                yield return null;

                float waitTime = 0f;

                if (useAutomaticTiming)
                {
                    // Old math way
                    float clipLength = cameraAnimator.GetCurrentAnimatorStateInfo(0).length;
                    waitTime = clipLength / cinematicSpeed;
                }
                else
                {
                    // NEW MANUAL WAY (Safe!)
                    waitTime = manualDuration;
                }

                Debug.Log($"Cinematic waiting for: {waitTime} seconds.");
                yield return new WaitForSecondsRealtime(waitTime);
            }

            // --- PHASE 3: GO! ---
            if (gameHUD != null) gameHUD.SetActive(true);
            if (cameraAnimator != null) cameraAnimator.enabled = false;
            if (playerScript != null) playerScript.enabled = true;
            Time.timeScale = 1f;
        }

        IEnumerator TypeText(string line)
        {
            foreach (char c in line.ToCharArray())
            {
                narrationText.text += c;
                yield return new WaitForSecondsRealtime(typingSpeed);
            }
        }

        public void ShowEndPanel(int score)
        {
            if (endPanel != null)
            {
                endPanel.SetActive(true);
                if (finalScoreText != null) finalScoreText.text = score.ToString();
                StartCoroutine(FadeIn(endPanelGroup));
            }
        }

        public void OnRestartPressed()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        IEnumerator FadeIn(CanvasGroup cg)
        {
            if (cg == null) yield break;
            cg.alpha = 0;
            cg.gameObject.SetActive(true);
            float alpha = 0;
            while (alpha < 1)
            {
                alpha += Time.unscaledDeltaTime * 2f;
                cg.alpha = alpha;
                yield return null;
            }
            cg.alpha = 1f;
        }
    }
}