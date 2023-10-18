using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringMolten : TriggerController
{
    public ParticleSystem particle;

    protected override void Update()
    {
        base.Update();

        if(!particle.isPlaying && isPlaying)
            particle.Play();

        if (particle.isPlaying &&!isPlaying)
            particle.Stop();
    }
}
