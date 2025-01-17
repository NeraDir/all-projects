using UnityEngine;

public class LoadingObject : MonoBehaviour
{
    public string loadingString;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
