using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private SarcoComponent[] sarcoComponents;

    public delegate void SarcoComponentEventHandler();
    public static event SarcoComponentEventHandler SarcoComponent;

    [SerializeField]
    private TMP_Text _currentTruthCountShow;

    public static bool GameOver;

    public static int RecordOfTruth
    {
        get 
        {
            if (PlayerPrefs.HasKey("SarcosTruthRecord"))
            {
                return PlayerPrefs.GetInt("SarcosTruthRecord");
            }
            return 0;
        }
        set 
        {
            PlayerPrefs.SetInt("SarcosTruthRecord", value);
        }
    }

    public static int currentTruth;

    private void Awake()
    {
        currentTruth = 0;
        GameOver = false;
        SarcoComponent += GameController_SarcoComponent;
        GameController_SarcoComponent();
    }

    private void LateUpdate()
    {
        _currentTruthCountShow.text = "X " + currentTruth.ToString();
    }

    private void GameController_SarcoComponent()
    {
        int idnexGoodSarco =  Random.Range(0, sarcoComponents.Length);
        foreach (var item in sarcoComponents)
        {
            item.isGood = false;
        }
        sarcoComponents[idnexGoodSarco].isGood = true;
    }

    public static void OnUpdateGameControlling() 
    {
        if (SarcoComponent != null)
            SarcoComponent();
    }

    private void OnDestroy()
    {
        SarcoComponent -= GameController_SarcoComponent;
    }
}
