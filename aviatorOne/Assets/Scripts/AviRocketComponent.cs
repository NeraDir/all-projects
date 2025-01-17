using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AviRocketComponent : MonoBehaviour, IRocketDestroy
{
    private Transform _target;
    [SerializeField]
    private float _moveSpeed = 20f;
    [SerializeField]
    private float _rotationSpeed = 6f;
    private Rigidbody _rocketBody;
    private TMP_Text _lifeTimeShow;
    private float _livingTime;

    [SerializeField]
    private GameObject _effectBoom;

    [SerializeField]
    private GameObject _effectTrail;

    [SerializeField]
    private TMP_Text _textTMP;

    public void Start()
    {
        _livingTime = 9f;
        _target = FindObjectOfType<AviaPlaneController>().transform;
        _rocketBody = GetComponent<Rigidbody>();
        _lifeTimeShow = Instantiate(_textTMP);
        _lifeTimeShow.GetComponent<AviaCamFollowe>().SetTarget(transform);
        StartCoroutine(Effect());
        StartCoroutine(Destroyer());
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Destroyer()
    {
        while (_livingTime > 0)
        {
            _livingTime -= Time.deltaTime;
            _lifeTimeShow.text = _livingTime.ToString("0.0") + "s";
            yield return null;
        }
        Instantiate(_effectBoom, transform.position, Quaternion.identity);
        Destroy(_lifeTimeShow.gameObject);
        Destroy(gameObject);
    }

    private IEnumerator Effect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            GameObject tempEffect = Instantiate(_effectTrail,_effectTrail.transform.position,_effectTrail.transform.rotation);
            tempEffect.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (AviaPlaneController.isEnd)
            return;
        if (_target != null)
        {
            Vector2 direction = (Vector2)_target.position - (Vector2)transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rocketBody.angularVelocity = new Vector3(0, 0, -rotateAmount * _rotationSpeed);
            _rocketBody.velocity = transform.up * _moveSpeed;
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AviaPlaneController avi))
        {
            Instantiate(_effectBoom, transform.position, Quaternion.identity);
            Destroy(_lifeTimeShow.gameObject);
            Destroy(gameObject);
            avi.GetDamage();
        }
        if (other.TryGetComponent(out IRocketDestroy detr))
        {
            Instantiate(_effectBoom, transform.position, Quaternion.identity);
            Destroy(_lifeTimeShow.gameObject);
            Destroy(gameObject);
        }
    }
}
