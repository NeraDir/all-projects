using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public List<CellController> AllCells = new();
    public List<Pazzle> AllPazzles = new();

    private void Start()
    {
        foreach (var item in AllPazzles)
        {
            item.InitPazzle();
        }

        int rndPazzle = Random.Range(0, AllPazzles.Count);

        GameManager.Instance.CountAllPieces = AllPazzles[rndPazzle].AllPieces.Count;
        GameManager.Instance.SetFullImage(AllPazzles[rndPazzle].FullImageSprite);

        for (int i = 0; i < AllCells.Count; i++)
        {
            AllCells[i].draggableItem.InitCell(AllPazzles[rndPazzle].AllPieces[i]);
        }
    }
}

[System.Serializable]
public struct PieceStructure
{
    public Sprite sprite;
    public int ID;
}

[System.Serializable]
public class Pazzle
{
    public Sprite FullImageSprite;
    public List<Sprite> SpritePieces = new();
    public List<PieceStructure> AllPieces = new();

    public void InitPazzle()
    {
        for (int i = 0; i < SpritePieces.Count; i++)
        {
            PieceStructure buff = new PieceStructure
            {
                sprite = SpritePieces[i],
                ID = i
            };

            AllPieces.Add(buff);
        }

        ShufflePieces();
    }

    public void ShufflePieces()
    {
        System.Random RND = new System.Random();

        AllPieces = AllPieces.OrderBy(n => RND.Next()).ToList();
    }
}