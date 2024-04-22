using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boids_Algorithm : ScriptableObject 
    // abstract class as does not need to be instantiated. Scriptable objects cannot be initialised and store large amounts of data
{
    public abstract Vector3 calcBoids(Boid_Agent agent, List<Transform> environment, Boids boids);
        /* returns a 3D vector, passes in an agent and a list of any surrounding obstacles,
        such as neighbouring boids or terrain objects
        the Unity class Transform stores the position, scale and properties of surrounding objects
        this can be thought of as what the boid can see in its visual range*/

}


