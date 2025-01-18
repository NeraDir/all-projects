using UnityEngine;

public class FrameRenderHandler : MonoBehaviour
{
    private const int _targetFrameRate = 60;

    private void Awake() => Initialize();

    private void Initialize() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = _targetFrameRate;
    }
}
