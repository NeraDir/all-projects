using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonLanController : MonoBehaviour
{
    public static int DragonLanSoulsCount 
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanSoulsCountSaveKey"))
            {
                return PlayerPrefs.GetInt("DragonLanSoulsCountSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanSoulsCountSaveKey",value);
        }
    }

    public static int DragonLanGameCanvasScaleValue
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanGameCanvasScaleValue"))
            {
                return PlayerPrefs.GetInt("DragonLanGameCanvasScaleValue");
            }
            return 70;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanGameCanvasScaleValue", value);
        }
    }

    public static string DragonLanGameSettingKey;

    public static int DragonLanSkinIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanSkinIndexSaveKey"))
            {
                return PlayerPrefs.GetInt("DragonLanSkinIndexSaveKey");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanSkinIndexSaveKey", value);
        }
    }

    public static int DragonLanPrefIndex
    {
        get
        {
            if (PlayerPrefs.HasKey("DragonLanPrefIndex"))
            {
                return PlayerPrefs.GetInt("DragonLanPrefIndex");
            }
            return 0;
        }
        set
        {
            PlayerPrefs.SetInt("DragonLanPrefIndex", value);
        }
    }

    public static UnityEvent<Transform> DragonShoot = new UnityEvent<Transform>();

    [SerializeField]
    private DragonLanBullet dragonBullet;

    [SerializeField]
    private Transform bulletPosition;

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    private Material[] dragonSkins;

    private void Start()
    {
        skinnedMeshRenderer.material = dragonSkins[DragonLanSkinIndex];
        DragonShoot.AddListener(Shoot);
    }

    private void Shoot(Transform target) 
    {
        DragonLanBullet tempBullet = Instantiate(dragonBullet, bulletPosition.position, Quaternion.identity);
        tempBullet.transform.DOMove(new Vector3(target.position.x, target.position.y + 0.5f, target.position.z), 0.75f);
        tempBullet.isEnemieBullet = false;
    }

    private void OnDestroy()
    {
        DragonShoot.RemoveAllListeners();
    }

    private void LateUpdate()
    {
        if (!DragonLanGameController.dragonAlive)
            return;
        transform.position += new Vector3(0, 0, 1) * (1.5f + ((float)DragonLanGameController.currentLevel / 10) + 0.1f) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DragonLanCoinsBag bag))
        {
            bag.transform.DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                DragonLanGameController.coinsPerLevel += Random.Range(1, 5);
                Destroy(bag.gameObject);
            });
        }
        if (other.TryGetComponent(out DragonLanGetBullet getBullet))
        {
            DragonLanGameController.fireballs+= 1;
            Destroy(getBullet.gameObject);
        }
    }
}
