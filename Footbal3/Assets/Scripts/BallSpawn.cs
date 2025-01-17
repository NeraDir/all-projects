using UnityEngine;

public class BallSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _positions;
    [SerializeField] private Transform _ball;
    [SerializeField] private Transform _lookTarget;
    [SerializeField] private Transform[] camPositions;

    public static int _indexOfPositions;

    private void Start()
    {
        Debug.Log(_indexOfPositions);

        _indexOfPositions = 0;
        _indexOfPositions = Random.Range(0, _positions.Length);
        for (int i = 0; i < _positions.Length; i++)
        {
            if (_indexOfPositions == i)
            {
                _ball.position = _positions[i].position;
                transform.position = camPositions[i].position;
                transform.LookAt(_lookTarget.position);
            }
        }
    }
}
