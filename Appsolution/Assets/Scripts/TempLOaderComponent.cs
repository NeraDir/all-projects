using UnityEngine;

public class TempLOaderComponent : MonoBehaviour
{
    [HideInInspector]public string tempKey;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
