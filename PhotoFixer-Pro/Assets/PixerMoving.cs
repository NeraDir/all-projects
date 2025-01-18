using UnityEngine;

public class PixerMoving : MonoBehaviour
{
    public string PixersavbingKEy;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
