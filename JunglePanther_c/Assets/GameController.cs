using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private List<SegmentPlatform> allPlatforms;

    private SegmentPlatform lastPlatform;

    [SerializeField]
    private int startPlatformsCount;

    private float currentZpos;

    private void OnEnable()
    {
        Tree.DetectTree += ShowGameOverPanel;
        PlatfromPlayerTrigger.PlayerDetected += SpawnPlatform;
    }
    private void OnDisable()
    {
        Tree.DetectTree -= ShowGameOverPanel;
        PlatfromPlayerTrigger.PlayerDetected -= SpawnPlatform;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    private void Start()
    {
        currentZpos = 0;
        SpawnFirstPlatfroms();
    }

    public void SpawnFirstPlatfroms()
    {
        int emptyPlatformCount = 0;

        if (startPlatformsCount > 2)
        {
            emptyPlatformCount = 2;
        }
        else
        {
            emptyPlatformCount = 1;
        }


        for (int i = 0; i < emptyPlatformCount; i++)
        {
            SpawnPlatform(false);
        }

        for (int i = 0; i < startPlatformsCount - emptyPlatformCount; i++)
        {
            SpawnPlatform(true);
        }
    }

    private void SpawnPlatform(bool isRandomPlatform)
    {
        int modelIndex = 0;

        if (isRandomPlatform)
        {
            modelIndex = Random.Range(0, allPlatforms.Count);
        }

        SegmentPlatform buff = allPlatforms[modelIndex];
        Vector3 futurePos = new Vector3(0, 0, currentZpos);
        lastPlatform = Instantiate(buff, futurePos, buff.transform.rotation);
        currentZpos += (lastPlatform.finalPoint.position.z - lastPlatform.startPoint.position.z);
    }


}

