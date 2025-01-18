using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class BallComponent : MonoBehaviour
{
    [SerializeField]
    private float _ballSpeed;

    [SerializeField]
    private float _ballJumpStrenght;

    [SerializeField]
    private float _groundRadius;

    [SerializeField]
    private LayerMask _groundLayer;

    [SerializeField]
    private GameObject _ballPartPrefab;

    [SerializeField]
    private int _gapValue;

    [SerializeField]
    private int _distanceBetweenBalls;

    [SerializeField]
    private List<Material> _ballMaterials;

    [SerializeField]
    private Image[] _heartsImages;

    private Rigidbody _ballBody;
    private List<GameObject> _ballPartsList = new();
    private List<Vector3> _ballsPartsPositionsHistory = new();
    private Vector3 _defaultSize;

    public static UnityEvent endReached = new UnityEvent();
    public static UnityEvent removePart = new UnityEvent();
    public static UnityEvent dead = new UnityEvent();

    private float targetFOV;

    private void Start()
    {
        targetFOV = 60;
        _ballBody = GetComponent<Rigidbody>();
        _ballBody.isKinematic = true;

        _ballPartsList.Clear();
        _defaultSize = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(_defaultSize, 0.24f);

        GetComponent<MeshRenderer>().material = _ballMaterials[Random.Range(0, _ballMaterials.Count)];
        transform.SetParent(null);

        _ballBody.isKinematic = false;
        removePart.AddListener(RemovePart);
    }

    private void FixedUpdate()
    {
        _ballBody.velocity = new Vector3(_ballBody.velocity.x, _ballBody.velocity.y, _ballSpeed);
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _heartsImages.Length; i++)
        {
            if (i>= GameSavesManager.GameHeartsCount)
            {
                _heartsImages[i].transform.DOScale(Vector3.zero, 0.25f);
            }
        }

        _ballsPartsPositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach (var part in _ballPartsList)
        {
            if (part == null)
            {
                _ballPartsList.Remove(part);
            }
            Vector3 point = _ballsPartsPositionsHistory[Mathf.Clamp(index * _gapValue, 0, _ballsPartsPositionsHistory.Count - 1)];
            point = new Vector3(point.x, point.y, point.z + _distanceBetweenBalls * index + 1);
            part.transform.position = point;
            part.transform.rotation = transform.rotation;

            index++;
        }

    }

    public void AddPart(GameObject newPart)
    {
        _ballPartsList.Add(newPart);

        if (_ballPartsList.Count == 1)
        {
            GameObject body = Instantiate(newPart);
            body.GetComponent<BallAdditionalComponent>().StopIdleMove();
            newPart.SetActive(false);
            AddPart(body);
        }
        GameSavesManager.GameCurrentBallsCount += 1;
    }

    public void RemovePart()
    {
        if (_ballPartsList.Count > 0)
        {
            Destroy(_ballPartsList[_ballPartsList.Count - 1]);
            _ballPartsList.Remove(_ballPartsList[_ballPartsList.Count - 1]);
            GameSavesManager.GameCurrentBallsCount -= 1;
        }
        else
        {
            GameSavesManager.GameHeartsCount--;
            if (GameSavesManager.GameHeartsCount <= 0)
            {
                dead?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BallAdditionalComponent ballPrefTemp) && ballPrefTemp.canTrigger)
        {
            ballPrefTemp.StopIdleMove();
            AddPart(ballPrefTemp.gameObject);
            targetFOV += 10;
            if (targetFOV >= 80)
            {
                targetFOV = 80;
            }
        }
        if (other.TryGetComponent(out StarComponent star))
        {
            star.OnColliseion();
        }
        if (other.TryGetComponent(out EndComponent end))
        {
            endReached?.Invoke();
        }
    }
}
