using UnityEngine;

public class RocketComponente : MonoBehaviour
{
    public string Rocketname;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
