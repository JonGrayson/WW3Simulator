using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public bool avoidObstacles = false;
    public GameObject myCohereTarget;
    BlendedSteering myBlendedSteering;
    PrioritySteering myPrioritySteering;
    Kinematic[] birds;

    void Start()
    {
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBirds = GameObject.FindGameObjectsWithTag("Bird");
        birds = new Kinematic[goBirds.Length - 1];
        int holder = 0;
        for(int i = 0; i < goBirds.Length - 1; i++)
        {
            if(goBirds[i] == this)
            {
                continue;
            }

            birds[holder++] = goBirds[i].GetComponent<Kinematic>();
        }
        separate.targets = birds;

        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        myBlendedSteering = new BlendedSteering();
        myBlendedSteering.behaviorTypes = new WeightandSteeringBehavior[3];

        myBlendedSteering.behaviorTypes[0] = new WeightandSteeringBehavior();
        myBlendedSteering.behaviorTypes[0].behavior = separate;
        myBlendedSteering.behaviorTypes[0].weight = 1f;

        myBlendedSteering.behaviorTypes[1] = new WeightandSteeringBehavior();
        myBlendedSteering.behaviorTypes[1].behavior = cohere;
        myBlendedSteering.behaviorTypes[1].weight = 1f;

        myBlendedSteering.behaviorTypes[2] = new WeightandSteeringBehavior();
        myBlendedSteering.behaviorTypes[2].behavior = myRotateType;
        myBlendedSteering.behaviorTypes[2].weight = 1f;

        //priority steering
        ObstacleAvoidance myAvoid = new ObstacleAvoidance();
        myAvoid.character = this;
        myAvoid.target = myCohereTarget;
        myAvoid.flee = true;

        BlendedSteering myHighPrioritySteering = new BlendedSteering();
        myHighPrioritySteering.behaviorTypes = new WeightandSteeringBehavior[1];
        myHighPrioritySteering.behaviorTypes[0] = new WeightandSteeringBehavior();
        myHighPrioritySteering.behaviorTypes[0].behavior = myAvoid;
        myHighPrioritySteering.behaviorTypes[0].weight = 1f;

        myPrioritySteering = new PrioritySteering();
        myPrioritySteering.groups = new BlendedSteering[2];
        myPrioritySteering.groups[0] = new BlendedSteering();
        myPrioritySteering.groups[0] = myHighPrioritySteering;

        myPrioritySteering.groups[1] = new BlendedSteering();
        myPrioritySteering.groups[1] = myBlendedSteering;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if(!avoidObstacles)
        {
            steeringUpdate = myBlendedSteering.GetSteering();
        }

        else
        {
            steeringUpdate = myPrioritySteering.GetSteering();
        }

        base.Update();
    }
}
