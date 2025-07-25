using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace TopDownSession
{
    public class BaseController : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;

        [SerializeField] private SpriteRenderer characterRenderer;
        [SerializeField] private Transform weaponPivot;

        protected Vector2 movementDirection = Vector2.zero;
        public Vector2 MovementDirection { get { return movementDirection; } }

        protected Vector2 lookDirection = Vector2.zero;
        public Vector2 LookDirection { get { return lookDirection; } }

        private Vector2 knockback = Vector2.zero;
        private float knockbackDuration = 0.0f;

        protected AnimationHandler animationHandler;
        protected StatHandler statHandler;

        [SerializeField] public WeaponHandler WeaponPrefab;
        protected WeaponHandler weaponHandler;

        protected bool isAttacking;
        private float timeSinceLastAttack = float.MaxValue;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            animationHandler = GetComponent<AnimationHandler>();
            statHandler = GetComponent<StatHandler>();

            if (WeaponPrefab != null)
                weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            else // 장착되어 있을 수도 있으니
                weaponHandler = GetComponentInChildren<WeaponHandler>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            HandleAction();
            Rotate(lookDirection);
            HandleAttackDelay();
        }

        protected virtual void FixedUpdate()
        {
            Movment(movementDirection);
            if (knockbackDuration > 0.0f)
            {
                knockbackDuration -= Time.fixedDeltaTime;
            }
        }

        protected virtual void HandleAction()
        {

        }

        private void Movment(Vector2 direction)
        {
            direction = direction * statHandler.Speed;
            if (knockbackDuration > 0.0f)
            {
                direction *= 0.2f;
                direction += knockback;
            }
            // 델타로 처리하지 않고, 바로 속도 지정해주기
            _rigidbody.velocity = direction;
            animationHandler.Move(direction);
        }

        private void Rotate(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bool isLeft = Mathf.Abs(rotZ) > 90f;

            characterRenderer.flipX = isLeft;

            if (weaponPivot != null)
            {
                weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
            }
            weaponHandler?.Rotate(isLeft);
        }

        public void ApplyKnockback(Transform other, float power, float duration)
        {
            knockbackDuration = duration;
            knockback = -(other.position - transform.position).normalized * power;
        }
        private void HandleAttackDelay()
        {
            if (weaponHandler == null)
                return;

            // 공격 딜레이 타임 누적
            if (timeSinceLastAttack <= weaponHandler.Delay)
            {
                timeSinceLastAttack += Time.deltaTime;
            }

            if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
            {
                //Debug.Log("Attack");
                timeSinceLastAttack = 0;
                Attack();


            }
        }

        protected virtual void Attack()
        {
            if (lookDirection != Vector2.zero)
                weaponHandler?.Attack();
        }

        public virtual void Death()
        {
            _rigidbody.velocity = Vector3.zero;

            // 죽으면 알파 값 낮추기
            foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                Color color = renderer.color;
                color.a = 0.3f;
                renderer.color = color;
            }
            // 작동하지 않도록 다 끄기
            foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
            {
                component.enabled = false;
            }

            Destroy(gameObject, 2f);
        }
    }
}