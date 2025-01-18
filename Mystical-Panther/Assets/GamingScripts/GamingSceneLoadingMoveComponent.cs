using UnityEngine;

public class GamingSceneLoadingMoveComponent : MonoBehaviour
{
    public string m_GamingSceneLoadingMoveString;

    public void Awake()
    {
        INIT(m_GamingSceneLoadingMoveString);
    }

    private void INIT(string inputString) 
    {
        m_GamingSceneLoadingMoveString = inputString;
        DontDestroyOnLoad(this.gameObject);
    }
}
