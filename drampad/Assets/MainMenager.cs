using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenager : MonoBehaviour
{
    [SerializeField] private Transform _buttonsConteiner;
    [SerializeField] private GameObject _mainButton;
    [SerializeField] private TextMeshProUGUI _mainText;
    private int[] nums;
    private int x1 = 0;
    private int x2 = 0;
    private void Start()
    {
        _mainText.text = "";
    }
    public void Restart()
    {
        x1 = 0;
        x2 = 0;
        nums = new int[4];

        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = Random.RandomRange(0, _buttonsConteiner.childCount);
        }

        StartCoroutine(Rest());

        _mainText.text = "";

        _mainButton.SetActive(false);
    }

    IEnumerator Rest()
    {
        _buttonsConteiner.GetChild(nums[x1]).GetComponent<ButtonDram>().Show();
        x1++;
        yield return new WaitForSeconds(0.8f);
        if (x1 < nums.Length)
            StartCoroutine(Rest());
        else
            _mainButton.SetActive(true);
    }

    public void Click(int n)
    {
        if (n != nums[x2])
            _mainText.text = "Lose";
        if (x2 < nums.Length-1)
            x2++;
        else
        {
            _mainButton.SetActive(true);
            _mainText.text = "Win";
        }
    }
}
