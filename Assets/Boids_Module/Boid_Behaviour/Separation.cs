using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Boid/Algorithm/Separation")]
public class Separation : Boids_Algorithm
{
    public override Vector3 calcBoids(Boid_Agent agent, List<Transform> environment, Boids boids)
    {
       //check for boids in range
       if (environment.Count == 0)
        {
            return Vector3.zero;
        }
       int avoidCount = 0;
        Vector3 separationVector = Vector3.zero;
        //initalise separation vector to [0,0,0]
        foreach(Transform boid in environment)
        //iterate through each boid in visual range
        {
            if (Vector3.SqrMagnitude(boid.position - agent.transform.position) < boids.getSquareAvoidRange)
            //compare magnitude of distance vector between boids to the avoid range
            {
              avoidCount++;
              //increment number of boids to avoid
              separationVector += (agent.transform.position - boid.position);
               //sum up all vectors but in opposite direction
            }
        }
        if (avoidCount > 0)
              separationVector /= avoidCount;
            //calculate average separation vector
        return separationVector;
    }
}
  
