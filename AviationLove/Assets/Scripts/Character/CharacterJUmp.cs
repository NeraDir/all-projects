using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterJUmp : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] _reactiveBurns;

    [SerializeField]
    private AnimationCurve _animationCurve;

    [SerializeField]
    private Transform _jumpingTarget;

    public Vector3 lerOffset;

    public float lerpTime = 3;

    private float _timer;

    private bool _jumping;

    [SerializeField]
    private ObjectFollow followe;

    [SerializeField]
    private GameObject Falleffect;

    [SerializeField]
    private CharacterMovement chMove;

    private Vector3 targetBegin;

    [SerializeField]
    private Image showStrenger;

    private float currentval;

    [SerializeField]
    private LineRenderer lineRenderer;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        if (_jumping)
        {
            foreach (var item in _reactiveBurns)
            {
                item.startLifetime = 0.63f;
            }

            _timer += Time.deltaTime;
            float lerpRation = _timer / lerpTime;

            Vector3 positionOffset = _animationCurve.Evaluate(lerpRation) * lerOffset;

            lineRenderer.SetPosition(0, targetBegin);
            lineRenderer.SetPosition(1, new Vector3(0, positionOffset.y,targetBegin.z + ((_jumpingTarget.position.z - targetBegin.z) / 2)));
            lineRenderer.SetPosition(2, _jumpingTarget.position);


            transform.position = Vector3.Lerp(targetBegin, _jumpingTarget.position, lerpRation) + positionOffset;
            if (_timer > lerpTime)
            {
                Instantiate(Falleffect, _jumpingTarget.transform.position, Falleffect.transform.rotation);
                foreach (var item in _reactiveBurns)
                {
                    item.startLifetime = 0;
                }
                _timer = lerpTime;
                _jumping = false;
                followe.enabled = true;
                chMove.enabled = true;
                _timer = 0;
                currentval = 0;
                showStrenger.fillAmount = 0;
            }

        }

        if (Input.GetMouseButtonDown(0))
        {

        }
        else if (Input.GetMouseButton(0))
        {
            if (_jumping)
                return;
            currentval += 30 * Time.deltaTime;
            if (currentval > 22)
            {
                currentval = 22;
            }
            showStrenger.fillAmount = Mathf.MoveTowards(showStrenger.fillAmount, currentval / 22, 10 * Time.deltaTime);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_jumping)
                return;
            _jumping = true;
            Keyframe[] keys = _animationCurve.keys;
            Keyframe keyFrame = keys[1];
            keyFrame.value = currentval;
            keys[1] = keyFrame;
            _animationCurve.keys = keys;
            followe.enabled = false;
            chMove.enabled = false;
            targetBegin = transform.position;
        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SlowerTrigger>(out SlowerTrigger slowe))
        {
            while (Time.timeScale > 0.5f)
            {
                Time.timeScale -= 0.1f;
            }
            Invoke(nameof(ReturnDefaultTime), 3);
        }
    }

    private void ReturnDefaultTime()
    {
        Time.timeScale = 1;
    }
}
