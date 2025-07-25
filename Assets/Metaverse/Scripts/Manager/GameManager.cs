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
        // 현재 맵의 타일맵 넣어서 사이즈 파악하기
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