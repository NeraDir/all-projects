using UnityEngine;

public class Bag : MonoBehaviour
{
    public void Collect()
    {
        FindObjectOfType<GameControl>().CollectBag();
        Destroy(gameObject);
    }
}
