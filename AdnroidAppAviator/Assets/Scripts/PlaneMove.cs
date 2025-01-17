using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaneMove : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private OilValue oil;

    private int score = 0;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject wheel, star, island;

    [SerializeField]
    private GameObject deadPan;
    [SerializeField]
    private Text deadPanText;

    private void Start()
    {
        Time.timeScale = 1;
        SpawnWheel();
        SpawnIsland();
    }

    private void SpawnWheel ()
    {
        Instantiate(wheel);
        Invoke("SpawnStar", 2f * 5f / PlayerPrefs.GetInt("speed"));
    }
    private void SpawnStar ()
    {
        Instantiate(star);
        Invoke("SpawnWheel", 2f * 5f / PlayerPrefs.GetInt("speed"));
    }
    private void SpawnIsland ()
    {
        Instantiate(island);
        Invoke("SpawnIsland", 7.5f * 5f / PlayerPrefs.GetInt("speed"));
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 target = hit.point;
            if (target.x < -0.6f) target.x = -0.6f;
            if (target.x > 0.6f) target.x = 0.6f;
            if (target.y > -3.5f) target.y = -3.5f;
            if (target.y < -6f) target.y = -6f;
            target.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, target, _speed * Time.deltaTime);//new Vector3(target.x,target.y, transform.position.z);
            transform.rotation = Quaternion.Euler(transform.position.x * -20f, -90, 0);
        }
        if (oil.oil <= 0) GameOver();
    }

    private void GameOver ()
    {
        oil.oil = 1;
        Time.timeScale = 0;
        deadPan.SetActive(true);
        deadPanText.text = "You find : \n" + score + " stars";
        PlayerPrefs.SetInt("stars", PlayerPrefs.GetInt("stars") + score);
    }
    public void Replay ()
    {
        SceneManager.LoadScene(2);
    }
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wheel")
        {
            GameOver();
        }
        if (other.tag == "Oil")
        {
            oil.OilRegen();
            Destroy(other.gameObject);
        }        
        if (other.tag == "Star")
        {
            scoreText.text = ++score + "";
            Destroy(other.gameObject);
        }
    }
}
