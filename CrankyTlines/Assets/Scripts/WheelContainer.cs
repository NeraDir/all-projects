using UnityEngine;
using UnityEngine.UI;

public class WheelContainer : MonoBehaviour
{
    private Text _text;

    private int _value;

    private void Start()
    {
        _text = GetComponent<Text>();
        _value = Random.Range(1, 199);
        _text.text = _value.ToString();
    }

    public int GetValue() { return _value; }
}
