using UnityEngine;
using UnityEngine.Events;

namespace PinPong
{
    [System.Serializable]
    public class MyIntEvent : UnityEvent<int>
    {
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        public UnityEvent m_StartGame;
        public UnityEvent m_EndGame;
        public UnityEvent m_UpdateBallColor;
        public MyIntEvent m_PlayerHitCount;
        public MyIntEvent m_UpdateScore;

        [SerializeField]
        private Spawner mySpawner;
        [SerializeField]
        private Settings mySettings;
        [SerializeField]
        private Data myData;

        private int _score;

        void Awake()
        {
            #region Init Singleton
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }
            #endregion

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Launches the game when loading
        /// </summary>
        private void Start()
        {
            myData.LoadData((r) =>
            {
                StartGame();
            });
        }

        /// <summary>
        /// Use for spawn ball and start game
        /// </summary>
        public void StartGame()
        {    
            m_StartGame?.Invoke();
        }

        /// <summary>
        /// Used when ball destroyed
        /// </summary>
        public void EndGame()
        {
            myData.UpdateScore(_score);
            _score = 0;
        }

        /// <summary>
        /// Fires when a player deflects the ball
        /// </summary>
        public void PlayerPunchBall()
        {
            m_PlayerHitCount?.Invoke(++_score);
        }

        public void UpdateScore(int score)
        {
            m_UpdateScore?.Invoke(score);
        }

        public Ball GetRandomBall()
        {
            return mySpawner.GetRandomBall();
        }

        public Color GetBallColor()
        {
            return mySettings.GetBallColor(myData.GetData.ballColor);
        }

        public Color GetBallColor(int id)
        {
            return mySettings.GetBallColor(id);
        }

        public DataClass GetSettings()
        {
            return myData.GetData;
        }

        public void UpdateSettings(float sense, int color)
        {
            myData.UpdateSettings(sense, color);
            m_UpdateBallColor?.Invoke();
        }
    }
}
