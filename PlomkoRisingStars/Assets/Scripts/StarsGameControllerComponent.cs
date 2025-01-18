using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsGameControllerComponent : MonoBehaviour
{
    public static string starSet;

    [SerializeField]
    private GameObject _ball;

    [SerializeField]
    private GameObject[] _turns;

    [SerializeField]
    private Transform _moveTarget;

    [SerializeField]
    private Transform[] _ballsPosition;

    [SerializeField]
    private Material[] _ballsMaterials;

    [SerializeField]
    private Material _ballOutlineMaterial;

    [SerializeField]
    private Text _recordShow;

    [SerializeField]
    private Text _recordResultShow;

    [SerializeField]
    private Text _ballsCountDisplay;

    [SerializeField]
    private GameObject _roadPiece;

    [SerializeField]
    private Transform[] _heartsImages;

    private int _heartsCount = 4;

    [SerializeField]
    private Transform _roadSpawnPosition;

    public static int record;

    public static int ballsCount;

    public static float moveObjectsSpeed;

    public static UnityEvent<int> addBall = new UnityEvent<int>();

    private IEnumerator Start()
    {
        record = 0;
        ballsCount = 0;
        moveObjectsSpeed = 10;
        StarsBallComponente tempBall = null;
        Instantiate(_roadPiece, _roadSpawnPosition.position, _roadSpawnPosition.rotation);
        while (true)
        {
            for (int i = 0; i < _ballsPosition.Length; i++)
            {
                if (i == 0)
                {
                    tempBall = Instantiate(_ball.GetComponent<StarsBallComponente>(), _ballsPosition[i].position, _ballsPosition[i].rotation);
                    tempBall.moveTarget = _moveTarget;
                    List<Material> ballMat = new List<Material>();
                    ballMat.Add(_ballOutlineMaterial);
                    ballMat.Add(_ballsMaterials[Random.Range(0, _ballsMaterials.Length)]);
		    tempBall.GetComponent<MeshRenderer>().materials = ballMat.ToArray();
                }
                if (Random.Range(0, 2) != 0)
                {
                    StarsTurnComponent tempTurn = Instantiate(_turns[0].GetComponent<StarsTurnComponent>(), _ballsPosition[1].position, _ballsPosition[1].rotation);
                    tempTurn.ball = tempBall;
                    break;
                }
                else
                {
                    StarsTurnComponent tempTurn = Instantiate(_turns[1].GetComponent<StarsTurnComponent>(), _ballsPosition[2].position, _ballsPosition[2].rotation);
                    tempTurn.ball = tempBall;
                    break;
                }
            }
            moveObjectsSpeed += 0.35f;
            yield return new WaitForSeconds(1);
           
        }
    }

    private void LateUpdate()
    {
        _recordShow.text = record.ToString();
        _ballsCountDisplay.text = ballsCount.ToString();
        _recordResultShow.text = record.ToString();
        for (int i = 0; i < _heartsImages.Length; i++)
        {
            if (i < _heartsCount)
            {
                _heartsImages[i].gameObject.SetActive(true);
            }
            else
            {
                _heartsImages[i].DOScale(Vector3.zero, 0.25f);
            }
        }
    }

    public void TurnsPressed(int turnIndex) 
    {
        addBall?.Invoke(turnIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StarsRoadComponent road))
        {
            Instantiate(_roadPiece, _roadSpawnPosition.position, _roadSpawnPosition.rotation);
        }
        else if (other.TryGetComponent(out StarsBallComponente ball))
        {
            _heartsCount--;
            if (_heartsCount <=0)
            {
                SceneManager.LoadScene("GameBonusScene");
            }
        }
        else if (other.TryGetComponent(out IDestroyeble destroyer))
        {
            destroyer.Use();
        }
    }
}
