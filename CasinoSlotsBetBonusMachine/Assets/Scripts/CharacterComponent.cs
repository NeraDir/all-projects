using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterComponent : MonoBehaviour
{
    [SerializeField]
    private Image characterHealthBar;

    public AudioClip makeDamage;

    [SerializeField]
    private AudioClip takeDamage;

    private MachineBoxerAniamtionController animator;

    public bool isPlayer;

    private float healthCount;

    private float maxHealthCount = 100;

    public static UnityEvent<bool> isCharacterDeath = new UnityEvent<bool>();

    private void Start()
    {
        healthCount = maxHealthCount;
        animator = GetComponent<MachineBoxerAniamtionController>();
    }

    private void LateUpdate()
    {
        UpdateVisualhealthBar();
    }

    private void UpdateVisualhealthBar() 
    {
        characterHealthBar.fillAmount = Mathf.Lerp(characterHealthBar.fillAmount, healthCount / maxHealthCount, 8 * Time.deltaTime);
    }

    public void TakeDamage(float damage) 
    {
        MachineGameController.staticAudioPlayer.PlayOneShot(takeDamage);
        healthCount -= damage;
        animator.SetAniamtion(5);
        if (healthCount <= 0)
        {
            healthCount = 0;
            isCharacterDeath?.Invoke(isPlayer);
        }
    }
}
