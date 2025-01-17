using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuComponent : MonoBehaviour
{
	public TMP_Text showBestDistance;

private void Start(){
	showBestDistance.text = GameManager.BestScore.ToString();
}

   public void onClickPlay(){
SceneManager.LoadScene("SampleScene");
}
public void onClickExit(){
Application.Quit();
}
}
