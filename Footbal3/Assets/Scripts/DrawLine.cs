using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform firePosition;
    public int maximumReflectionCount = 5;
    public float maximumRayCastDistance = 50f;

    LineRenderer lineRenderer;

    List<Vector3> reflectionPositions = new List<Vector3>();

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    void Update()
    {
        DrawCurrentTrajectory();
    }

    void DrawCurrentTrajectory()
    {
        reflectionPositions.Clear();

        Vector2 position = transform.position;
        Vector2 direction = firePosition.position - transform.position;

        reflectionPositions.Add(position);

        for (int i = 0; i <= maximumReflectionCount; ++i)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, direction, maximumRayCastDistance);
            if (hit)
            {
                position = hit.point + hit.normal * 0.00001f;
                direction = Vector2.Reflect(direction, hit.normal);

                reflectionPositions.Add(position);
            }
        }

        lineRenderer.positionCount = reflectionPositions.Count;
        lineRenderer.SetPositions(reflectionPositions.ToArray());
    }
}
