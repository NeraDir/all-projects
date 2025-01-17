using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private Transform Pos1;
    [SerializeField] private Transform Pos2;

    private LineRenderer lineRenderer;

    [SerializeField] private Camera cam;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray,out hit))
        {
            if (BallMovement.ballMoving == 0)
            {
                lineRenderer.SetPosition(0, Pos1.position);
                lineRenderer.SetPosition(1, Pos2.transform.position);
            }
            else
            {
                enabled = false;
            }
        }
    }
}
