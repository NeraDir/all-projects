using TMPro;
using UnityEngine;

public class PlatformsMove : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public float totalValue;

    private float _moveSpeed = 0.5f;

    public void Init(string value,float total)
    {
        totalValue = total;
        _text.text = value;
    }

    private void LateUpdate()
    {
        transform.position += new Vector3(0, -1f, 0) * (_moveSpeed + TigerClawsGameController.PlatformSpeedMultiplayer);
    }

    public void OnPlaced(float total)
    {
        _text.text = "";
        if (totalValue != total)
            TigerClawsGameController.onGameEnd?.Invoke(false);
    }
}
