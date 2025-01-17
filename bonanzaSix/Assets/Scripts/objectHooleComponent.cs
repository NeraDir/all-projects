using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class objectHooleComponent : MonoBehaviour
{
    private float speed;

    private float rotateValue;

    public static UnityEvent<Mesh> onMerge = new UnityEvent<Mesh>();
    public static UnityEvent dead = new UnityEvent();
    public static UnityEvent lastReached = new UnityEvent();

    [SerializeField]
    private LayerMask _layer;

    private MeshCollider _meshCollider;

    private bool _onCollider;

    public Mesh _mesh;

    public void Init()
    {
        _meshCollider = GetComponent<MeshCollider>();
        speed = 3.5f;
        dead.AddListener(Dead);
    }

    private void LateUpdate()
    {
        if (_mesh == null)
            return;
        _meshCollider.sharedMesh = _mesh;
        transform.parent.position += new Vector3(0, 0, 1) * speed * Time.deltaTime;
        transform.parent.Rotate(new Vector3(0, 0, rotateValue), 110 * Time.deltaTime);

        if (GameManager.holesSpawnedList.Count > 0)
        {
            if (Vector3.Distance(transform.position, GameManager.holesSpawnedList[0].transform.position) < 0.5f)
            {
                if (transform.parent.transform.rotation.z  < GameManager.holesSpawnedList[0].transform.rotation.z + 10 && transform.parent.transform.rotation.z  > GameManager.holesSpawnedList[0].transform.rotation.z - 10)
                {
                    OnMerger(GameManager.holesSpawnedList[0].transform.parent.GetComponentInChildren<goodMergeComponent>());
                }
                else
                {
                    dead?.Invoke();
                }
               
            }
        }
       
    }

    private void OnDestroy()
    {
        dead.RemoveListener(Dead);
    }

    private void Dead()
    {
        speed = 0;
    }

    public void OnClickRotate(int value)
    {
        rotateValue = value;
    }

    public void OnUpRotate()
    {
        rotateValue = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out holeObjectComponentn holeObject))
        {
            dead?.Invoke();
        }
        else if (other.gameObject.TryGetComponent(out goodMergeComponent good))
        {

        }
    }

    private void OnMerger(goodMergeComponent good)
    {
        GameManager.holesSpawnedList.Remove(GameManager.holesSpawnedList[0]);
        onMerge?.Invoke(good.transform.parent.GetComponentInChildren<holeObjectComponentn>().GetHoleMesh());
        good.transform.parent.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(good.transform.parent.gameObject));
        speed += .75f;
        if (good.GetLast())
        {
            lastReached?.Invoke();
        }
    }
}
