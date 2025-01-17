using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new CardsData",menuName = "new Datas")]
public class SunsOfEgypCardDatas : ScriptableObject
{
   public List<SunsOfEgyptCardData> cardsDatas = new List<SunsOfEgyptCardData>();
}

[System.Serializable]
public class SunsOfEgyptCardData
{
    public int cardDamage;
    public int cardHealth;
    public Sprite cardSprite;
}
