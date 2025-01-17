using UnityEngine;

public class PalceToGo : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform ball;

    private bool clicked = false;

    private void Update()
    {
        RaycastHit raycastHit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit))
        {

            if (Input.GetMouseButtonDown(0) && !clicked)
            {
                clicked = true;
            }
            else if (Input.GetMouseButton(0) && clicked)
            {
                transform.position = raycastHit.point;
                if (ball.position.z >= transform.position.z)
                {
                    transform.position = ball.position;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                
            }
        }
    }
}
