using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class GamingSlotConfig
{
    public string lineName;

    public GamingSlotPlaceTrigger[] line;

    public List<GamingSlotPlaceTrigger> winningPlaces = new List<GamingSlotPlaceTrigger>();

    public GameObject linerShower;

    public GameObject wonShower;
    public void GetWinningLines()
    {
        for (var i = 0; i < line.Length; i++)
        {
            if (line[i].GetCurrentTriggeredSlot() != null)
            {
                if (i == 0)
                {
                    winningPlaces.Add(line[i]);
                }
                else
                {
                    if (line[i].GetCurrentTriggeredSlot().slotIndex == line[0].GetCurrentTriggeredSlot().slotIndex)
                    {
                        winningPlaces.Add(line[i]);
                    }
                }
            }
        }

        if (winningPlaces.Count >= 3)
        {
            foreach (var win in winningPlaces)
            {
                win.currentContainer.GetComponentInChildren<Animator>().SetBool("casted", true);
                GamngSlotRotating.won += (int)win.currentContainer.slotPrice;
            }
            wonShower.SetActive(true);
            linerShower.SetActive(true);
        }
    }

    public void SetDefault() 
    {
        foreach (var win in winningPlaces)
        {
            win.currentContainer.GetComponentInChildren<Animator>().SetBool("casted", false);
        }
        winningPlaces.Clear();
        linerShower.SetActive(false);
        wonShower.SetActive(false);
    }
}
