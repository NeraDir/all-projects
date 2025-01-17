using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoarTableManager : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Sprite[] _crystallSprites;

    [SerializeField]
    private Image[] _crystallImages;

    public void SetNewCrystalls() 
    {
        foreach (var item in _crystallImages)
        {
            item.sprite = _crystallSprites[Random.Range(0, _crystallSprites.Length)];
        }
    }

    private bool _isClicked = false;

    public void CanClickAgain() 
    {
        _isClicked = false;
    }

    public void ReFill() 
    {
        if (_isClicked)
            return;
        _isClicked = true;
        _animator.SetBool("tableCrystallState", true);
    }

    public void SetAnimationToFIll() 
    {
        _animator.SetBool("tableCrystallState", false);
    }

    public Image[] GetImagesToCheckIn() 
    {
        return _crystallImages;
    }
}
