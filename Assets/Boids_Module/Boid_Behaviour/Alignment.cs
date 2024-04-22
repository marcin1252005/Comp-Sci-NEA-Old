using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Boid/Algorithm/Alignment")]
//creat asset under same submenu as other methods
public class Alignment : Boids_Algorithm

{
    public override Vector3 calcBoids(Boid_Agent agent, List<Transform> environment, Boids boids)
    {
        //check for boids in range
        if (environment.Count == 0)
            //instead of returning [0,0,0], maintain current alignment
            return agent.transform.forward;
        Vector3 sumVelocity = Vector3.zero;
        foreach(Transform boid in environment) 
        {
            sumVelocity += boid.forward;
            //sum all all velocity vectors
        }
        Vector3 alignmentVector = sumVelocity/environment.Count;
        //calculate average alignment
        return alignmentVector;
    }
}
