using UnityEngine;

public class MenuPanelsOpenHandler : MonoBehaviour
{
    public void ChangePanelActiveState(GameObject targetPanel) => targetPanel.SetActive(!targetPanel.activeSelf);
}
