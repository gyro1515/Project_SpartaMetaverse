using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace MetaverseSession
{
    public class BaseController : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;

        [SerializeField] private SpriteRenderer characterRenderer;

        protected Vector2 movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return movementDirection; } }

        protected AnimationHandler animationHandler;

        private bool canMove = true;
        public bool CanMove { get 
            { return canMove; } 
            set
            {
                canMove = value;
                if (!value)
                {
                    movementDirection = Vector2.zero;
                }
            }
        }
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            animationHandler = GetComponent<AnimationHandler>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandleAction();
            
        }

        protected virtual void FixedUpdate()
        {
            if (!canMove)
            {
                Movment(Vector2.zero);
                return;
            }
            Movment(movementDirection);
            Rotate();
        }

        protected virtual void HandleAction()
        {

        }

        private void Movment(Vector2 direction)
        {
            //direction = direction * statHandler.Speed;
            direction = direction * 6; // ���ǵ� ����
                  // ��Ÿ�� ó������ �ʰ�, �ٷ� �ӵ� �������ֱ�
            _rigidbody.velocity = direction;
            animationHandler.Move(direction);
        }
        private void Rotate() // �̵� ���⿡ ���� ȸ��
        {
            if (_rigidbody.velocity.x == 0) return;

            characterRenderer.flipX = _rigidbody.velocity.x < 0; // �¿� ������
        }
        
    }
}