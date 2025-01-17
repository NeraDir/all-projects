using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BeastEnergyPlayerSkins 
{
    [SerializeField] private MeshRenderer[] meshRenderers;

    [SerializeField] private BeastEnergySkinMaterials[] meshMaterials;

    public void SetSkin() 
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].materials = meshMaterials[i].meshMaterials;
        }
    }
}

[System.Serializable]
public class BeastEnergySkinMaterials 
{
    public Material[] meshMaterials;
}

public class BeastEnergyPlayerControllerManager : MonoBehaviour
{
    public static UnityEvent beastEnergyDeath = new UnityEvent();

    [SerializeField] private Animator _beastEnergyPlayerAnimator;

    [SerializeField] private float _beastEnergyPlayerMove;

    [SerializeField] private float _beastEnergySphereGroundCheckRadius;

    [SerializeField] private LayerMask _beastEnergyCheckLayer;

    private Rigidbody _beastEnergyBody;

    private bool _beastEnergyPlayerOnGround;

    private bool _beastEnergyPlayerDoOtherMotions;

    private Coroutine _beastEnergyCouroutine;

    private void Start()
    {
        _beastEnergyPlayerDoOtherMotions = false;
        _beastEnergyBody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (!BeastEnergyGameManager.beastEnergyRunLaunched)
            return;
        _beastEnergyBody.velocity = new Vector3(_beastEnergyBody.velocity.x, _beastEnergyBody.velocity.y, _beastEnergyPlayerMove);
        _beastEnergyPlayerOnGround = Physics.CheckSphere(transform.position, _beastEnergySphereGroundCheckRadius, _beastEnergyCheckLayer);
        if (_beastEnergyPlayerOnGround && !_beastEnergyPlayerDoOtherMotions)
        {
            SetBeastAnimationState(1,false);
        }
    }

    public void SetBeastAnimationState(int index,bool state) 
    {
        _beastEnergyPlayerDoOtherMotions = state;
        _beastEnergyPlayerAnimator.SetInteger("BeastEnergyPlayerState", index);
        AnimationClip[] clips = _beastEnergyPlayerAnimator.runtimeAnimatorController.animationClips;
        float currentAniamtionTime = clips[index].length;
        if (_beastEnergyPlayerDoOtherMotions)
        {
            if (_beastEnergyCouroutine != null)
            {
                StopCoroutine(_beastEnergyCouroutine);
                _beastEnergyCouroutine = null;
            }
                
            _beastEnergyCouroutine = StartCoroutine(SetDefault(currentAniamtionTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BeastEnergyCoinManager coin))
        {
            coin.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => { Destroy(coin.gameObject); BeastEnergyGameManager.beastEnergyCoinsCount++; });
        }
        if (other.TryGetComponent(out BeastEnergyRoadManager road))
        {
            BeastEnergyGameManager.beastEnergyRoadTriggererd?.Invoke();
            Destroy(road.gameObject, 7);
        }
        if (other.TryGetComponent(out BeastEnergyTrapManager trap))
        {
            beastEnergyDeath?.Invoke();
        }
    }

    private IEnumerator SetDefault(float value) 
    {
        yield return new WaitForSeconds(value);
        _beastEnergyPlayerDoOtherMotions = false;
    }

    public bool GetState() 
    {
        return _beastEnergyPlayerOnGround;
    }
}
