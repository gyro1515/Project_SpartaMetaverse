using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TheStackSession
{
    public class GameUI : BaseUI
    {
        TextMeshProUGUI scoreText;
        TextMeshProUGUI comboText;
        TextMeshProUGUI maxComboText;
        protected override UIState GetUIState()
        {
            return UIState.Game;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);
            scoreText = transform.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
            comboText = transform.Find("ComboTxt").GetComponent<TextMeshProUGUI>();
            maxComboText = transform.Find("MaxComboTxt").GetComponent<TextMeshProUGUI>();
        }
        public void SetUI(int score, int combo, int maxCombo)
        {
            scoreText.text = score.ToString();
            comboText.text = combo.ToString();
            maxComboText.text = maxCombo.ToString();
        }
    }
}