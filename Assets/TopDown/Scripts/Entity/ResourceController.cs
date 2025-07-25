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
        // �ִ� ü���� StatHandler�κ��� ������
        // �ڵ� ����:
        // nullüũ�ϰ� null�̸� 0�� ��ȯ, �ƴ϶�� statHandler.Health ��ȯ
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
                timeSinceLastChange += Time.deltaTime; // �����ð� ����
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    // ���� �����Ǹ� �ִϸ��̼� ����
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

            timeSinceLastChange = 0f; // ���� Ÿ�� ����
            CurrentHealth += change;
            /*CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;*/
            // ������ �̰� �� ���� �ʳ�?
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