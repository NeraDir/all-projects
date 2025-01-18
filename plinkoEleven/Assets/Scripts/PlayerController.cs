using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _checkRadius;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Joystick _js;
    [SerializeField] private AnimationCurve _animCurve;

    [SerializeField]
    private Image[] _heartsImages;

    private int _heartsCount;

    public static UnityEvent PlayerIsDeath = new UnityEvent();
    public static UnityEvent PlayerReachPart = new UnityEvent();
    public static UnityEvent PlayerGetHeart = new UnityEvent();

    private MeshRenderer _meshRenderer;

    private List<GameObject> _bgObjects = new List<GameObject>();

    private void Start()
    {
        _heartsCount = 3;
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = GameManager.targetMaterial;
        PlayerGetHeart.AddListener(GetHeart);
    }

    private void GetHeart()
    {
        _heartsCount++;
    }

    private void OnDestroy()
    {
        PlayerGetHeart.RemoveListener(GetHeart);
    }

    private void LateUpdate()
    {
        if (_heartsCount <= 0)
        {
            PlayerIsDeath?.Invoke();
            return;
        }
        transform.position += new Vector3(_js.Horizontal * _moveSpeed, -20.1f, _moveSpeed) * Time.deltaTime;
        if (Physics.CheckSphere(transform.position, _checkRadius, _groundLayer))
            StartCoroutine(Jumping());

        if (_heartsCount >= 3)
            _heartsCount = 3;
        for (int i = 0; i < _heartsImages.Length; i++)
            if (i >= _heartsCount)
                _heartsImages[i].transform.DOScale(Vector3.zero, 0.25f);
            else
                _heartsImages[i].transform.DOScale(Vector3.one, 0.25f);
    }

    private IEnumerator Jumping()
    {
        float progressValue = 0;
        float currentTime = 0;
        while (progressValue != _jumpDuration)
        {
            currentTime += Time.deltaTime;
            progressValue = currentTime / _jumpDuration;
            transform.position = new Vector3(transform.position.x, _animCurve.Evaluate(progressValue), transform.position.z);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.TryGetComponent(out PartComponent part))
            {
                PlayerReachPart?.Invoke();
            }
            if (other.TryGetComponent(out PartPlatformComponent platform))
            {
                if (GameManager.targetMaterial != platform.myMaterial)
                {
                    _heartsCount -= 1;
                }
                platform.transform.DOScale(Vector3.zero, 0.25f);
            }
            if (other.TryGetComponent(out ICollisionObject collsion))
            {
                collsion.Use();
            }
            if (other.CompareTag("bgItems"))
            {
                _bgObjects.Add(other.gameObject);
                if (_bgObjects.Count > 1)
                {
                    Destroy(_bgObjects[0].gameObject);
                    _bgObjects.Remove(_bgObjects[0]);
                }
                Instantiate(other.gameObject, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 2340.427f), Quaternion.identity);
            }
            if (other.CompareTag("Death"))
            {
                PlayerIsDeath?.Invoke();
                enabled = false;
            }
        }
    }
}
