using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevengerLoading : MonoBehaviour
{
    public TMP_Text displayLoadingValue;

    private IEnumerator Start()
    {
        float loadingValue = 0f;
        while (loadingValue < 100)
        {
            loadingValue = Mathf.MoveTowards(loadingValue, 101, 20 * Time.deltaTime);
            displayLoadingValue.text = loadingValue.ToString("0.00")+"%";
            yield return null;
        }
        SceneManager.LoadScene("RevengerMenu");
    }
}
