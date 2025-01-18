using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class RabbitJungleComponent : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _lineVertexCount = 12;

    [SerializeField]
    private float _pointOf2 = 2;

    [SerializeField]
    private Transform _beginPoint;

    [SerializeField]
    private Transform _middlePoint;

    [SerializeField]
    private Transform _endPoint;

    [SerializeField]
    private GameObject _eggGetEffect;

    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField]
    private Material[] _rabbitSkins;

    private Vector3[] _pointsList;

    private GameObject _currentPlatform;

    private GameObject _nextPlatform;

    private bool _sliderDirections = false;

    private bool _isJumping = false;

    private int _currentPointIndex = 0;

    public static bool canDo;

    private Animator _aniamtor;

    private void Start()
    {
        canDo = false;
        _aniamtor = GetComponent<Animator>();
        _skinnedMeshRenderer.material = _rabbitSkins[RabbitJungleGameManager.rabbitJungleSkinSelectedIndex];
    }

    private void LateUpdate()
    {
        if (canDo)
        {
            return;
        }

        if (Input.GetMouseButton(0) && !_isJumping)
        {
            UpdateTrajectory();
        }
        else if (Input.GetMouseButtonUp(0) && !_isJumping)
        {
            _isJumping = true;
            transform.parent = null;
        }

        if (_isJumping)
        {
            _aniamtor.SetBool("RabbitState", true);
            transform.position = Vector3.MoveTowards(transform.position, _pointsList[_currentPointIndex], 25 * Time.deltaTime);
            if (transform.position == _pointsList[_currentPointIndex])
            {
                _currentPointIndex++;
                if (_currentPointIndex >= _pointsList.Length)
                {
                    _isJumping = false;
                    _currentPointIndex = 0;
                }
            }
        }
        else
        {
            _aniamtor.SetBool("RabbitState", false);
        }
    }

    public void UpdateTrajectory()
    {
        _endPoint.transform.position = new Vector3(_nextPlatform.transform.position.x, _nextPlatform.transform.position.y + 1.5f, _nextPlatform.transform.position.z);

        if (_slider.value >= _slider.maxValue)
        {
            _sliderDirections = true;
        }
        else if (_slider.value <= _slider.minValue)
        {
            _sliderDirections = false;
        }

        if (_sliderDirections)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _slider.minValue, 1 * Time.deltaTime);
        }
        else
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _slider.maxValue, 1 * Time.deltaTime);
        }

        _middlePoint.position = new Vector3(_middlePoint.position.x, _middlePoint.position.y + _slider.value, _middlePoint.position.z);
       
        var pointList = new List<Vector3>();
        
        for (float ratio = 0; ratio <= 1; ratio += 1 / _lineVertexCount)
        {
            var tangent1 = Vector3.Lerp(_beginPoint.position, _middlePoint.position, ratio);
            var tangent2 = Vector3.Lerp(_middlePoint.position, _endPoint.position, ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }
        
        _pointsList = pointList.ToArray();
        _lineRenderer.positionCount = pointList.Count;
        _lineRenderer.SetPositions(pointList.ToArray());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RabbitJunglePlatformComponent platform))
        {
            _currentPlatform = platform.gameObject;
            _nextPlatform = RabbitJungleGameManager.PlatformsList[RabbitJungleGameManager.PlatformsList.IndexOf(_currentPlatform) + 1];

            transform.parent = _currentPlatform.transform;
            platform.OnUse();
        }
        if (other.TryGetComponent(out RabbitJungleEggsComponent egg))
        {
            egg.transform.DOScale(Vector3.zero, 0.25f)
                .OnComplete(() => 
                {
                   ParticleSystemRenderer tempParticle =  Instantiate(_eggGetEffect.GetComponent<ParticleSystemRenderer>(), egg.transform.position, Quaternion.identity);
                    tempParticle.material = egg.GetComponent<MeshRenderer>().material;
                    RabbitJungleGameManager.rabbitJungleScore += 1;
                    Destroy(_eggGetEffect.gameObject);
                });
        }
        if (other.TryGetComponent(out RabbitJungleBeeComponent bee))
        {
            canDo = true;
            RabbitJunglePlatformComponent.playerDeath?.Invoke();
        }
    }
}
