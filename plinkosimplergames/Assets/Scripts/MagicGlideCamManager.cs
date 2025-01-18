using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicGlideCamManager : MonoBehaviour
{
    [SerializeField] private Transform _magicGlideFollowTarget;
    [SerializeField] private Vector3 _magicGlideFollowOffset;
    [SerializeField] private float _magicGlideFollowSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, _magicGlideFollowTarget.position.y + _magicGlideFollowOffset.y, _magicGlideFollowTarget.position.z + _magicGlideFollowOffset.z), _magicGlideFollowSpeed * Time.deltaTime);
    }
}
