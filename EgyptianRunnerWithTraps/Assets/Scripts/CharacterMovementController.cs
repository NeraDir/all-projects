using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    [SerializeField]
    private Animator _characterAnimator;

    [SerializeField]
    private Transform _crystallsToMovePosition;

    [SerializeField]
    private SkinnedMeshRenderer _characterMesh;

    [SerializeField]
    private Material[] _characterSkins;

    [SerializeField]
    private GameObject _finishPage;

    [SerializeField]
    private GameObject _nextButton;

    private bool isLoose;

    private void Start()
    {
        isLoose = false;
        _characterMesh.material = _characterSkins[GameManager.egyptianSelectedSkinValue];
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(0) && !GameManager.gameEnded)
        {
            transform.position += new Vector3(0, 0, 1) * 4.25f * Time.deltaTime;
            _characterAnimator.SetBool("CharacterAnimationState", true);
        }
        else
        {
            _characterAnimator.SetBool("CharacterAnimationState", false);
        }
        _nextButton.SetActive(!isLoose);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CrystallRocksComponents crystallRock))
        {
            crystallRock.Use(_crystallsToMovePosition);
        }
        else if (other.TryGetComponent(out TrapComponent trap))
        {
            GameManager.gameEnded = true;
            isLoose = true;
            _finishPage.SetActive(true);
        }
        else if (other.TryGetComponent(out FinishComponent finish))
        {
            GameManager.gameEnded = true;
            isLoose = false;
            _finishPage.SetActive(true);
        }
    }
}
