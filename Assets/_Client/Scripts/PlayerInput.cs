using UnityEngine;

namespace PinPong
{
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour
    {
        private Player _player;
        private float _sensetivity;
        private Vector3 _oldPos;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void Start()
        {
            GameManager.instance.m_StartGame.AddListener(StartPlay);
        }

        private void OnDestroy()
        {
            GameManager.instance.m_StartGame.RemoveListener(StartPlay);
        }

        private void StartPlay()
        {
            _sensetivity = GameManager.instance.GetSettings().sense;
        }

        private void FixedUpdate()
        {
            Controlls();
        }

        /// <summary>
        /// Player Input
        /// </summary>
        private void Controlls()
        {
#if UNITY_EDITOR
            Vector3 dir = Input.mousePosition - _oldPos;
            _player.MovePlayer(dir * _sensetivity * 0.1f);
            _oldPos = Input.mousePosition;

#elif UNITY_ANDROID || UNITY_IPHONE
            if (Input.touches.Length > 0)
            {
                _player.MovePlayer(Input.GetTouch(0).deltaPosition * _sensetivity);
            }
#endif
        }
    }
}
