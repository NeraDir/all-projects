using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuPage : MonoBehaviour
{
    public TMP_Text coinsTextDisplay;


    public GameObject levelPanel;
    public GameObject howToPlayPanel;
    public GameObject shopPanel;

    private Animator mAnimator;


    private void OnEnable()
    {
        mAnimator = GetComponent<Animator>();
        coinsTextDisplay.text = Configs.allCoinsCount.ToString();
        PlayOpenAnimation();

        if (!PlayerPrefs.HasKey("HowKey"))
        {
            PlayerPrefs.SetInt("HowKey", 1);
            Invoke(nameof(OpenHowToPlayPage), 2f);
        }
    }



    public void OpenLevelsPage()
    {
        //levelPanel.SetActive(true);

        PlayCloseAnimation();
        StartCoroutine(OpenPage(levelPanel));
        //Invoke(nameof(HidePage));
    }
    public void OpenHowToPlayPage()
    {
        howToPlayPanel.SetActive(true);
    }
    public void OpenShopPage()
    {
        //shopPanel.SetActive(true);
        PlayCloseAnimation();
        StartCoroutine(OpenPage(shopPanel));
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public void CloseHowToPlay()
    {
        howToPlayPanel.gameObject.SetActive(false);
    }


    public void PlayOpenAnimation()
    {
        mAnimator.SetInteger("key", 0);
    }
    public void PlayCloseAnimation()
    {
        mAnimator.SetInteger("key", 1);
    }

    public void HidePage()
    {
        gameObject.SetActive(false);
    }
    
    private IEnumerator OpenPage(GameObject nextPage)
    {

        float waitTime = mAnimator.runtimeAnimatorController.animationClips[1].length;


        yield return new WaitForSeconds(waitTime);

        gameObject.SetActive(false);
        nextPage.SetActive(true);
    }
}
