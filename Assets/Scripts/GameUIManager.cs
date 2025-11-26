namespace DoofusGame
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;
    using System.Collections;

    public class GameUIManager : MonoBehaviour
    {
        
        public static GameUIManager Instance;

        [Header("UI Panels")]
        public GameObject startPanel;
        public GameObject endPanel;
        public GameObject introPanel;          
        public GameObject gameHUD;             
        
        [Header("Text Elements")]
        public TextMeshProUGUI finalScoreText;
        public TextMeshProUGUI narrationText;  
        public TextMeshProUGUI levelText;      

        [Header("Cinematics")]
        public Animator cameraAnimator;        
        public MonoBehaviour playerScript;     

        [Header("Cinematic Settings")]
        public float typingSpeed = 0.05f;
        
        [Tooltip("The speed of the animation. 0.1 = Super Slow Motion.")]
        [Range(0.001f, 2.0f)]
        public float cinematicSpeed = 0.1f; 

        public string animationClipName = "CameraIntroAnim"; 

        [Header("Timing Control")]
        public bool useManualDuration = true; 
        public float manualDuration = 6.0f; 

        
        private string line1 = "In a world of forgotten legends...";
        private string line2 = "The Pulpit Gauntlet awaits.";
        private string line3 = "Ready or not... HERE I COME!";

        private CanvasGroup startPanelGroup;
        private CanvasGroup endPanelGroup;

        void Awake()
        {
            
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            if(startPanel != null) startPanelGroup = startPanel.GetComponent<CanvasGroup>();
            if(endPanel != null) endPanelGroup = endPanel.GetComponent<CanvasGroup>();
        }

        void Start()
        {
            if(startPanel != null) startPanel.SetActive(true);
            if(endPanel != null) endPanel.SetActive(false);
            if(introPanel != null) introPanel.SetActive(false);
            if(gameHUD != null) gameHUD.SetActive(false);
            if(levelText != null) levelText.gameObject.SetActive(false); 

            Time.timeScale = 0f;

            if(cameraAnimator != null) 
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
            
            if(startPanel != null) startPanel.SetActive(false);
            if(introPanel != null) introPanel.SetActive(true); 
            
            if(cameraAnimator != null)
            {
                cameraAnimator.enabled = true;
                cameraAnimator.Play(animationClipName, 0, 0f); 
                cameraAnimator.speed = 0f; 
            }

            if(narrationText != null)
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

            
            if(introPanel != null) introPanel.SetActive(false); 

            if(cameraAnimator != null)
            {
                cameraAnimator.speed = cinematicSpeed; 
                
                float waitTime = 0f;
                if (useManualDuration) waitTime = manualDuration;
                else
                {
                    yield return null; 
                    float clipLength = cameraAnimator.GetCurrentAnimatorStateInfo(0).length;
                    waitTime = clipLength / cinematicSpeed;
                }

                yield return new WaitForSecondsRealtime(waitTime);
            }

            
            if(gameHUD != null) gameHUD.SetActive(true);
            if(cameraAnimator != null) cameraAnimator.enabled = false; 
            if(playerScript != null) playerScript.enabled = true;     
            Time.timeScale = 1f; 

            
            ShowLevelUp(1);
        }

        
        public void ShowLevelUp(int level)
        {
            if(levelText != null)
            {
                StartCoroutine(LevelTextRoutine(level));
            }
        }

        IEnumerator LevelTextRoutine(int level)
        {
            levelText.text = "LEVEL " + level;
            levelText.gameObject.SetActive(true);
            
            
            yield return new WaitForSeconds(3.0f);
            
            levelText.gameObject.SetActive(false);
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
            if(endPanel != null)
            {
                endPanel.SetActive(true);
                if(finalScoreText != null) finalScoreText.text = score.ToString();
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
            if(cg == null) yield break;
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