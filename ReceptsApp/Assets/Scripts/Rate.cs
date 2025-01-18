using UnityEngine;

public class Rate : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Stars;

    public void OnClickSelectRate(int StarID) 
    {
        for (int i = 0; i < m_Stars.Length; i++)
        {
            if (i <= StarID)
            {
                m_Stars[i].SetActive(true);
            }
            else
            {
                m_Stars[i].SetActive(false);
            }
        }
    }

    public void OnClickRate() 
    {
        #if UNITY_ANDROID
                Application.OpenURL("market://details?id=YOUR_ID");
        #elif UNITY_IPHONE
                Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
        #endif
    }

}
