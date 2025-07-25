using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheStackSession
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected UIManager uiManager;

        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        protected abstract UIState GetUIState();
        public void SetActive(UIState state)
        {
            gameObject.SetActive(GetUIState() == state);
        }
    }
}