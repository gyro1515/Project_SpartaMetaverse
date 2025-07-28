using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MetaverseSession
{
    public enum CharState
    {
        Char1, Char2, Char3,
    }
    public class CharSelUI : MonoBehaviour
    {
        protected UIManager uiManager;

        [SerializeField] Button char1;
        [SerializeField] Button char2;
        [SerializeField] Button char3;
        [SerializeField] List<CharacterBase> characterBases;

        static int selectedChar = -1;
        public virtual void Init(UIManager uiManager)
        {
            this.uiManager = uiManager;
            // 버튼 재정의 하고 델리게이트 추가로 만들어서 어떤 버튼(index 기반)이 눌렸는지 체크해야 하지만 시간이 없는 관계로 함수 3개...
            char1.onClick.AddListener(OnClickSelChar1);
            char2.onClick.AddListener(OnClickSelChar2);
            char3.onClick.AddListener(OnClickSelChar3);
            
        }
        public void OnClickSelChar1()
        {
            selectedChar = 0;
            SetCharacter();
            SetAcive(false);
        }
        public void OnClickSelChar2()
        {
            selectedChar = 1;
            SetCharacter();
            SetAcive(false);

        }
        public void OnClickSelChar3()
        {
            selectedChar = 2;
            SetCharacter();
            SetAcive(false);

        }
        void SetAcive(bool active)
        {
            gameObject.SetActive(active);
        }
        public void SetCharacter() // 처음 씬 로드될 시, UI매니저에서도 실행됨
        {
            if (selectedChar == -1) return;
            GameManager.instance.player.SetCharacter(characterBases[selectedChar]);
        }
    }
}