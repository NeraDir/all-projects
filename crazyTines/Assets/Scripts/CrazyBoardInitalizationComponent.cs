using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrazyBoardInitalizationComponent : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> _packOfallSprites = new List<Sprite>();

    [SerializeField]
    private CrazyTableCellComponent _cellPrefab;

    [SerializeField]
    private Transform _showCombinationPos;

    [SerializeField]
    private LevelDatas _levelDatas;

    public List<Sprite> _packOfSpritesCombination = new List<Sprite>();

    private List<CrazyTableCellComponent> _cellsOnTable = new List<CrazyTableCellComponent>();

    private void Awake()
    {
        _cellsOnTable = GetComponentsInChildren<CrazyTableCellComponent>().ToList();
        SetRndDifferentCombination();
    }

    private void Start()
    {
        
        SetCombintaion();
    }

    private void SetCombintaion()
    {;
        string levelString = _levelDatas.levelPattern[GameSavesData.SelectedLevelIndex >= _levelDatas.levelPattern.Count ? Random.Range(0,_levelDatas.levelPattern.Count): GameSavesData.SelectedLevelIndex].Replace(" ", "");
        for (int i = 0; i < levelString.Length; i++)
        {
            if (levelString[i] == '$')
            {
                int index = Random.Range(0, _packOfSpritesCombination.Count);
                _cellsOnTable[i].Init(_packOfSpritesCombination[index], true);
                CrazyGameControllerComponent.needCombinations.Add(_packOfSpritesCombination[index]);
            }
            else
            {
                _cellsOnTable[i].Init(_packOfallSprites[Random.Range(0, _packOfallSprites.Count)],true);
            }
        }
        CrazyGameControllerComponent.OnShowCombintation?.Invoke();
    }

    private void SetRndDifferentCombination()
    {
        int countOfDifferentSprite = Random.Range(1, 3);
        for (int i = 0; i < countOfDifferentSprite; i++)
        {
            int rndINdex = Random.Range(0, _packOfallSprites.Count);
            _packOfSpritesCombination.Add(_packOfallSprites[rndINdex]);
            _packOfallSprites.Remove(_packOfallSprites[rndINdex]);
        }
        
    }
}
