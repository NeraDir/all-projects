using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField] private GameObject[] textOfStartPage;
    [SerializeField] private GameObject[] imageOfStartPage;
    [SerializeField] private GameObject[] activePageCircle;

    [SerializeField] private GameObject PageRateMe;
    [SerializeField] private GameObject StartPage;
    [SerializeField] private GameObject Mainpage;

    private int indexOfActivepage = 0;

    private void Start()
    {
        for (int i = 0; i < activePageCircle.Length; i++)
        {
            if (indexOfActivepage == i)
            {
                activePageCircle[i].transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                imageOfStartPage[i].SetActive(true);
                textOfStartPage[i].SetActive(true);
            }
            else
            {
                activePageCircle[i].transform.localScale = new Vector3(1f, 1f, 1f);
                imageOfStartPage[i].SetActive(false);
                textOfStartPage[i].SetActive(false);
            }
        }
    }

    public void OnClicknext()
    {
        indexOfActivepage++;
        for (int i = 0; i < activePageCircle.Length; i++)
        {
            if (indexOfActivepage == i)
            {
                activePageCircle[i].transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                imageOfStartPage[i].SetActive(true);
                textOfStartPage[i].SetActive(true);
            }
            else
            {
                activePageCircle[i].transform.localScale = new Vector3(1f, 1f, 1f);
                imageOfStartPage[i].SetActive(false);
                textOfStartPage[i].SetActive(false);
            }
        }

        if (indexOfActivepage >= activePageCircle.Length)
        {
            PageRateMe.SetActive(true);
            StartPage.SetActive(false);
        }
    }

    public void OnClickOpenMainPAge() 
    {
        PageRateMe.SetActive(false);
        Mainpage.SetActive(true);
    }
}
