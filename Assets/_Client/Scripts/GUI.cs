using UnityEngine;
using UnityEngine.UI;

namespace PinPong
{
    public class GUI : MonoBehaviour
    {
        [SerializeField] private SettingsWin settingsWin;
        [SerializeField] private Text scoreText;
        [SerializeField] private Text bestScoreText;

        private void Awake()
        {
        }
        private void Start()
        {
            GameManager.instance.m_StartGame.AddListener(UpdateBestScore);
            GameManager.instance.m_PlayerHitCount.AddListener(UpdateScore);          
        }
        private void OnDestroy()
        {
            GameManager.instance.m_PlayerHitCount.RemoveListener(UpdateScore);
            GameManager.instance.m_StartGame.RemoveListener(UpdateBestScore);
        }

        private void UpdateScore(int value)
        {
            scoreText.text = "Score: " + value.ToString();
        }

        private void UpdateBestScore()
        {
            bestScoreText.text = "Best Score: " + GameManager.instance.GetSettings().bestScore.ToString();
            scoreText.text = "Score: 0";
        }

        /// <summary>
        /// Open/close settings win from gui btn
        /// </summary>
        /// <param name="value">true = open / false = close</param>
        public void OpenSettingsBtn(bool value)
        {
            if (value)
            {
                settingsWin.LoadSettings(GameManager.instance.GetSettings());
            }
            else
            {
                settingsWin.SaveSettings();
            }

            Time.timeScale = value ? 0 : 1;
        }  
    }
}
