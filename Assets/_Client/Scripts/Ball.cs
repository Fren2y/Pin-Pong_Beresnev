using UnityEngine;

namespace PinPong
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        internal Rigidbody _ballRB;
        internal BallData _ballSO;

        internal const float maxVelocity = 10;

        private void Awake()
        {
            _ballRB = GetComponent<Rigidbody>();
        }

        private void OnDestroy() => GameManager.instance.m_UpdateBallColor.RemoveListener(UpdateBallColor);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerBlock playerBlock))
            {
                //Add Player velocity power
                PushBall(playerBlock.transform.forward, Vector3.right * playerBlock.GetPlayer.GetPlayerVelocity());
                GameManager.instance.PlayerPunchBall();
                return;
            }

            if (other.TryGetComponent(out Goal goal))
            {
                DeactivateBall();
                return;
            }

            if (other.TryGetComponent(out Wall wall))
            {
                PushBall(wall.transform.forward, Vector3.zero);
                return;
            }
        }

        /// <summary>
        /// Fires when you need to push the ball away from an obstacle
        /// </summary>
        /// <param name="reflectVector">Normals</param>
        /// <param name="addForce"></param>
        public virtual void PushBall(Vector3 reflectVector, Vector3 addForce)
        {
            Vector3 curVelocity = Vector3.Reflect(_ballRB.velocity, reflectVector);

            curVelocity *= _ballSO.reboundSpeed;

            curVelocity += addForce;

            curVelocity.x = Mathf.Clamp(curVelocity.x, -maxVelocity, maxVelocity);
            curVelocity.y = 0;
            curVelocity.z = Mathf.Clamp(curVelocity.z, -maxVelocity, maxVelocity);

            _ballRB.velocity = curVelocity;
        }

        /// <summary>
        /// Load ball data
        /// Starts moving
        /// </summary>
        /// <param name="ballData"></param>
        public virtual void ActivateBall(BallData ballData)
        {
            _ballSO = ballData;

            _ballRB.AddForce(new Vector3(Random.Range(-_ballSO.moveSpeed, _ballSO.moveSpeed)/3, 0, Random.Range(0.0f, 1.0f) > 0.5f ? _ballSO.moveSpeed : -_ballSO.moveSpeed), ForceMode.VelocityChange);

            GameManager.instance.m_UpdateBallColor.AddListener(UpdateBallColor);

            UpdateBallObj();
        }

        public virtual void DeactivateBall()
        {
            Destroy(gameObject);
            GameManager.instance.EndGame();
            GameManager.instance.StartGame();
        }

        private void UpdateBallObj()
        {
            transform.localScale = Vector3.one * _ballSO.size;
            UpdateBallColor();
        }

        private void UpdateBallColor()
        {
            GetComponent<MeshRenderer>().material.SetColor("_Color", GameManager.instance.GetBallColor());
        }
    }
}
