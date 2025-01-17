using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfModel : MonoBehaviour
{
    private Animator eAnimator;
    [SerializeField]
    private HeadSweetie headSweetie;

    private void OnEnable()
    {
        eAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (headSweetie != null)
        {
            headSweetie.transform.Rotate(0, 1, 0);
        }
    }

    public void SetFallAnimatoionState()
    {
        eAnimator.SetInteger("animation_clip_index", 1);
        headSweetie.StartMove();
        headSweetie = null;
    }
}
