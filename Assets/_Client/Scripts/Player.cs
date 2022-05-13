using UnityEngine;

namespace PinPong
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Transform _playerTransform;
        private Rigidbody _playerRb;

        private float moveVelocity = 0;

        private float leftPos = 0;
        private float rightPos = 0;

        private void Awake()
        {
            _playerTransform = transform;
            _playerRb = GetComponent<Rigidbody>();

            #region Determination of zone boundaries
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.right * 100, out hit))
            {
                Debug.DrawLine(transform.position, -transform.right * 100, Color.red);
                leftPos = hit.point.x + transform.localScale.x / 2;
            }

            if (Physics.Raycast(transform.position, transform.right * 100, out hit))
            {
                Debug.DrawLine(transform.position, transform.right * 100, Color.red);
                rightPos = hit.point.x - transform.localScale.x / 2;
            }
            #endregion
        }

        /// <summary>
        /// Move player with constraints
        /// </summary>
        /// <param name="dir"></param>
        public void MovePlayer(Vector3 dir)
        {
            if (dir.x != 0)
            {
                moveVelocity = dir.x;

                Vector3 pos = _playerRb.position;
                pos += dir;

                if (pos.x > rightPos) pos.x = rightPos;
                if (pos.x < leftPos)  pos.x = leftPos;

                _playerRb.position = pos;
            }
            else
            {
                moveVelocity = 0;
            }
        }

        public float GetPlayerVelocity()
        {
            return moveVelocity;
        }
    }
}
