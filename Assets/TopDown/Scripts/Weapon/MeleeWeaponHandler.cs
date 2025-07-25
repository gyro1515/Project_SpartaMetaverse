using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class MeleeWeaponHandler : WeaponHandler
    {
        [Header("Melee Attack Info")]
        public Vector2 collideBoxSize = Vector2.one;

        protected override void Start()
        {
            base.Start();
            collideBoxSize = collideBoxSize * WeaponSize;
        }

        public override void Attack()
        {
            base.Attack();
            float an = Quaternion.Angle(transform.rotation, Quaternion.Euler((Vector3)Controller.LookDirection));
            //RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize, an, Vector2.zero, 0, target);
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize, 0, Vector2.zero, 0, target);

            if (hit.collider != null)
            {

                ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
                if (resourceController != null)
                {
                    resourceController.ChangeHealth(-Power);
                    // 디버그용
                    //DrawBox2D(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize,0, Color.red);
                    if (IsOnKnockback)
                    {
                        BaseController controller = hit.collider.GetComponent<BaseController>();
                        if (controller != null)
                        {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                        }
                    }
                }
            }
        }

        public override void Rotate(bool isLeft)
        {
            if (isLeft)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
        void DrawBox2D(Vector2 center, Vector2 size, float angleDeg, Color color, float duration = 1)
        {
            Quaternion rot = Quaternion.Euler(0, 0, angleDeg);
            Vector2 halfSize = size * 0.5f;

            // 박스 네 꼭짓점 (회전 적용)
            Vector2 topLeft = (Vector3)center + rot * new Vector2(-halfSize.x, halfSize.y);
            Vector2 topRight = (Vector3)center + rot * new Vector2(halfSize.x, halfSize.y);
            Vector2 bottomRight = (Vector3)center + rot * new Vector2(halfSize.x, -halfSize.y);
            Vector2 bottomLeft = (Vector3)center + rot * new Vector2(-halfSize.x, -halfSize.y);

            // 모서리 연결
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }
        // 이렇게 하면 호출이 안되도 계속 그려짐
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize);
        }
    }
}