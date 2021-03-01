using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget : Kinematic
{
    Face mySeekRotateType;

    // Start is called before the first frame update
    void Start()
    {
        mySeekRotateType = new Face();
        mySeekRotateType.character = this;
        mySeekRotateType.target = myTarget;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = mySeekRotateType.getSteering().angular;
        base.Update();
    }
}
