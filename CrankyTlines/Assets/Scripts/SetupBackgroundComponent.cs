using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetupBackgroundComponent : MonoBehaviour
{
    private List<Transform> _items;

    private Sequence _sequence;

    private Image _bgImage;

    [SerializeField] private Sprite[] _bgSprites;

    private IEnumerator Start()
    {
        _items = transform.GetComponentsInChildren<Transform>().ToList();
        _bgImage = _items[0].GetComponent<Image>();
        _items.Remove(_items[0]);
        
        foreach (var item in _items)
        {
            float rndRotate = Random.Range(-360, 360);
            Vector3 pos = item.position;
            item.rotation = Quaternion.Euler(0, 0, rndRotate);
            float rndMoveTime = Random.Range(0.75f, 2);
            _sequence = DOTween.Sequence();
            _sequence.Append(item.DOMoveY(pos.y + Random.Range(10, 20), rndMoveTime));
            _sequence.Append(item.DOMoveY(pos.y - Random.Range(10, 20), rndMoveTime));
            _sequence.SetLoops(-1, LoopType.Yoyo);
            yield return new WaitForSeconds(Random.Range(0.01f,0.1f));
        }
    }

    private void LateUpdate()
    {
        if(_bgImage != null)
            _bgImage.sprite = _bgSprites[TlineGameDataSaves.TlineCurrentBackgroundIndex];
    }
}
