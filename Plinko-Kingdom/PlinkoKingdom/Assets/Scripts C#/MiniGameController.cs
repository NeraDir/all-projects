using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MiniGameController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] m_ShowCurrentX;

    [SerializeField]
    private TMP_Text[] m_Harder;

    [SerializeField]
    private TMP_Text[] m_Middle;

    [SerializeField]
    private TMP_Text[] m_PreMiddle;

    [SerializeField]
    private TMP_Text[] m_PrePreMiddle;

    [SerializeField]
    private TMP_Text[] m_Zeroing;

    [SerializeField]
    private PlinkosComponent[] m_HarderPLink;

    [SerializeField]
    private PlinkosComponent[] m_MiddlePLink;

    [SerializeField]
    private PlinkosComponent[] m_PrePLink;

    [SerializeField]
    private PlinkosComponent[] m_PrePrePLink;

    [SerializeField]
    private PlinkosComponent[] m_ZeroPLink;

    public delegate void OnTriggerGetValue(int value);
    public static OnTriggerGetValue Ontrigger;

    private int xValueResult;

    [SerializeField]
    private GameObject m_Ball;

    public float WidthValue;
    public float HeightValue;

    [SerializeField]
    private GameObject M_ResultPanel;

    [SerializeField]
    private TMP_Text m_ResultPointsValue;

    private float m_value;

    [SerializeField]
    private GameObject menuButon;

    public static int GameWinningValue
    {
        get
        {
            if (PlayerPrefs.HasKey("WinningValueSaveKey"))
            {
                return PlayerPrefs.GetInt("WinningValueSaveKey");
            }
            return 10;
        }
        set
        {
            PlayerPrefs.SetInt("WinningValueSaveKey", value);
        }
    }

    private float tempValue;

    private void OnResulting()
    {
        M_ResultPanel.SetActive(true);
    }

    public static void UseEvent(int value)
    {
        if (Ontrigger != null)
        {
            Ontrigger(value);
        }
    }

    private void Start()
    {
        m_value = GameWinningValue;
        Ontrigger += OnSetValues;
        tempValue = GameWinningValue;
        SetValues(Random.Range(5f, 20f), m_Harder, m_HarderPLink);
        SetValues(Random.Range(1f, 5f), m_Middle, m_MiddlePLink);
        SetValues(Random.Range(0.5f, 1.5f), m_PreMiddle, m_PrePLink);
        SetValues(Random.Range(0.05f, 0.3f), m_PrePreMiddle, m_PrePrePLink);
        SetValues(Random.Range(0f, 0.3f), m_Zeroing, m_ZeroPLink);


        for (int i = 0; i < Random.Range(5, 20); i++)
        {
            GameObject ballSpawned = Instantiate(m_Ball, m_Ball.transform.position, m_Ball.transform.rotation);
            ballSpawned.transform.localPosition = new Vector3(Random.Range(m_Ball.transform.position.x, m_Ball.transform.position.x + WidthValue), Random.Range(m_Ball.transform.position.y, m_Ball.transform.position.y + HeightValue), m_Ball.transform.position.z);
            ballSpawned.SetActive(true);
        }

        Invoke(nameof(OnResulting), 8);
    }


    private void OnDestroy()
    {
        GameWinningValue = 0;

        Ontrigger -= OnSetValues;
    }

    public void OnSetValues(int value)
    {
        xValueResult += value;
        menuButon.SetActive(true);
    }

    private void LateUpdate()
    {
        foreach (var item in m_ShowCurrentX)
        {
            item.text = "x" + xValueResult.ToString("0");
        }
        tempValue = Mathf.Lerp(tempValue, m_value, 20 * Time.deltaTime);
        m_ResultPointsValue.text = (tempValue).ToString("0");
    }

    public void SetNewValue() 
    {
        m_value *= xValueResult;
    }

    public void OnClickGoMenu() 
    {
        PlayerDatas.Points += (int)m_value;
        SceneManager.LoadScene("MenuScene");
    }

    private void SetValues(float value, TMP_Text[] showeValues, PlinkosComponent[] plinker) 
    {
        foreach (var item in showeValues)
        {
            item.text = "x" + value.ToString("0.0");
        }

        foreach (var item in plinker)
        {
            item.xValue = value;
        }
    }
}
