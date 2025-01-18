using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BattleParticipant
{


    private void OnEnable()
    {
        PreparationforBattlePage.TapToScreenEvent += DisableItems;
    }
    private void OnDisable()
    {
        PreparationforBattlePage.TapToScreenEvent -= DisableItems;
    }


    public override void ShowItems()
    {
        PreparationforBattlePage.canClickToScreen = true;
        base.ShowItems();

    }

    public override void DisableItems()
    {
        PreparationforBattlePage.canClickToScreen = false;
        base.DisableItems();
    }
}
