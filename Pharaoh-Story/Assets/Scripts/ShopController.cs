using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private AllSellebleItems _allShopItems;

    [SerializeField]
    private ShopConteiner _patternForAllItems;

    [Header("NAV BAR")]
    [SerializeField]
    private PageIdentObject _pagesIdemntiImage;

    [SerializeField]
    private Transform _pagesNavBar;

    [SerializeField]
    private Image currentItem;

    public static int currentOpenedPage;

    public delegate void PageIdentObjectS(int index);
    public static event PageIdentObjectS pageClicked;

    private void Start()
    {
        currentOpenedPage = 0;
        _allShopItems = FindObjectOfType<AllSellebleItems>();

        for (var i = 0; i < _allShopItems.shopebleItems.Count; i++)
        {
            PageIdentObject tem = Instantiate(_pagesIdemntiImage,_pagesNavBar);
            tem.pageIndex = i;
        }
        OnSelectPage(0);
    }

    public void OnSelectPage(int value)
    {
        currentOpenedPage += value;
        if (!CheckStateOfPage())
        {
            if (pageClicked != null)
            {
                pageClicked(currentOpenedPage);
            }
            currentItem.sprite = _allShopItems.shopebleItems[currentOpenedPage].itemIcon;
            _patternForAllItems.currentContainerItem = _allShopItems.shopebleItems[currentOpenedPage];
        }
        else
        {
            return;
        }
    }

    private bool CheckStateOfPage()
    {
        if (currentOpenedPage < 0)
        {
            currentOpenedPage = 0;
            return true;
        }
        else if (currentOpenedPage > _allShopItems.shopebleItems.Count - 1)
        {
            currentOpenedPage = _allShopItems.shopebleItems.Count - 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
