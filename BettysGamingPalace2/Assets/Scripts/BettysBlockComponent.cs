using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BettysBlockComponent : MonoBehaviour
{
    public Material material;

    private GameObject _effect;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        _effect = Resources.Load("Effects/Effect") as GameObject;
    }

    private void OnDestroy()
    {
        GameObject newEffect = Instantiate(_effect, transform.position,Quaternion.identity);
        newEffect.GetComponent<ParticleSystemRenderer>().material = material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BettysDeadLineComponent deadLine))
        {
            BettysGameController.showResult?.Invoke(false);
        }
        if (other.TryGetComponent(out BettysPlayerController palyer))
        {
            BettysGameController.showResult?.Invoke(false);
        }
    }
}
