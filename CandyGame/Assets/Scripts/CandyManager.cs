using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CandyManager : MonoBehaviour
{
    [SerializeField] private CandyGenerator candyGenerator;
    [SerializeField] private GameObject prefab;
    private int index;

    [SerializeField] private List<Sprite> sprites;
    //public List<GameObject> Prefabs; 
    public float newObjectScaleFactor = 0.01f;

    public static int caramelSurpriseTryCounts
    {
        get
        {
            if (PlayerPrefs.HasKey("caramelSurpriseTryCountsSaveKey"))
            {
                return PlayerPrefs.GetInt("caramelSurpriseTryCountsSaveKey");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("caramelSurpriseTryCountsSaveKey", value);
        }
    }

    public static string caramelSurpriseNameKey;

    public static int caramelSurpriseWinsCount
    {
        get
        {
            if (PlayerPrefs.HasKey("caramelSurpriseWinsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("caramelSurpriseWinsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("caramelSurpriseWinsCountSaveKey", value);
        }
    }

    private void Start()
   {
        Candy.CandysHit += MergeCandy;
   }
    // public void Merge(GameObject object1, GameObject object2)
    //{
    //    index = object1.GetComponent<Candy>().Id + 1;
    //    // Определяем позицию для спавна нового объекта (например, в середине между двумя объектами)
    //    Vector3 newPosition = (object1.transform.position + object2.transform.position) / 2;

    //    // Спавним новый объект
    //    GameObject newObject = Instantiate(prefab, newPosition, Quaternion.identity);

    //    // Увеличиваем размер нового объекта
    //    newObject.transform.localScale += Vector3.one * newObjectScaleFactor * index;
    //    newObject.GetComponent<SpriteRenderer>().sprite = sprites[index];
    //    newObject.GetComponent<Candy>().Id = index;

    //    // Удаляем старые объекты
    //    Destroy(object1);
    //    Destroy(object2);
    //}
    public void MergeCandy(GameObject gameObject1, GameObject gameObject2, int id)
    {
        candyGenerator.Merge(gameObject1, gameObject2, id);
    }

    private void Update()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        if ((go != null && !go.CompareTag("UI")) || go == null)
        {
            if (Input.GetMouseButton(0))
            {
                if (candyGenerator.candy != null && !candyGenerator.candy.CompareTag("Bomb"))
                {
                    Vector2 pos = Input.mousePosition;
                    pos = Camera.main.ScreenToWorldPoint(pos);

                    candyGenerator.candy.transform.position = new Vector2(pos.x, candyGenerator.candy.transform.position.y);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (candyGenerator.candy != null && !candyGenerator.candy.CompareTag("Bomb"))
                {
                    candyGenerator.candy.Rb.constraints = RigidbodyConstraints2D.None;
                    candyGenerator.candy.falled = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        Candy.CandysHit -= MergeCandy;
    }

    private IEnumerator DragCandy()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (Time.timeScale > 0)
        {

        }
    }

    private IEnumerator FallCandy()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (Time.timeScale > 0)
        {

        }
    }
}
