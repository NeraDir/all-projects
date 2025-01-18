using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LuckyPlayerControllerComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject luckyJetPackEffect;

    [SerializeField]
    private Transform[] luckyEffectPositions;

    [SerializeField]
    private Text luckyFuelDisplay;

    [SerializeField]
    private InputField luckyInput;

    [SerializeField]
    private Image luckyFuelBarDisplay;

    private float luckyfuelValue = 100;

    private int luckyAnswer;

    public static UnityEvent luckyFuelEnd = new UnityEvent();

    private IEnumerator Start()
    {
        luckyAnswer = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            foreach (var item in luckyEffectPositions)
            {
                Instantiate(luckyJetPackEffect, item.position, item.rotation);
            }
        }
    }

    private void LateUpdate()
    {
        luckyAnswer = System.Convert.ToInt32(luckyInput.text);
        if (luckyfuelValue <= 0)
        {
            luckyFuelEnd?.Invoke();
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            transform.position += transform.forward * 15 * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-45, 0, 180), 8 * Time.deltaTime);
            luckyfuelValue -= 0.1f;
            luckyFuelBarDisplay.fillAmount = Mathf.Lerp(luckyFuelBarDisplay.fillAmount, luckyfuelValue / 100, 8 * Time.deltaTime);
            luckyFuelDisplay.text = luckyfuelValue.ToString("0") +"/"+ 100.ToString("0");
            return;
        }
        transform.position += transform.forward * 7.5f * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(35, 0, 180), 8 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LuckyGameTriggerPiece triggerPiece))
        {
            LuckyGameControllerComponent.LuckyGamePlayerTriggeredLevelPiece?.Invoke();
            triggerPiece.LuckyDestroy();
        }
        if(other.TryGetComponent(out LuckyStarComponent star))
        {
            star.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { Destroy(star.gameObject); LuckyGameControllerComponent.LuckyGameCurrentScore++; });
        }
        if (other.TryGetComponent(out LuckyAsteroidComponent asteroid))
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            LuckyGameControllerComponent.LuckyPlayerHeartsCount--;
        }
        if (other.TryGetComponent(out LuckyRingComponent ring))
        {
            if (ring.luckyTotalSum == luckyAnswer)
            {
                ring.transform.parent.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { Destroy(ring.transform.parent.gameObject); LuckyGameControllerComponent.LuckyGameCurrentScore += Random.Range(1,5); });
            }
            else
            {
                ring.transform.parent.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { Destroy(ring.transform.parent.gameObject); LuckyGameControllerComponent.LuckyPlayerHeartsCount--; });
                
            }
        }
        if (other.TryGetComponent(out LuckyFuelComponent fuelComponent))
        {
            fuelComponent.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() => { Destroy(fuelComponent.gameObject); luckyfuelValue +=10; });
        }
    }
}
