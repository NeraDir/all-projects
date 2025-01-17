using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachineMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _machineBoxerGameInfoScreen;

    [SerializeField]
    private GameObject _machineBoxerGameMenu;

    [SerializeField]
    private Text _machineBoxerGameBalance;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (!PlayerPrefs.HasKey("MachineBoxerGameInfoScreenShowedSaveKey"))
        {
            _machineBoxerGameInfoScreen.SetActive(true);
            _machineBoxerGameMenu.SetActive(false);
            PlayerPrefs.SetInt("MachineBoxerGameInfoScreenShowedSaveKey",1);
        }
        
        StartCoroutine(AddBalance());
    }

    private void LateUpdate()
    {
        _machineBoxerGameBalance.text = MachineGameDataSaver.MachineBoxerPlayerPlayBalance.ToString("0");
    }

    private IEnumerator AddBalance() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            MachineGameDataSaver.MachineBoxerPlayerPlayBalance += 10;
        }
    }
}
