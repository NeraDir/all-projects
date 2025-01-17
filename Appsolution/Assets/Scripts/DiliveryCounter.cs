using UnityEngine;
using UnityEngine.UI;

public class DiliveryCounter : MonoBehaviour
{
    [SerializeField]
    private DiliveryPlacesSpawner _diliveryPlacesSpawner;
    [SerializeField]
    private LevelResulter _levelResulter;
    [SerializeField]
    private ParticleSystem _diliveryParticle;
    [SerializeField]
    private int _diliveryInLevel;
    [SerializeField]
    private Text _diliveryCountText;


    private int _currentDiliveryCount;
    void Start()
    {
        _diliveryCountText.text = $"{_currentDiliveryCount}/{_diliveryInLevel}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _diliveryParticle.gameObject.transform.position = transform.position;
            _diliveryParticle.Play();
            RedrawDiliveryText();
            _diliveryPlacesSpawner.SetDiliveryPlace();
        }
    }

    public void RedrawDiliveryText()
    {
        _currentDiliveryCount++;

        _diliveryCountText.text = $"{_currentDiliveryCount}/{_diliveryInLevel}";

        if(_currentDiliveryCount == _diliveryInLevel)
        {
            _levelResulter.LevelWin();
        }
    }

    public void ResetDiliveryCount()
    {
        _currentDiliveryCount = 0;

        _diliveryCountText.text = $"{_currentDiliveryCount}/{_diliveryInLevel}";
    }


}
