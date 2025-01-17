using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class playercontroller : MonoBehaviour
{
    private bool move;

    public static UnityEvent RoadSpawn = new UnityEvent();

    public static UnityEvent PlayerIsDeath = new UnityEvent();

    [SerializeField]
    private GameObject _transitionEffect;

    [SerializeField]
    private GameObject[] _models;

    private void Start()
    {
        swipecontroller.SwipeRight.AddListener(MoveRight);
        swipecontroller.swipesLeft.AddListener(MoveLeft);
        changeplayermodelcomponent.playerModel.AddListener(ChangeModel);
    }

    private void LateUpdate()
    {
        if (!gamemanager.gameRunned)
            return;
        transform.position += new Vector3(0, 0, 1) * 15 * Time.deltaTime;
    }

    private void OnDestroy()
    {
        swipecontroller.SwipeRight.RemoveListener(MoveRight);
        swipecontroller.swipesLeft.RemoveListener(MoveLeft);
        changeplayermodelcomponent.playerModel.RemoveListener(ChangeModel);
    }

    private void ChangeModel(string key,string road) 
    {
        Instantiate(_transitionEffect, transform.position, Quaternion.identity);
        foreach (var model in _models)
        {
            model.gameObject.SetActive(false);
        }
        switch (key)
        {
            case "car":
                if (road == "air")
                {
                    PlayerIsDeath?.Invoke();
                    return;
                }
                _models[0].SetActive(true);
                transform.DOMoveY(0.86f, 0.25f);
                break;
            case "plane":
                if (road == "road")
                {
                    PlayerIsDeath?.Invoke();
                    return;
                }
                _models[1].SetActive(true);
                transform.DOMoveY(12.2f, 0.25f);
                break;
            case "boat":
                if (road == "road")
                {
                    PlayerIsDeath?.Invoke();
                    return;
                }
                _models[2].SetActive(true);
                transform.DOMoveY(-19.59f, 0.25f);
                break;
        }
    }

    private void MoveLeft() 
    {
        if (move)
            return;
        if (transform.position.x - 2.75f < -2.75f)
            return;
        move = true;
        transform.DOMoveX(transform.position.x - 2.75f,0.25f).OnComplete(() => move = false);
    }

    private void MoveRight() 
    {
        if (move)
            return;
        if (transform.position.x + 2.75f > 2.75f)
            return;
        move = true;
        transform.DOMoveX(transform.position.x + 2.75f, 0.25f).OnComplete(() => move = false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out roadcomponent road))
        {
            RoadSpawn?.Invoke();
            Destroy(road.gameObject, 3);
        }
        if (other.TryGetComponent(out changeplayermodelcomponent changer))
        {
            changer.Use();
        }
        if (other.TryGetComponent(out starscomponent star))
        {
            star.Use();
        }
        if (other.TryGetComponent(out trapcomponent trap))
        {
            PlayerIsDeath?.Invoke();
        }
    }
}
