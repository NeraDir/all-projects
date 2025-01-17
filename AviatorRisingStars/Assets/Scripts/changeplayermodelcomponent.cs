using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class changeplayermodelcomponent : MonoBehaviour
{
    public static UnityEvent<string,string> playerModel = new UnityEvent<string,string>();

    [SerializeField]
    private string _modelKey;

    [SerializeField]
    private string _roadType;

    public void Use() 
    {
        playerModel?.Invoke(_modelKey, _roadType);
        transform.DOScale(Vector3.zero,0.25f).OnComplete(() => Destroy(gameObject));
    }
}
