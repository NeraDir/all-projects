using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsBallsBonusGameControllerComponent : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPositions;

    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private TMP_Text _displayOne;

    [SerializeField]
    private TMP_Text _displayTwo;

    [SerializeField]
    private TMP_Text _displayThree;

    [SerializeField]
    private TMP_Text _displayFour;

    [SerializeField]
    private Material _outlineMat;

    [SerializeField]
    private TMP_Text[] _displayXes;

    [SerializeField]
    private GameObject _resultPage;

    [SerializeField]
    private Text _showWinX;

    [SerializeField]
    private Text _showRecordByX;

    [SerializeField]
    private Material[] _ballsColors;

    private List<int> xesList = new List<int>();

    public static List<int> xesBallsLister = new List<int>();

    private bool canSearch;

    private IEnumerator Start()
    {
        int spawningCount = 0;
        int rndX = Random.Range(1, 4);
        foreach (var txt in _displayXes) 
        {
            xesList.Add(rndX);
            xesBallsLister.Add(0);
            txt.text = "X" + rndX;
            rndX += Random.Range(1, 4);
        }
        while (spawningCount != StarsGameControllerComponent.ballsCount)
        {
            GameObject tempBall = Instantiate(_ball, new Vector3(Random.Range(_spawnPositions[0].position.x, _spawnPositions[1].position.x), _spawnPositions[0].position.y, _spawnPositions[0].position.z), _spawnPositions[0].rotation);
            List<Material> mete = new List<Material>();
            mete.Add(_outlineMat);
            mete.Add(_ballsColors[Random.Range(0, _ballsColors.Length)]);
            tempBall.GetComponent<MeshRenderer>().materials = mete.ToArray();
            spawningCount++;
            yield return new WaitForSeconds(0.25f);
        }
        canSearch = true;
    }

    private void LateUpdate()
    {
        _displayOne.text = xesBallsLister[0].ToString();
        _displayTwo.text = xesBallsLister[1].ToString();
        _displayThree.text = xesBallsLister[2].ToString();
        _displayFour.text = xesBallsLister[3].ToString();
        _showRecordByX.text = (StarsGameControllerComponent.record * GetResultX()).ToString();
        _showWinX.text = "X" + GetResultX().ToString();
        if (canSearch)
            if (FindObjectOfType<StarsBonusBallComponent>() == null)
                _resultPage.SetActive(true);
    }

    public void OnClickRestartGame() 
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickMenu() 
    {
        if (StarsGameMenuComponent._starsGameBestScore < StarsGameControllerComponent.record * GetResultX())
        {
            StarsGameMenuComponent._starsGameBestScore = (StarsGameControllerComponent.record * GetResultX());
        }
        SceneManager.LoadScene("GameMenu");
    }

    private int GetResultX() 
    {
        var xer = xesList.OrderByDescending(x => x);
        int resultX = xer.ElementAt(0);
        return resultX;
    }
}
