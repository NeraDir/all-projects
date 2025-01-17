using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Text _textEnemy;


    private void Start()
    {
        SetLevel(CollisionController.NextLevel);
    }

    private void SetLevel(bool newLevel)
    {
        _textEnemy = GetComponentInChildren<Text>();
/*        if (newLevel)
        {            
            _maxHP = float.Parse(_textEnemy.text) + hpNewLevel;
            _textEnemy.text = _maxHP.ToString();
        }
        else
        {
            _maxHP = float.Parse(_textEnemy.text);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        

    }
}
