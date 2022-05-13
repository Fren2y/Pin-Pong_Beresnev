using UnityEngine;

namespace PinPong
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private BallData[] _allBalls;
        [SerializeField]
        private Ball _ballObj;
        [SerializeField]
        private Ball _extraBallObj;

        /// <summary>
        /// Get random ball from list
        /// </summary>
        /// <param name="secretBall">?</param>
        /// <returns>Return configured ball</returns>
        public Ball GetRandomBall(bool secretBall = false)
        {
            Ball ball = Instantiate(secretBall ? _extraBallObj : _ballObj);
            ball.ActivateBall(_allBalls[Random.Range(0, _allBalls.Length)]);
            return ball;
        }
    }
}
