using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeTrigger : MonoBehaviour
{
    [SerializeField] private PlaceComponent _placeComponent;

    private List<JarComponent> _jars = new List<JarComponent>();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out JarComponent jar))
        {
            if (_jars.Contains(jar))
            {
                return;    
            }
            _jars.Add(jar);
            _placeComponent.AddItem(jar.gameObject);
        }
    }
}
