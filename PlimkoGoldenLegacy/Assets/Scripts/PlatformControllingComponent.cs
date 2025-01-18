using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformControllingComponent : MonoBehaviour
{
    private Rigidbody2D body;

    public Transform line;

    public Vector3 directionwe;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void OnMouseDown()
    {
        
    }

    private void LateUpdate()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, line.localPosition.y * directionwe.x, line.localPosition.z * directionwe.z), 1000 * Time.deltaTime);
    }

    private void OnMouseDrag()
    {
        if (SceneManager.GetActiveScene().name == "games2") 
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            body.transform.position += new Vector3(direction.x,transform.localPosition.y) * 20 * Time.deltaTime;
        }
        else
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            body.velocity = direction * 5;
        }
    }

    private void OnMouseUp()
    {
        
    }
}
