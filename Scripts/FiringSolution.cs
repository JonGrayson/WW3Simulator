using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FiringSolution
{
    public Nullable<Vector3> Calculate(Vector3 start, Vector3 end, float muzzleVelocity, Vector3 gravitationallPull)
    {
        Nullable<float> timeToTarget = GetTimeToTarget(start, end, muzzleVelocity, gravitationallPull);
        if(!timeToTarget.HasValue)
        {
            return null;
        }
        Debug.Log("Time to Target: " + timeToTarget);

        Vector3 delta = end - start;
        Debug.Log("Vector to Target: " + delta);

        Vector3 n1 = delta * 2;
        Vector3 n2 = gravitationallPull * (timeToTarget.Value * timeToTarget.Value);
        float d = 2 * muzzleVelocity * timeToTarget.Value;
        Vector3 solution = (n1 - n2) / d;

        Debug.Log("Solution = " + n1 + " - " + n2 + " / " + d);
        Debug.Log("Solution = " + solution);

        return solution;
    }

    public Nullable<float> GetTimeToTarget(Vector3 start, Vector3 end, float muzzleVelocity, Vector3 gravitationalPull)
    {
        Vector3 delta = start - end;

        float a = gravitationalPull.magnitude * gravitationalPull.magnitude;
        float b = -4 * (Vector3.Dot(gravitationalPull, delta) + muzzleVelocity * muzzleVelocity);
        float c = 4 * delta.magnitude * delta.magnitude;

        float b2minus4ac = (b * b) - (4 * a * c);
        if(b2minus4ac < 0)
        {
            return null;
        }

        float time0 = Mathf.Sqrt((-b + Mathf.Sqrt(b2minus4ac)) / (2 * a));
        float time1 = Mathf.Sqrt((-b - Mathf.Sqrt(b2minus4ac)) / (2 * a));

        Nullable<float> timeToTarget;
        if(time0 < 0)
        {
            if(time1 < 0)
            {
                return null;
            }
            else
            {
                timeToTarget = time1;
            }
        }
        else if (time1 < 0)
        {
            timeToTarget = time0;
        }
        else
        {
            timeToTarget = Mathf.Min(time0, time1);
        }

        return timeToTarget;
    }
}
