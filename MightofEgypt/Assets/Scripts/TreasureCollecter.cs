using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TreasureCollecter : MonoBehaviour
{
    private const string _treasureTag = "Treasure";

    [SerializeField] private TMP_Text _treasuresLeftText;

    [SerializeField] private int _treasuresLeft = 5;

    public UnityEvent OnTreasureCollected, OnAllTreasuresCollected;

    private void Awake() {
        UpdateTreasureLeftText();
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case _treasureTag:
                CollectTreasure(other.gameObject);
                break;
        }
    }

    public void UpdateTreasureLeftText() => _treasuresLeftText.text = $"Treasures left: {_treasuresLeft}";

    public void CollectTreasure(GameObject treasure) {
        if (_treasuresLeft > 1) {
            _treasuresLeft--;
            Destroy(treasure);
        }
        else OnAllTreasuresCollected.Invoke();
        OnTreasureCollected.Invoke();
    }
}
