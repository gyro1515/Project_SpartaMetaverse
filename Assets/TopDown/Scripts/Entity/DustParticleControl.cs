using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownSession
{
    public class DustParticleControl : MonoBehaviour
    {
        [SerializeField] private bool createDustOnWalk = true;
        [SerializeField] private ParticleSystem dustParticleSystem;
        public void CreateDustParticles()
        {
            if (createDustOnWalk)
            {
                dustParticleSystem.Stop();
                dustParticleSystem.Play();
            }
        }
    }
}