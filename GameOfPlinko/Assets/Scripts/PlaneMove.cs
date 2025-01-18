using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlaneMove : MonoBehaviour
{
    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private Transform spawnPosition;

    private Rigidbody _planeBody;

    [SerializeField]
    private GameObject _gamingPanel;

    [SerializeField]
    private GameObject _flyingPanel;

    private float strenghtOfJump;

    [SerializeField]
    private Slider sliderStrenght;

    [SerializeField]
    private GameObject tappingTXT;

    [SerializeField]
    private GameObject pressingTxt;

    [SerializeField]
    private TMP_Text[] _livingTimerShower;

    [SerializeField]
    private TMP_Text[] _passedRingsShower;

    [SerializeField]
    private TMP_Text[] _coinsEarnShower;

    [SerializeField]
    private TMP_Text _strenghtShower;

    private bool rotatier;

    private Animator animator;

    private bool isDowned;

    private float rotatingvalue;

    [SerializeField]
    private GameObject[] AirPlanes;

    [SerializeField]
    private GameObject _loosingPanel;

    private float _fuelValue;

    private float _fuelStartValue;

    [SerializeField]
    private Image Image_fuelImage;

    private bool _canFly;


    private float _fuelTimer;

    private float _coins;

    public static int passedParts;

    private float _livingTimer;

    private void Start()
    {
        GameManager.GameBeginned = false;
        passedParts = 0;
        _coins = 0;
        Time.timeScale = 1;
        _canFly = true;
        _fuelValue = 10 * (GamePlayerInformation.PlaneSelected + 1);
        _fuelStartValue = _fuelValue;
        AirPlanes[GamePlayerInformation.PlaneSelected].SetActive(true);
        rotatingvalue = 45;
        rotatier = false;
        sliderStrenght.maxValue = 10;
        sliderStrenght.value = 0;
        animator = GetComponent<Animator>();
        strenghtOfJump = 0;
        _planeBody = GetComponent<Rigidbody>();
        StartCoroutine(spawningEffect());
    }

    public void BeginFly() 
    {
        _planeBody.AddForce(transform.forward * (strenghtOfJump * 10), ForceMode.Impulse);
        _gamingPanel.SetActive(false);
        _flyingPanel.SetActive(true);
        GameManager.GameBeginned = true;
        animator.enabled = false;
    }

    private IEnumerator spawningEffect() 
    {
        while (true)
        {
            GameObject spawOb = Instantiate(effect, spawnPosition.position,spawnPosition.rotation);
            spawOb.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void LateUpdate()
    {
        if (GameManager.GameBeginned)
        {
            if (isDowned && _canFly)
            {
                rotatingvalue += 0.1f;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x - rotatingvalue, transform.rotation.y, transform.rotation.z), 4 * Time.deltaTime);
            }
            else
            {
                rotatingvalue = 45;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.x + 45, transform.rotation.y, transform.rotation.z), 4 * Time.deltaTime);
            }

            if (_fuelValue <= 0)
            {
                _canFly = false;
                _fuelValue = 0;
            }
            else
            {
                _canFly= true;
            }

            _livingTimer += Time.deltaTime;

            _fuelValue -= Time.deltaTime;

            Image_fuelImage.fillAmount = Mathf.MoveTowards(Image_fuelImage.fillAmount, _fuelValue / _fuelStartValue, 11 * Time.deltaTime);

            _planeBody.velocity = transform.forward * GamePlayerInformation.PlanesSpeed;
        }
        else
        {
            strenghtOfJump -= 7 * Time.deltaTime;
            if (strenghtOfJump <= 0)
            {
                strenghtOfJump = 0;
            }
            sliderStrenght.value = Mathf.Lerp(sliderStrenght.value, strenghtOfJump / 10, 8 * Time.deltaTime);

            _strenghtShower.text = strenghtOfJump.ToString("0");
        }

        foreach (var item in _coinsEarnShower)
        {
            item.text = "COINS: " + _coins.ToString("0");
        }

        foreach (var item in _livingTimerShower)
        {
            item.text = "LIFE TIME: " + _livingTimer.ToString("0") + "s";
        }

        foreach (var item in _passedRingsShower)
        {
            item.text = "RINGS: " + passedParts.ToString("0");
        }
    }

    public void OnDown() 
    {
        if (!_canFly)
            return;
        isDowned = true;
        pressingTxt.SetActive(false);
    }

    public void Down()
    {
        if (!_canFly)
            return;
        isDowned = false;
    }

    public void ClickToSetSpeed() 
    {
        tappingTXT.SetActive(false);
        strenghtOfJump += 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PartNextSpawn parter))
        {
            parter.OnTriggerUSE();
            _coins += 10;
        }
        else if (other.TryGetComponent(out GoodRinger ring))
        {
            passedParts++;
            ring.OnTriggeredSizer();
            _fuelValue = _fuelStartValue;
        }
        else if (other.TryGetComponent(out DeadObject objectDead)) 
        {
            objectDead.OnDeath();
            _loosingPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (GamePlayerInformation.RecordOfLivingTime < _livingTimer)
        {
            GamePlayerInformation.RecordOfLivingTime = _livingTimer;
        }

        if (GamePlayerInformation.RecordOfPassedRings < passedParts)
        {
            GamePlayerInformation.RecordOfPassedRings = passedParts;
        }

        GamePlayerInformation.GameCoins += _coins;
    }

    public void OnClickLoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

        if (GamePlayerInformation.RecordOfLivingTime < _livingTimer)
        {
            GamePlayerInformation.RecordOfLivingTime = _livingTimer;
        }

        if (GamePlayerInformation.RecordOfPassedRings < passedParts)
        {
            GamePlayerInformation.RecordOfPassedRings = passedParts;
        }

        GamePlayerInformation.GameCoins += _coins;
    }
}
