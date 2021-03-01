using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightandSteeringBehavior
{
    public SteeringBehavior behavior = null;
    public float weight = 0f;
}

public class BlendedSteering
{
    public WeightandSteeringBehavior[] behaviorTypes;

    float maxAcceleration = 1f;
    float maxRotation = 5f;

    public SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        foreach (WeightandSteeringBehavior types in behaviorTypes)
        {
            SteeringOutput output = types.behavior.getSteering();
            if(output != null)
            {
                result.angular += output.angular * types.weight;
                result.linear += output.linear * types.weight;
            }
        }

        result.linear = result.linear.normalized * maxAcceleration;
        float angularAcceleration = Mathf.Abs(result.angular);
        if(angularAcceleration > maxRotation)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxRotation;
        }

        return result;
    }
}
