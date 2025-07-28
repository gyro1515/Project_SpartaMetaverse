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

        [SerializeField] protected SpriteRenderer characterRenderer;
        [SerializeField] protected Animator characterAnimator;

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
            direction = direction * 6; // 스피드 고정
                  // 델타로 처리하지 않고, 바로 속도 지정해주기
            _rigidbody.velocity = direction;
            animationHandler.Move(direction);
        }
        private void Rotate() // 이동 방향에 따른 회전
        {
            if (_rigidbody.velocity.x == 0) return;

            characterRenderer.flipX = _rigidbody.velocity.x < 0; // 좌우 반전만
        }
        
    }
}