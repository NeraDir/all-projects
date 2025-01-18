using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalStone : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> allParts;

    [SerializeField]
    private int multiplyValue;
    [SerializeField]
    private int powerPrice;

    [SerializeField]
    private TMP_Text multText;
    [SerializeField]
    private TMP_Text powerPriceText;

    private void OnEnable()
    {
        multText.text = multiplyValue.ToString("X#");
        powerPriceText.text = powerPrice.ToString("-#");
    }

    private Collider lastCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TigerEntityColliderManager tiger))
        {
            if (lastCollider != other)
            {
                lastCollider = other;

                for (int i = 0; i < allParts.Count; i++)
                {
                    Destroy(multText.gameObject);
                    allParts[i].AddComponent<Rigidbody>();
                    Destroy(allParts[i], Random.Range(2, 4));

                }
            }
        }
    }

    public int GetMultiplyValue()
    {
        return multiplyValue;
    }
    public int GetPowerPrice()
    {
        return powerPrice;
    }
}
