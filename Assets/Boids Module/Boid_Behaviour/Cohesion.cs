using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Boid/Algorithm/Cohesion")]
//Creates submenus and allows to create new asset in unity editor
public class Cohesion :Boids_Algorithm
//inherits from boid algorithm
{
    public override Vector3 calcBoids(Boid_Agent agent, List<Transform> environment, Boids boids)
    //overrides calcboids method from boid algorithm for cohesion calculation
    {
        //check for any boids in range
        if (environment.Count == 0)
            return Vector3.zero;
        //set sumCentre to zero
        Vector3 sumCentre = Vector3.zero;
        foreach(Transform boid in environment)
        {
            sumCentre += boid.transform.position;
            //sum up all the position vectors of every boid in range
        }
        Vector3 avgCentre = sumCentre/environment.Count;
        //calculate the average
        Vector3 cohesionVector = avgCentre - agent.transform.position;
        //find the distance vector from the boid to its perceived centre of mass
        return cohesionVector;
    }

   
}
