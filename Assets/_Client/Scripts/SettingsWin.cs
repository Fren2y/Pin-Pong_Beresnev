using UnityEngine;
using UnityEngine.UI;

namespace PinPong
{
    public class SettingsWin : MonoBehaviour
    {
        [SerializeField] private Slider _senseSlider;
        [SerializeField] private Toggle[] _colorToggles;

        /// <summary>
        /// Hide win
        /// Update Toggles color
        /// </summary>
        private void Start()
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            gameObject.SetActive(false);

            for (int i = 0; i < _colorToggles.Length; i++)
            {
                _colorToggles[i].image.color = GameManager.instance.GetBallColor(i);
            }
        }

        /// <summary>
        /// Display Current Setting
        /// </summary>
        /// <param name="data"></param>
        public void LoadSettings(DataClass data)
        {
            for (int i = 0; i < _colorToggles.Length; i++)
            {
                if (i == data.ballColor)
                {
                    _colorToggles[i].isOn = true;
                    break;
                }
            }

            _senseSlider.value = data.sense;
        }

        /// <summary>
        /// Save Current Settings
        /// </summary>
        public void SaveSettings()
        {
            int selectedColor = 0;

            for (int i = 0; i < _colorToggles.Length; i++)
            {
                if (_colorToggles[i].isOn)
                {
                    selectedColor = i;
                    break;
                }
            }

            GameManager.instance.UpdateSettings(_senseSlider.value, selectedColor);
        }
    }
}
