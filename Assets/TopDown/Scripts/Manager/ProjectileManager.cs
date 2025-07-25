using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class ProjectileManager : MonoBehaviour
    {
        private static ProjectileManager instance;
        public static ProjectileManager Instance { get { return instance; } }

        [SerializeField] private GameObject[] projectilePrefabs;

        [SerializeField] private ParticleSystem impactParticleSystem;

        private void Awake()
        {
            instance = this;
        }

        public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
        {
            GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
            GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

            ProjectileController projectileController = obj.GetComponent<ProjectileController>();
            projectileController.Init(direction, rangeWeaponHandler, this);

            // 발사체 궤도 디버그
            //Debug.DrawRay(startPostiion, direction * 100f, Color.red, 1f);

        }
        public void CreateImpactParticlesAtPosition(Vector3 position, RangeWeaponHandler weaponHandler)
        {
            impactParticleSystem.transform.position = position;
            // 구조체이지만 참조 형식처럼 적용됨 -> 참조라고 봐도 무방
            ParticleSystem.EmissionModule em = impactParticleSystem.emission;
            em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));
            ParticleSystem.MainModule mainModule = impactParticleSystem.main;
            mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
            impactParticleSystem.Play();
        }
    }
}