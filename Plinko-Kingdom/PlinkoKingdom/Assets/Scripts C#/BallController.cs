using UnityEngine;

public class BallController : MonoBehaviour
{
    [HideInInspector] public string tempKey;

    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
