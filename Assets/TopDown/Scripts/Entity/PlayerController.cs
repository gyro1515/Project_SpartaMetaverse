using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TopDownSession
{
    public class PlayerController : BaseController
    {
        private GameManager gameManager;
        private Camera _camera;

        public void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
            _camera = Camera.main;
        }
        protected override void HandleAction()
        {
            /*float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            movementDirection = new Vector2(horizontal, vertical).normalized;

            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
            lookDirection = (worldPos - (Vector2)transform.position);

            // �� ������ ũ���� 0.9f���� �۴ٸ�
            if (lookDirection.magnitude < .9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
            // ���� ��ȣ �Է¹ޱ�
            isAttacking = Input.GetMouseButton(0);*/
        }

        public override void Death()
        {
            base.Death();
            gameManager.GameOver();
        }
        // �Ʒ��� Player Input Component���� �ҷ�����
        void OnMove(InputValue inputValue)
        {
            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
        }

        void OnLook(InputValue inputValue)
        {
            Vector2 mousePosition = inputValue.Get<Vector2>();
            Vector2 worldPos = _camera.ScreenToWorldPoint(mousePosition);
            lookDirection = worldPos - (Vector2)transform.position;

            if (lookDirection.magnitude < .5f) // 0.9�� �ʹ� ŭ
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
        }

        void OnFire(InputValue inputValue)
        {
            // UI�� ���콺�� �ö� �������� �߻���� �ʰ�
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            isAttacking = inputValue.isPressed;
        }
    }
}