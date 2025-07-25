using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlaneSession
{
    public class Player : MonoBehaviour
    {
        Animator animator;
        Rigidbody2D _rigidbody;

        public float flapForce = 6f;
        public float forwardSpeed = 3f;
        public bool isDead = false;
        float deathCooldown = 0f;

        public bool isFlap = false;

        public bool godMode = false;

        GameManager gameManager;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;

            // 하위 컴포넌트까지 찾기
            animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            if (animator == null)
            {
                Debug.Log("Not anim");
            }
            if (_rigidbody == null)
            {
                Debug.Log("Not rig");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                if (deathCooldown <= 0f)
                {
                    // 게임 재시작
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        gameManager.RestartGame();
                    }
                }
                else
                {
                    deathCooldown -= Time.deltaTime;
                }
            }
            else
            {
                // Input.GetMouseButtonDown(0) -> 0은 터치도 가능하도록
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }
        // 물리 연산이 필요할 때 일정한 간격으로 호출
        private void FixedUpdate()
        {
            if (isDead) return;

            Vector3 velocity = _rigidbody.velocity;
            velocity.x = forwardSpeed;

            if (isFlap)
            {
                velocity.y += flapForce;
                isFlap = false;
            }

            _rigidbody.velocity = velocity;

            float angel = Mathf.Clamp(_rigidbody.velocity.y * 10, -90, 90);
            transform.rotation = Quaternion.Euler(0, 0, angel);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode || isDead) return;

            isDead = true;
            deathCooldown = 1f;
            animator.SetInteger("IsDie", 1);
            gameManager.GameOver();

        }
    }
}