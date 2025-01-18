using UnityEngine;

public class PageIdentObject : MonoBehaviour
{
    public int pageIndex;

    private Vector3 beginScale;

    public void Awake()
    {
        ShopController.pageClicked += ShopController_pageClicked;
        beginScale = transform.localScale;
    }

    private void ShopController_pageClicked(int index)
    {
        if (index == pageIndex)
            transform.localScale = beginScale * 1.5f;
        else
            transform.localScale = beginScale;
    }

    private void OnDestroy()
    {
        ShopController.pageClicked -= ShopController_pageClicked;
    }
}
