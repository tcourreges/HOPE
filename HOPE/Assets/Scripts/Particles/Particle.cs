using UnityEngine;
 using System.Collections;
 
/*
	simple script to kill a ParticleSystem when the animation is done
*/

 public class Particle : MonoBehaviour 
 {
     private ParticleSystem ps;
 
 
     public void Start() 
     {
         ps = GetComponent<ParticleSystem>();
     }
 
     public void Update() 
     {
         if(ps)
         {
             if(!ps.IsAlive())
             {
                 Destroy(gameObject);
             }
         }
     }
 }
