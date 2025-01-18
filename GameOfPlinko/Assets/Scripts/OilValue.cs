using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OilValue : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Image circleSlider;

    public float oil = 1f;

    private void Start()
    {
        speed = 0.0015f * 5f / PlayerPrefs.GetInt("oil");
    }

    private void FixedUpdate()
    {
        oil -= speed;
        circleSlider.fillAmount = oil;
        circleSlider.color = Color.Lerp(Color.green, Color.red, 1 - oil);
    }

    public void OilRegen ()
    {
        oil = 1;
        circleSlider.fillAmount = oil;
    }


}
