﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour {

    public ParticleSystem DamageParticles;
    public ParticleSystem FireParticles;
    public ParticleSystem MovementParticles;
    #region API

    public void Init()
    {
        DamageParticles.Stop();
        FireParticles.Stop();
        MovementParticles.Stop();
    }


    /// <summary>
    /// Play the Particles effect
    /// </summary>
    public void PlayParticles(ParticlesType _type)
    {
        switch (_type)
        {
            case ParticlesType.Damage:
                if (DamageParticles.isPlaying) 
                    DamageParticles.Stop();
                DamageParticles.Play();
                break;
            case ParticlesType.Fire:
                if (FireParticles.isPlaying) 
                    FireParticles.Stop();
                FireParticles.Play();
                break;
            case ParticlesType.Movement:
                if (!MovementParticles.isPlaying)
                    MovementParticles.Play();
                break;
            default:
                break;
        }
        
    }

    /// <summary>
    /// Stop the particles effect
    /// </summary>
    public void StopParticles(ParticlesType _type)
    {
        switch (_type)
        {
            case ParticlesType.Damage:
                DamageParticles.Stop();
                break;
            case ParticlesType.Fire:
                FireParticles.Stop();
                break;
            case ParticlesType.Movement:
                MovementParticles.Stop();
                break;
            default:
                break;
        }

    }

    #endregion


    public enum ParticlesType
    {
        Damage,
        Fire,
        Movement,
    }
}