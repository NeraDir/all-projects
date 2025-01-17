using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator Instance;
    public PlayerController PLController;
    public CameraFollow flCam;

    public Transform StartSpawnPlayer;
    [SerializeField] private TMP_Text StarsddAmount;
    [SerializeField] private TMP_Text MetresddAmount;
    [SerializeField] private Joystick joystick;

    public List<Movement> PlayersSkins = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Movement buff = Instantiate(PlayersSkins[GlobalSave.ChoosenRocket], StartSpawnPlayer.position, PlayersSkins[GlobalSave.ChoosenRocket].transform.rotation);
        buff.StarsShowAmount = StarsddAmount;
        buff.MetresShowAmount = MetresddAmount;
        buff.joystick = joystick;
        PLController.PlayerMovement = buff;
        flCam.Target = buff.transform;
    }

    public List<Transform> Segments = new();
    public LosePanelInit LosePanel;

    public void EndGameAFF()
    {
        LosePanel.Init(Movement.Instance.CurrerentStars, Movement.Instance.CurrentMetres);
        LosePanel.gameObject.SetActive(true);
    }

    public void SpawnSegment()
    {
        int randSegmentID = Random.Range(0, Segments.Count);

        Instantiate(Segments[randSegmentID], new Vector3(-380.899994f, -6.22155523f, -2.60003662f), Segments[randSegmentID].rotation);
    }
}
