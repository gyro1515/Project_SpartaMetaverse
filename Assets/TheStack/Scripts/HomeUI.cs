using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheStackSession
{
    public class HomeUI : BaseUI
    {
        Button startButton;
        Button exitButton;

        protected override UIState GetUIState()
        {
            return UIState.Home;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            startButton = transform.Find("StartBtn").GetComponent<Button>();
            exitButton = transform.Find("ExitBtn").GetComponent<Button>();

            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnClickExitButton);
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