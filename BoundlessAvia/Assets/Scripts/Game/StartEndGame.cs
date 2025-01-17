using Game.Shop;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StartEndGame : MonoBehaviour
    {
        [SerializeField] private List<Transform> _cowSpawnPoses;
        [SerializeField] private List<Mesh> _cowsMeshes;
        [SerializeField] private List<Sprite> _cowsSprites;
        [SerializeField] private GameObject _cow;
        [SerializeField] private TMP_Text _cowCountText;
        private int _cowIndex;
        private int _cowsCollectNum;
        private int _correctCowCount;
        private int _correctCowCountLeft;

        [SerializeField] private Score _score;
        [SerializeField] private int _correctCowScoreAdd;
        [SerializeField] private int _correctCowScoreRemove;

        [SerializeField] private Money _money;
        [SerializeField] private TMP_Text _moneyGet;
        [SerializeField] private int _correctCowMoneyAdd;
        [SerializeField] private int _correctCowMoneyRemove;

        [SerializeField] private Image _cowGoalImage;

        [SerializeField] private GameObject _endPanel;

        private void Start()
        {
            _cowIndex = Random.Range(0, _cowsMeshes.Count);
            _cowsCollectNum = Random.Range(3, _cowSpawnPoses.Count / 2);
            _cowGoalImage.sprite = _cowsSprites[_cowIndex];
            SpawnCows();
        }

        private void SpawnCows()
        {
            List<Transform> spawnPoints = _cowSpawnPoses.GetRange(0, _cowSpawnPoses.Count);
            
            for(int i = 0; i < _cowsCollectNum; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(3, spawnPoints.Count)];
                var spawnedCow = Instantiate(_cow, spawnPoint.position, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                spawnedCow.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = _cowsMeshes[_cowIndex];
                _correctCowCount++;
                spawnPoints.Remove(spawnPoint);
            }

            _cowCountText.text = $"{_correctCowCountLeft}/{_correctCowCount}";

            if(spawnPoints.Count > 0)
            {
                var cowMeshesLast = _cowsMeshes.GetRange(0, _cowsMeshes.Count);
                cowMeshesLast.Remove(_cowsMeshes[_cowIndex]);

                int cowsSpawnCount = _cowSpawnPoses.Count - _cowsCollectNum;
                for(int i = 0; i < cowsSpawnCount; i++)
                {
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                    var spawnedCow = Instantiate(_cow, spawnPoint.position, Quaternion.Euler(0, Random.Range(-180f, 180f), 0));
                    spawnedCow.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = cowMeshesLast[Random.Range(0, cowMeshesLast.Count)];
                    spawnPoints.Remove(spawnPoint);
                }
            }
        }

        public void IsCorrectCow(Mesh mesh) 
        {
            if(mesh == _cowsMeshes[_cowIndex])
            {
                _score.AddScore(_correctCowScoreAdd);
                _money.AddMoney(_correctCowScoreRemove);
                _correctCowCountLeft++;
                _cowCountText.text = $"{_correctCowCountLeft}/{_correctCowCount}";
                if(_correctCowCountLeft >= _correctCowCount) End();
            }
            else
            {
                _score.DicreaseScore(_correctCowMoneyAdd);
                _money.DicreaseMoney(_correctCowMoneyRemove);
            }
        }

        public void End()
        {
            _moneyGet.text = _money.money.ToString();
            _endPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}