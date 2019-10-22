using System.Collections;
using Tools.Patterns.GameEvents;
using UnityEngine;

namespace HexCardGame.UI
{
    public class UiParticles : UiGameEventListener
    {
        protected ParticleSystem[] Particles { get; set; }

        protected virtual void Awake() => Particles = GetComponentsInChildren<ParticleSystem>();

        protected virtual IEnumerator Play(float delay = 0)
        {
            yield return new WaitForSeconds(delay);

            foreach (var particleSys in Particles)
                if (particleSys != null)
                    particleSys.Play();
        }
    }
}