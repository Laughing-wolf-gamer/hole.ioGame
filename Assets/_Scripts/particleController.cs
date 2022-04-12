using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour {

    public ParticleSystem particle;
    public ParticleSystem ember;
    public ParticleSystem smoke;

    // Use this for initialization
  public  void PlayParticle(Color color)
    {
        var main = ember.main;
        main.startColor = color;
        var main3 = smoke.main;
        main3.startColor = color;
        var main4 = particle.main;
        main4.startColor = color;

        particle.Play(true);
    }
	
	 
}
