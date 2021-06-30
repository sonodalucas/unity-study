using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HakuBody : MonoBehaviour
{
    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    public Transform[] bodyParts;

    private Vector3[] segmentVelocity;

    private void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
    }

    private void Update()
    {
        segmentPoses[0] = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector3 targetPos = segmentPoses[i - 1] + (segmentPoses[i] - segmentPoses[i - 1]).normalized * targetDist;
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], targetPos, ref segmentVelocity[i], smoothSpeed);
            bodyParts[i - 1].transform.position = segmentPoses[i];
        }
        
        lineRend.SetPositions(segmentPoses);
    }
}
