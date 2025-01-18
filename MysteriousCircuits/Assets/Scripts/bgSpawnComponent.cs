using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bgSpawnComponent : MonoBehaviour
{
    [SerializeField]
    private Image _fallImage;

    [SerializeField]
    private Sprite[] _fallSprites;

    [SerializeField]
    private Transform[] _fallPositions;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Image newImage = Instantiate(_fallImage,new Vector3(
                    Random.Range(_fallPositions[0].position.x, _fallPositions[1].position.x),
                    _fallPositions[0].position.y,
                    _fallPositions[0].position.z),
                Quaternion.Euler(0,0,Random.Range(-360,360)));
            newImage.transform.SetParent(_fallPositions[0].parent);
            newImage.transform.SetSiblingIndex(0);
            newImage.sprite = _fallSprites[Random.Range(0, _fallSprites.Length)];
            newImage.transform.localScale = Vector3.one;
            Destroy(newImage.gameObject,5);
        }
    }
}
