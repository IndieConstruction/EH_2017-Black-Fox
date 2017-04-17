using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour {

    public ParticleSystem particles;

    #region API

    /// <summary>
    /// Play the Particles effect
    /// </summary>
    public void PlayParticles()
    {
        particles.Play();
        Debug.Log(particles.isEmitting);
    }

    /// <summary>
    /// Stop the particles effect
    /// </summary>
    public void StopParticles()
    {
        particles.Stop();
    }

    #endregion
}
