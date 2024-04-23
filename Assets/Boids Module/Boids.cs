using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    public Boid_Agent boidPrefab;
    //stores boid model
    List<Boid_Agent> agents = new List<Boid_Agent>();
    public Boids_Algorithm resultantVector;

    public Transform BoidsSpawnPoint;
    //where boids will spawn from game object in editor
    [Range(1f, 10000f)]
    public float visualRange = 3f;
    //visual range of boids
    [Range(0f, 10000f)]
    public float avoidRange = 0.1f;
    //takes a parameter between 0 and 1, will be a percentage of the visual range of the boid where it will avoid collision
    [Range(10, 300)]
    public int initialPop = 29;
    //starting value of boids
    [Range(0f, 10f)]
    public float speedMultiplier = 1f;
    [Range(0f, 100f)]
    public float maxSpeed = 100f;
    [Range(0.01f,10f)]
    public float boidDensity = 0.1f;
    //for initalising boids, to determine how spread out they are
    public float getboidDensity { get { return boidDensity; } }
    //getter method

    private float squareMaxSpeed;
    private float squareVisualRange;
    private float squareAvoidRange;
    //square values used for comparisons
    public void setSquareMaxSpeed(float newSquareMaxSpeed)
    {
       squareMaxSpeed = newSquareMaxSpeed;
    }
                                      
    public float getSquareMaxSpeed { get { return squareMaxSpeed; } }
    
    public void setSquareVisualRange(float newSquareVisualRange)
    {
        squareVisualRange = newSquareVisualRange;
    }
    public float getSquareVisualRange { get { return squareVisualRange; } }
    
    public void setSquareAvoidRange(float newSquareAvoidRange)
    {
        squareAvoidRange = newSquareAvoidRange;
    }
    public float getSquareAvoidRange { get { return squareAvoidRange; } }   
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        setSquareMaxSpeed(maxSpeed * maxSpeed);
        setSquareVisualRange(visualRange*visualRange);
        setSquareAvoidRange(avoidRange * avoidRange);
        //set all square values
       

        //instantiate all boids when loading the game
        for (int i = 0; i < initialPop; i++)
        {
            Boid_Agent newBoidAgent = Instantiate(boidPrefab, (UnityEngine.Random.insideUnitSphere * initialPop * boidDensity)+BoidsSpawnPoint.transform.position, Quaternion.Euler(UnityEngine.Random.insideUnitSphere * 360f));
            /*Instantiate function allows for many clones of a prefab to be instantiated at once
            Generates a random point inside the unit sphere in which to instantiate boid
            Quaternion is the parameter for rotation. Quaternion.identity function will copy rotation position and not alter it*/
            newBoidAgent.name = "Boid" + i;
            agents.Add(newBoidAgent);
            // appends each new boid to an array of all boids
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Boid_Agent agent in agents)
        //iterate through all agents
        {
            List<Transform> environment = boidsInVisualRange(agent);
            //agent.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.blue, environment.Count / 10f);
            //create a list of transforms of all the boids within visual range of another boid
            Vector3 updatePosition = resultantVector.calcBoids(agent, environment, this);
            updatePosition *= speedMultiplier;
            //change vector by the speed multiplier
            if (updatePosition.sqrMagnitude > getSquareMaxSpeed)
            //check if exceeds max speed
            {
                updatePosition = updatePosition.normalized * maxSpeed;
                //if exceeds max speed, normalise and reduce magnitude to 1
                //then multiply by maxSpeed to reset magnitude to maxSpeed
            }
                
           //calculates final vector for each agent given its environment in the array
            agent.UpdatePosition(updatePosition) ;
        }

        List<Transform> boidsInVisualRange(Boid_Agent agent)
        {
            List<Transform> environment = new List<Transform>();
            Collider[] colliders = Physics.OverlapSphere(agent.transform.position, visualRange);
            //performs collision check by checking for colliders within sphere of radius visual range
            foreach (Collider collider in colliders)
            {
                if (collider != agent.getboidCollider)
                {
                    //ignore own collider
                    environment.Add(collider.transform);
                }
                //append transform to list of colliders within range      

            }
            return environment;

        }
    }
}
