using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
/*This is a Unity class which creates a box collider around the agent
  I have chosen a simple box collider as it will use the least computer resources
  Since flocks of boids will have large amounts of agents, the collider 
cannot be too complex or this will impact performance*/
public class Boid_Agent : MonoBehaviour
//MonoBehaviour class contains many useful life cycle functions
{
    //public Vector3 previousPosition;
    //private Vector3 velocity;

    private BoxCollider boidCollider;
    //each agent will have a collider attribute
    public BoxCollider getboidCollider { get { return boidCollider; } }
    // getter method to access collider

    // Start is called before the first frame update
    void Start()
    {
        //previousPosition = transform.position;
        boidCollider = GetComponent<BoxCollider>();
        // to cache
    }
    public void UpdatePosition(Vector3 movementVector)
    //void will not return a value
    {
        //Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        //setVelocity(velocity);
        transform.forward = movementVector;
        transform.position += (Vector3)movementVector * Time.deltaTime;
        // Time.deltaTime returns the interval in seconds from the last frame to the current one
        // position = speed * time
        // the transform class attached to the boid will move the boid
        // Unlike Vector3.forward, Transform.forward moves the GameObject while also considering its rotation
        // Transform.forward moves the GameObject in the z axis
    }
}

   