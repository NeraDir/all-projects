using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewardContainerSubDisplay : MonoBehaviour {
    [SerializeField] private Image[] _rewardImage;
    [SerializeField] private TMP_Text[] _rewardText;

    [SerializeField] private GameObject _verticalContainer;
    [SerializeField] private GameObject _horizontalContainer;
    public void Init(string rewardText,Sprite rewardSprite,bool isHorizontal) {
        if(isHorizontal)
            _horizontalContainer.SetActive(true);
        else
            _verticalContainer.SetActive(true);
        
        foreach (var item in _rewardImage) {
            item.sprite = rewardSprite;
        }

        foreach (var item in _rewardText) {
            item.text = rewardText;
        }
        
    }
}