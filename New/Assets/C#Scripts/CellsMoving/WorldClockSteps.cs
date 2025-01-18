using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClockSteps : MonoBehaviour
{
    [SerializeField] private float _maxTimeForStep;

    private bool isStep = false;
    private bool oneMoreStep = true;
    public float maxTimeForStep => _maxTimeForStep;
    public delegate void StepEvent();
    public static event StepEvent State0;
    public static event StepEvent State1;
    public static event StepEvent State2;
    public static event StepEvent State3;

    public static WorldClockSteps instance;


    public static int enemiesDamageValue
    {
        get
        {
            if (PlayerPrefs.HasKey("enemiesDamageValueSavingValue"))
            {
                return PlayerPrefs.GetInt("enemiesDamageValueSavingValue");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("enemiesDamageValueSavingValue", value);
        }
    }

    public static int enemiesHealthValueWorld
    {
        get
        {
            if (PlayerPrefs.HasKey("enemiesHealthValueWorldSavingKey"))
            {
                return PlayerPrefs.GetInt("enemiesHealthValueWorldSavingKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("enemiesHealthValueWorldSavingKey", value);
        }
    }

    private void Awake()
    {
        if (_maxTimeForStep <= 0)
            Debug.LogError("_maxTimeForStep <= 0");
        if (instance != null)
            Debug.LogError("more than one such class on the Scene");
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartStep());
        }
    }
    public void TryStep()
    {
        if (!isStep)
        {
            StartCoroutine(StartStep());
            oneMoreStep = false;
        }
    }
    public void OneMoreStep()
    {
        oneMoreStep = true;
    }
    private IEnumerator StartStep()
    {
        isStep = true;

        State0?.Invoke();
        State1?.Invoke();
        State2?.Invoke();

        yield return new WaitForSeconds(_maxTimeForStep);

        State3?.Invoke();

        isStep = false;
        if (oneMoreStep)
        {
            oneMoreStep = false;
            StartCoroutine(StartStep());
        }
    }
}

