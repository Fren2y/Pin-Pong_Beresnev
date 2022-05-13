using UnityEngine;

namespace PinPong
{
    public class PlayerBlock : MonoBehaviour
    {
        private Player _player;
        public Player GetPlayer { get => _player; private set => _player = value; }

        private void Awake()
        {
            GetPlayer = transform.GetComponentInParent<Player>();
        }
    }
}
