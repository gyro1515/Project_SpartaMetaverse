using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FlappyPlaneSession 
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI restartText;

        public void Start()
        {
            if (restartText == null)
            {
                Debug.LogError("restart text is null");
            }

            if (scoreText == null)
            {
                Debug.LogError("scoreText is null");
                return;
            }

            restartText.gameObject.SetActive(false);
        }

        public void SetRestart()
        {
            restartText.gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}

