using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PimoBonusGameController : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPosition;

    [SerializeField]
    private GameObject _pack;

    [SerializeField]
    private GameObject _result;

    [SerializeField]
    private TMP_Text[] _score;

    [SerializeField]
    private TMP_Text _balls;


    private IEnumerator Start()
    {

        while (PimoGameController._ballsCount > 0)
        {
            yield return new WaitForSeconds(3);
            Instantiate(_pack, _spawnPosition.position, Quaternion.identity);
        }
        _result.SetActive(true);
    }

    private void LateUpdate()
    {
        if (PimoGameController._scoreCount > PimoGameController.BallsMaxCount)
        {
            PimoGameController.MaxScore = PimoGameController._scoreCount;
        }
        foreach (var item in _score)
        {
            item.text = PimoGameController._scoreCount.ToString();
        }
        _balls.text = "x" + PimoGameController._ballsCount.ToString();
    }

    public void OnClickMenu()
    {
        PimoGameController._scoreCount = 0;
        PimoGameController._ballsCount = 0;
        Scene nextScene = SceneManager.CreateScene("PlimoMayhemMenuScene");
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(nextScene);
        GameObject menuCanvas = Resources.Load("Prefabs/Menu") as GameObject;
        Instantiate(menuCanvas);
        SceneManager.UnloadScene(currentScene);
    }
}
