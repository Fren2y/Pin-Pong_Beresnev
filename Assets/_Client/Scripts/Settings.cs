using UnityEngine;

namespace PinPong
{
    public class Settings : MonoBehaviour
    {
        [SerializeField]
        private float _inputSense = 1.0f;

        public float GetControllerSence { get => _inputSense; private set => _inputSense = value; }

        [SerializeField]
        private Color[] _ballColors;

        /// <summary>
        /// Get color from arrow
        /// </summary>
        /// <param name="num">color num</param>
        /// <returns></returns>
        public Color GetBallColor(int num)
        {
            return _ballColors[num];
        }
    }
}
