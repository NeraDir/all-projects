using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RevengerMainLoading : MonoBehaviour
{
    public List<string> revengePlayerSettingsKeys;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("RevengerLoadingScene");
    }

   
}
