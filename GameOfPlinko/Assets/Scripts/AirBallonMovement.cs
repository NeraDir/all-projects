using System.Collections.Generic;
using UnityEngine;

public class AirBallonMovement : MonoBehaviour
{
    public string AirBallSpeed;
    public string ballName;
    public List<string> ballPieces = new List<string>();

    private string _ballName;

    private void Awake()
    {
        _ballName = ballName;
        if (ballPieces.Count > 0 )
        {
            ballPieces.Clear();
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
