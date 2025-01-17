using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineComponent : MonoBehaviour
{
    private holeObjectComponentn _myHole;

    public void Init()
    {
        _myHole = GetComponentInChildren<holeObjectComponentn>();
    }

    public holeObjectComponentn GetMyHole()
    {
        return _myHole;
    }
}
