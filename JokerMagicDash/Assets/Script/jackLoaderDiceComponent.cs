using UnityEngine;

public class jackLoaderDiceComponent : MonoBehaviour
{
    public static int BestScore
    {
        get
        {
            if (PlayerPrefs.HasKey("JackBestScoreString"))
                return PlayerPrefs.GetInt("JackBestScoreString");
            return 10000;
        }
        set
        {
            PlayerPrefs.SetInt("JackBestScoreString", value);
        }
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(1, 1, 0.4f), 360 * Time.deltaTime);
    }
}
