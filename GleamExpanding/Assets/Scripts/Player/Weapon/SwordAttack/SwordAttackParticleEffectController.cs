using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackParticleEffectController : MonoBehaviour
{
    // Particle System
    public ParticleSystem particleEffect;




    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        GetComponents();

        // Start Stop Effect
        StopEffect();
    }




    // Get Components
    private void GetComponents()
    {
        // Get Components
        particleEffect = GetComponent<ParticleSystem>();
        if (particleEffect == null)
        {
            Debug.LogWarning("Set Particle Effect Manually");
        }
    }




    // Start Play Effect
    public void PlayEffect()
    {
        if (particleEffect != null)
        {
            particleEffect.Play();
        }
    }

    // Stop Play Effect
    public void StopEffect()
    {
        if (particleEffect != null)
        {
            particleEffect.Stop();
        }
    }
}
