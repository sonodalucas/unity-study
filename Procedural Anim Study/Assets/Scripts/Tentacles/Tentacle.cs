using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public float trailSpeed;
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public Transform wiggleDir;
    public Transform particles;
    public bool hasParticles;

    private Vector3[] segmentVelocity;

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
    }

    private void Update()
    {
        wiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude);
        
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i],
                segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentVelocity[i],
                smoothSpeed + i / trailSpeed);
        }
        
        lineRend.SetPositions(segmentPoses);
        if (hasParticles)
            particles.position = segmentPoses[segmentPoses.Length - 1];
    }
}
