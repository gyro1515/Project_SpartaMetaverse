using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = .5f;

        private BaseController baseController;
        private StatHandler statHandler;
        private AnimationHandler animationHandler;

        private float timeSinceLastChange = float.MaxValue;

        private Action<float, float> OnChangeHealth;

        public float CurrentHealth { get; private set; }
        //public float MaxHealth => statHandler.Health;
        // 최대 체력은 StatHandler로부터 가져옴
        // 코드 개선:
        // null체크하고 null이면 0을 반환, 아니라면 statHandler.Health 반환
        public float MaxHealth => statHandler?.Health ?? 0f;

        public AudioClip damageClip;

        private void Awake()
        {
            statHandler = GetComponent<StatHandler>();
            animationHandler = GetComponent<AnimationHandler>();
            baseController = GetComponent<BaseController>();
        }

        private void Start()
        {
            CurrentHealth = statHandler.Health;
        }

        private void Update()
        {
            if (timeSinceLastChange < healthChangeDelay)
            {
                timeSinceLastChange += Time.deltaTime; // 무적시간 누적
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    // 무적 해제되며 애니메이션 세팅
                    animationHandler.InvincibilityEnd();
                }
            }
        }

        public bool ChangeHealth(float change)
        {
            if (change == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0f; // 무적 타임 시작
            CurrentHealth += change;
            /*CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;*/
            // 위보다 이게 더 낫지 않나?
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

            if (change < 0)
            {
                animationHandler.Damage();
                if (damageClip != null)
                    SoundManager.PlayClip(damageClip);
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }

        private void Death()
        {
            baseController.Death();
        }
        public void AddHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth += action;
        }

        public void RemoveHealthChangeEvent(Action<float, float> action)
        {
            OnChangeHealth -= action;
        }
    }
}