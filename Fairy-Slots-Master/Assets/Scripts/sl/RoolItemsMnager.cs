using UnityEngine;

public class RoolItemsMnager : MonoBehaviour
{
    public MainConteiner[] cont;

    public Animator myAnimator;

    public SlotButtonManager slRot;

    public void setInit() 
    {
        for (int i = 0; i < cont.Length; i++)
        {
            cont[i].INIT();
        }

    }

    public void setSecondAnimation()
    {
        for (int i = 0; i < slRot.config.Length; i++)
        {
            slRot.config[i].GetWinningLines();
        }

        SlotButtonManager.isLooping = false;
    }
}
