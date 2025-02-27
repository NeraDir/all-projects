using UnityEngine;

public class TigerComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out LooseLineComponent loose))
            TigerClawsGameController.onGameEnd?.Invoke(false);
    }
}
