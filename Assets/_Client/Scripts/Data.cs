using UnityEngine;
using System;

namespace PinPong
{
    [Serializable] public class DataClass
    {
        public int bestScore = 0;
        public int ballColor = 0;
        public float sense = 0.5f;
    }

    public class Data : MonoBehaviour
    {
        private DataClass _myData;

        public DataClass GetData { get => _myData; private set => _myData = value; }

        /// <summary>
        /// Save Player Data
        /// </summary>
        /// <param name="res">For Callback Server Side </param>
        public void SaveData(Action<bool> res)
        {
            PlayerPrefs.SetString("PData", JsonUtility.ToJson(GetData));
            PlayerPrefs.Save();

            res(true);
        }

        /// <summary>
        /// Load Player Data
        /// </summary>
        /// <param name="res">For Callback Server Side</param>
        public void LoadData(Action<DataClass> res)
        {
            if (PlayerPrefs.HasKey("PData"))
            {
                GetData = JsonUtility.FromJson<DataClass>(PlayerPrefs.GetString("PData"));
            }
            else
            {
                GetData = new DataClass();
            }

            res(GetData);
        }

        /// <summary>
        /// Update Settings when end edit
        /// </summary>
        /// <param name="sense">Sensetivity</param>
        /// <param name="color">Ball Color</param>
        public void UpdateSettings(float sense, int color)
        {
            GetData.ballColor = color;
            GetData.sense = sense;

            SaveData((r) =>
            {
                Debug.Log("Settings Saved");
            });
        }

        /// <summary>
        /// Update Score when game end
        /// </summary>
        /// <param name="score">New Score</param>
        public void UpdateScore(int score)
        {
            if (score > GetData.bestScore)
            {
                GetData.bestScore = score;

                SaveData((r) =>
                {
                    Debug.Log("Scores Saved");
                });
            }
        }
    }
}
