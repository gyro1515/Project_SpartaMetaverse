using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace MetaverseSession
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public PlayerController player { get; private set; }
        
        public static bool isFirstLoading = true;
        // ���� ���� Ÿ�ϸ� �־ ������ �ľ��ϱ�
        public Tilemap tilemap; 

        private void Awake()
        {
            instance = this;
            player = FindObjectOfType<PlayerController>();
            player.Init(this);
        }
        private void Start()
        {
            StartCoroutine(FadeManager.Instance.FadeIn());
        }
    }
}