using UnityEngine;
using TMPro;
using System.Collections;

public class RocketScript : MonoBehaviour
{
    [Header("Rocket Loagic")]
    private Transform _targetToMove;

    [SerializeField]
    private float _rocketMovementSpeed;

    [SerializeField]
    private float _rocketRotationSpeed;

    private Rigidbody _rocketBody;

    [Space(10)]
    [Header("VISUAL EFFECTS")]
    [SerializeField]
    private GameObject _boomEffect;

    [SerializeField]
    private GameObject _fireEffect;

    [SerializeField]
    private Transform _fireSpawnPosition;

    [SerializeField]
    private TMP_Text _livingTimeShow;

    [SerializeField]
    private Vector3 _liviningTimeOffset;

    private float _timeToSpawnFire;

    public bool isFaller;

    private void Start()
    {
        _rocketBody = GetComponent<Rigidbody>();
        if (!isFaller)
        {
            _targetToMove = FindObjectOfType<PlanerController>().transform;
            _livingTimeShow.transform.parent = null;
        }
       
        StartCoroutine(Living());
    }

    private void LateUpdate()
    {
        if (isFaller)
            return;
        _timeToSpawnFire += Time.deltaTime;
        _livingTimeShow.transform.position = Vector3.Lerp(_livingTimeShow.transform.position, transform.position + _liviningTimeOffset, 150 * Time.deltaTime);
        if (_timeToSpawnFire >= 0.05f)
        {
            Instantiate(_fireEffect, _fireSpawnPosition.position, _fireSpawnPosition.rotation);
            _timeToSpawnFire = 0;
        }
    }

    private IEnumerator Living() 
    {
        float livingTime = 10;
        while (livingTime > 0) 
        {
            livingTime--;
            if (_livingTimeShow != null)
            {
                _livingTimeShow.text = livingTime.ToString("0");
            }
           
            yield return new WaitForSeconds(1);
        }
        Die();
    }

    private void FixedUpdate()
    {
        if (isFaller)
            return ;
        if (_targetToMove != null) 
        {
            Vector2 direction = (Vector2)_targetToMove.position - (Vector2)transform.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.forward).z;

            _rocketBody.angularVelocity = new Vector3(0, 0, -rotateAmount * _rocketRotationSpeed);

            _rocketBody.velocity = transform.forward * _rocketMovementSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IRocketDieble planer))
        {
            if (!isFaller && !other.TryGetComponent(out ShipMovement ship))
            {
                Die();
            }
            else if(isFaller && other.TryGetComponent(out ShipMovement fdf))
            {
                Die();
                GameManager.destroyedShipsCurrentValue++;
                Destroy(fdf.gameObject);
            }

        }

        if (other.TryGetComponent(out WaterScript water))
        {
            Die();
        }
    }

    private void Die() 
    {
        if (_livingTimeShow != null)
        {
            Destroy(_livingTimeShow.gameObject);
        }
        
        GameManager.NoiseCam();
        Instantiate(_boomEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
