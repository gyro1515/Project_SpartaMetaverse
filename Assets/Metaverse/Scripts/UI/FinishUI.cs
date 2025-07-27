using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MetaverseSession
{
    public class FinishUI : MonoBehaviour
    {
        protected UIManager uiManager;
        [SerializeField] protected Button Close;
        [SerializeField] TextMeshProUGUI cScoreText;
        [SerializeField] TextMeshProUGUI bScoreText;

        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
            Close.onClick.AddListener(OnClickCloseButton);
            // 저장된 스코어에 따라 스코어 텍스트 설정하기
            uiManager.GetScore(out int curScore, out int bestScore);
            cScoreText.text = curScore.ToString();
            bScoreText.text = bestScore.ToString();
        }
        public void OnClickCloseButton()
        {
            gameObject.SetActive(false); // UI 끄기
        }
    }
}