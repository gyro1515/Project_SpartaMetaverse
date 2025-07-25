using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class AnimationHandler : MonoBehaviour
    {
        // 문자열 보다는 특정 숫자로 비교하는게 낫다.
        // id 값 부여
        private static readonly int IsMoving = Animator.StringToHash("IsMove");
        private static readonly int IsDamage = Animator.StringToHash("IsDamage");

        protected Animator animator;

        protected virtual void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void Move(Vector2 obj)
        {
            animator.SetBool(IsMoving, obj.magnitude > .5f);
        }

        public void Damage()
        {
            animator.SetBool(IsDamage, true);
        }

        public void InvincibilityEnd()
        {
            animator.SetBool(IsDamage, false);
        }
    }
}

