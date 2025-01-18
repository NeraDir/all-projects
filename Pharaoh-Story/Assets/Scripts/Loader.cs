using UnityEngine;

public class Loader : MonoBehaviour
{
    public static string LoadingTxt;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
