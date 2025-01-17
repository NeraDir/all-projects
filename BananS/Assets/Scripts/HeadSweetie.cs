using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class HeadSweetie : MonoBehaviour
{
    public float moveSpeed;
    public float jumpPower;
    private Rigidbody sRigidbody;

    public float checkGroundRadius;
    public LayerMask groundMask;

    public LayerMask jellyMask;


    public GameObject partPrefab;
    private List<GameObject> parts = new();
    private List<Vector3> positionsHistory = new();

    public int gap;
    public int distanceBetweenParts;

    private Vector3 defSize;


    public List<Material> sweetieMaterials;

    public Jelly currentJelly;

    private bool canMove;


    private void OnEnable()
    {
        canMove = false;

        

        //canJumpInJelly = false;
        UI_GamePlayLayer.TapToScreenEvent += Jump;
    }
    private void OnDisable()
    {
        UI_GamePlayLayer.TapToScreenEvent -= Jump;
    }



    private void Start()
    {
        sRigidbody = GetComponent<Rigidbody>();
        sRigidbody.isKinematic = true;



        Invoke(nameof(SpawnParts), 3f);

        defSize = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(defSize, 0.24f);


        GetComponent<MeshRenderer>().material = sweetieMaterials[Random.Range(0, sweetieMaterials.Count)];

    }

    private void SpawnParts()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            sRigidbody.velocity = new Vector3(moveSpeed, sRigidbody.velocity.y, sRigidbody.velocity.z);
    



        positionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach(var part in parts)
        {
            //Vector3 point = headPositionsData[Mathf.Clamp(idx * distanceBetwenPart, 0, headPositionsData.Count - 1)];
            //Vector3 point = positionsHistory[Mathf.Min(index * gap, positionsHistory.Count - 1)];
            Vector3 point = positionsHistory[Mathf.Clamp(index * gap, 0, positionsHistory.Count - 1)];
            //Vector3 direction = point - part.transform.position;

            part.transform.position = point;
            part.transform.rotation = transform.rotation;
            //part.transform.LookAt(point);
           

            index++;
        }


        
    }

    public void StartMove()
    {
        //Quaternion lastRotation = Quaternion.Euler(transform.rotation.x, 0, 0);
        transform.SetParent(null);

        sRigidbody.isKinematic = false;
        canMove = true;


        

        //transform.rotation = lastRotation;
    }

    public void StopMove()
    {
        sRigidbody.velocity = Vector3.zero;
        canMove = false;
        Destroy(sRigidbody);
    }


    public void Jump()
    {
        if (!canMove)
            return;

        if(Physics.CheckSphere(transform.position, checkGroundRadius, groundMask))
        {
            sRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (currentJelly != null)
        {
            Debug.Log("!null");
            currentJelly.SetJumpAnimationState();
            currentJelly = null;

            sRigidbody.AddForce(Vector3.up * jumpPower * 2f, ForceMode.Impulse);
        }
        else
        {
            Debug.Log("null");
        }

        
    }

    public delegate void AddPartDelegate();
    public static event AddPartDelegate AddPartEvent;

    public delegate void ObstacleTriggerDelegate();
    public static event ObstacleTriggerDelegate ObstacleTriggerEvent;

    public void AddPart(GameObject newPart)
    {
        //GameObject body = Instantiate(partPrefab);
        parts.Add(newPart);

        if(parts.Count == 1)
        {

            GameObject body = Instantiate(newPart);
            body.GetComponent<SweetiePart>().StopIdleMove();

            newPart.SetActive(false);

           

            AddPart(body);
        }

        if (parts.Count < 5f)
        {
            if (AddPartEvent != null)
                AddPartEvent();
        }

        ParametersPerformer.sweetieCount++;

        /*
        Sequence testSeq = DOTween.Sequence();
        testSeq.Append(body.transform.DOScale(defSizeTemp, 0.24f));
        testSeq.Append(body.transform.DOScale(Vector3.zero, 1f));
        testSeq.SetLoops(-1, LoopType.Restart);
        //testSeq.Pause();
        */
        /*
        Vector3 defSizeTemp = body.transform.localScale;
        body.transform.localScale = Vector3.zero;

        Sequence testSeq = DOTween.Sequence();
        testSeq.Append(body.transform.DOScale(defSizeTemp, 0.24f));
        testSeq.Append(body.transform.DOScale(Vector3.zero, 1f));
        testSeq.SetLoops(-1, LoopType.Restart);
        */
        /*
        Vector3 defSizeTemp = body.transform.localScale;
        body.transform.localScale = Vector3.zero;

        body.transform.DOScale(defSizeTemp, 0.24f).OnComplete(()=> body.transform.DOScale(Vector3.zero, 1f));
        */
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out SweetiePart sweetiePart) && sweetiePart.canTrigger)
        {
            sweetiePart.StopIdleMove();
            AddPart(sweetiePart.gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            if (ObstacleTriggerEvent != null)
                ObstacleTriggerEvent();
        }
    }

}
