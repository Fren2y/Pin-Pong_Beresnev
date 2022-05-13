using UnityEngine;

namespace PinPong
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Transform _ballSpawnPoint;
        private Ball _currentBall;

        private void OnEnable() => GameManager.instance.m_StartGame.AddListener(SpawnBall);
        private void OnDisable() => GameManager.instance.m_StartGame.RemoveListener(SpawnBall);

        private void SpawnBall()
        {
            _currentBall = GameManager.instance.GetRandomBall();
        }
    }
}
