using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_SegmentController : MonoBehaviour
{
    public int index;
    public SegmentType segmentType;
    public Transform downPoint;
    public Transform upPoint;

    public Transform ballDetecterPoint;


}

public enum SegmentType
{
    easy,
    medium,
    hard
}