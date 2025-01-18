using UnityEngine.Audio;
using UnityEngine;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixer _targetGameMixer;

    private const string _volumeParameterName = "MasterVolume";

    public void ChangeVolumeLevel(float volume) => _targetGameMixer.SetFloat(_volumeParameterName, volume);
}
