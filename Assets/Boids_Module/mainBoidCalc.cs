using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Boid/Algorithm/mainBoidCalc")]
//create an asset to contain all different behaviours/functions
public class mainBoidCalc : Boids_Algorithm
{
    public Boids_Algorithm[] resultantVectors;
    //array which stores all resultantVectors which can be easily scaled
    public float[] weights;
    //each resultant vector will have a corresponding weight to determine how
    //much it contributes to overall calculation
    public override Vector3 calcBoids(Boid_Agent agent, List<Transform> environment, Boids boids)
    {
        //weights and vectors should correspond, otherwise return error
        if (weights.Length != resultantVectors.Length) 
        {
            Debug.LogError("number of resultant vectors and weights do not match up for boids");
            return Vector3.zero;
        }
   
        Vector3 resultantVector = Vector3.zero;
        //iterate through all vectors
        for(int i  = 0; i < resultantVectors.Length; i++) 
        { 
            Vector3 updateVector = resultantVectors[i].calcBoids(agent, environment, boids) * weights[i];

            if (updateVector != Vector3.zero)
            {
                //makes sure the magnitude does not exceed weight
                if (updateVector.sqrMagnitude > weights[i] * weights[i])
                {
                    updateVector.Normalize();
                    //if it does exceed weight, normalise and then scale back up to magnitude of weight
                    updateVector *= weights[i];
                }
                
                resultantVector += updateVector;
            
            }
           

        }
        return resultantVector;
    } 
}
