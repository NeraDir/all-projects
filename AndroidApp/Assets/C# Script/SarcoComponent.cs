using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SarcoComponent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    private Sprite _sarcoOpenedGood;

    [SerializeField]
    private Sprite _sarcoOpenedBad;

    private Image _sarcoImage;

    public bool isGood;

    private Sprite _sarcoDefaultSprite;

    private void Awake()
    {
        _sarcoImage = GetComponent<Image>();
        GameController.SarcoComponent += UpdateSelf;
        _sarcoDefaultSprite = _sarcoImage.sprite;
    }

    public void UpdateSelf() 
    {
        _sarcoImage.sprite = _sarcoDefaultSprite;
        GameController.GameOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController.GameOver)
            return;
        if (isGood)
        {
            _sarcoImage.sprite = _sarcoOpenedGood;
            GameController.currentTruth++;

        }
        else
        {
            _sarcoImage.sprite = _sarcoOpenedBad;
            StartCoroutine(ReloadScene());

        }
        Invoke(nameof(Updating), 1);
        GameController.GameOver = true;
        
    }

    private void Updating() 
    {
        GameController.OnUpdateGameControlling();
    }

    private IEnumerator ReloadScene() 
    {
        yield return new WaitForSeconds(1);
        if (GameController.currentTruth > GameController.RecordOfTruth)
        {
            GameController.RecordOfTruth = GameController.currentTruth;
        }
        SceneManager.LoadScene(1);
    }

    private void OnDestroy()
    {
        GameController.SarcoComponent -= UpdateSelf;
    }
}
