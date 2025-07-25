using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheStackSession
{
    public class ScoreUI : BaseUI
    {
        TextMeshProUGUI scoreText;
        TextMeshProUGUI comboText;
        TextMeshProUGUI bestScoreText;
        TextMeshProUGUI bestComboText;

        Button startButton;
        Button exitButton;

        protected override UIState GetUIState()
        {
            return UIState.Score;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            scoreText = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
            comboText = transform.Find("ComboTxt").GetComponent<TextMeshProUGUI>();
            bestScoreText = transform.Find("BestScoreTxt").GetComponent<TextMeshProUGUI>();
            bestComboText = transform.Find("BestComboTxt").GetComponent<TextMeshProUGUI>();
            startButton = transform.Find("StartBtn").GetComponent<Button>();
            exitButton = transform.Find("ExitBtn").GetComponent<Button>();

            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
        }
        public void SetUI(int score, int combo, int bestScore, int bestCombo)
        {
            scoreText.text = score.ToString();
            comboText.text = combo.ToString();
            bestScoreText.text = bestScore.ToString();
            bestComboText.text = bestCombo.ToString();
        }

        void OnClickStartButton()
        {
            uiManager.OnClickStart();
        }

        void OnClickExitButton()
        {
            uiManager.OnClickExit();
        }
    }
}