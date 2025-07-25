using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class RangeWeaponHandler : WeaponHandler
    {
        [Header("Ranged Attack Data")]
        [SerializeField] private Transform projectileSpawnPosition;

        [SerializeField] private int bulletIndex;
        public int BulletIndex { get { return bulletIndex; } }

        [SerializeField] private float bulletSize = 1;
        public float BulletSize { get { return bulletSize; } }

        [SerializeField] private float duration;
        public float Duration { get { return duration; } }

        [SerializeField] private float spread;
        public float Spread { get { return spread; } }

        [SerializeField] private int numberofProjectilesPerShot;
        public int NumberofProjectilesPerShot { get { return numberofProjectilesPerShot; } }

        [SerializeField] private float multipleProjectilesAngel;
        public float MultipleProjectilesAngel { get { return multipleProjectilesAngel; } }

        [SerializeField] private Color projectileColor;
        public Color ProjectileColor { get { return projectileColor; } }

        private ProjectileManager projectileManager;

        public override void Attack()
        {
            base.Attack();

            float projectilesAngleSpace = multipleProjectilesAngel;
            int numberOfProjectilesPerShot = numberofProjectilesPerShot;

            float minAngle = -((numberOfProjectilesPerShot - 1) / 2f) * projectilesAngleSpace;
            //float minAngle = -numberOfProjectilesPerShot * projectilesAngleSpace;

            // Raycast 그리기: 마우스 방향
            //Debug.DrawRay(projectileSpawnPosition.position, Controller.LookDirection * 100f, Color.magenta, 1f);
            for (int i = 0; i < numberOfProjectilesPerShot; i++)
            {
                float angle = minAngle + projectilesAngleSpace * i;
                // 궤도 디버그, 랜덤 전
                //Debug.DrawRay(projectileSpawnPosition.position, RotateVector2(Controller.LookDirection, angle) * 100f, Color.white, 1f);

                float randomSpread = Random.Range(-spread, spread);
                angle += randomSpread;

                CreateProjectile(Controller.LookDirection, angle);
            }
        }
        protected override void Start()
        {
            base.Start();
            projectileManager = ProjectileManager.Instance;
        }
        private void CreateProjectile(Vector2 _lookDirection, float angle)
        {
            projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
        }
        private static Vector2 RotateVector2(Vector2 v, float degree)
        {
            return Quaternion.Euler(0, 0, degree) * v;
        }
    }
}