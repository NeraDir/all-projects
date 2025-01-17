using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBoxerTrigger : MonoBehaviour
{
    public float Damage = 0;

    private CharacterComponent myCharacter;

    private void Start()
    {
        myCharacter = GetComponentInParent<CharacterComponent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterComponent character))
        {
            if (character != myCharacter)
            {
                MachineGameController.staticAudioPlayer.PlayOneShot(myCharacter.makeDamage);
                character.TakeDamage(Damage);
                Damage = 0;
            }
        }
    }
}
