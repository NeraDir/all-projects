using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody ballBody;
    [SerializeField] private Camera cam;
    [SerializeField] private DrawLine linedrawer;
    [SerializeField] private LineRenderer line;

    [SerializeField] private Transform objectGetRotation;

    [SerializeField] private Transform placeToGetRotation;

    RaycastHit hit;

    Ray ray;

    public static int ballMoving;

    public static bool rotation;

    private float rotationSpeed = 0;

    public static int grabOrNo = 0;

    public static bool started = false;

    private int oneClickOff;

    private float xPosObj;

    private void Start()
    {
        xPosObj = placeToGetRotation.position.x;
        ballMoving = 0;
        started = false;
        rotation = false;
        line.enabled = false;
    }

    private void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                line.enabled = false;
                objectGetRotation.rotation = Quaternion.identity;
                placeToGetRotation.position = new Vector3(0,0,0);
            }
            else if (Input.GetMouseButton(0) && oneClickOff <= 0)
            {
                objectGetRotation.LookAt(placeToGetRotation.transform);
                if (hit.point.z <= transform.position.z)
                {
                    placeToGetRotation.position = transform.position;
                }
                line.enabled = true;
            }
            else if (Input.GetMouseButtonUp(0) && placeToGetRotation.position.x != xPosObj)
            {
                LineRenderer line = GameObject.FindObjectOfType<LineRenderer>();
                Destroy(line);
                ballBody.transform.LookAt(placeToGetRotation.transform);
                ballBody.AddForce(transform.forward * 30, ForceMode.Impulse);
                ballMoving = 1;
                rotation = true;
                grabOrNo = Random.Range(1,3);
                Debug.Log(grabOrNo);
                started = true;
                enabled = false;
                line.enabled = false;
                PostDatas();
            }
        }

        if (rotation && !started)
        {
            rotationSpeed += 2 * Time.deltaTime;
            if (rotationSpeed >= 2)
            {
                rotationSpeed = 2;
            }
            transform.Rotate(transform.right * rotationSpeed, Space.Self);
        }
        else if(!rotation && started && ballMoving == 0)
        {
            rotationSpeed -= 2 * Time.deltaTime;
            if (rotationSpeed <= 0)
            {
                rotationSpeed = 0;
            }
            transform.Rotate(transform.right * rotationSpeed, Space.Self);
            Invoke(nameof(RestartGame), 5);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    void PostDatas() => StartCoroutine(PostData_Coroutine());

    IEnumerator PostData_Coroutine()
    {
        string uri = "https://game.anyplay.pro/private/football3D";
        WWWForm form = new WWWForm();
        form.AddField("title", "test data");
        using (UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                Debug.Log(request.downloadHandler.text);
        }
    }
}


