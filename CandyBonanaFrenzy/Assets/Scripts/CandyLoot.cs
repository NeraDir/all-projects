using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLoot : MonoBehaviour 
{
    private int _score;

    public int Score => _score;

    public int Index;

    [SerializeField]
    private Material[] _materials;

    [SerializeField]
    private MeshRenderer[] _rendererTypes;

    private MeshRenderer _rendererCurrent;

    public bool isCoal;

    [SerializeField]
    private Material _coalMaterial;

    [SerializeField]
    private MeshRenderer _coalRenderer;

    public void Init()
    {
        if (_rendererCurrent != null)
        {
            Destroy(_rendererCurrent.gameObject);
        }
        if (Random.Range(0, 2) != 0)
        {
            isCoal = Random.Range(0, 2) != 0 ? true : false;
            Index = Random.Range(0, _rendererTypes.Length);
            _rendererCurrent = Instantiate(isCoal == true ? _coalRenderer : _rendererTypes[Index],transform);
            _rendererCurrent.material = isCoal == true ? _coalMaterial : _materials[Random.Range(0, _materials.Length)];
            _rendererCurrent.transform.localPosition = new Vector3(0, 1.1f, 0);
            _score = (isCoal == true ? Random.Range(-10, -20) : Random.Range(5, 10)) * (Index + 1);
        }
        else
        {
            _score = 0;
        }
    }

    private void LateUpdate()
    {
        transform.Rotate(new Vector3(0, 1, 0.1f), 90 * Time.deltaTime);
    }

    public void Destroye() 
    {
        if (_rendererCurrent != null)
            StartCoroutine(Destroing());
    }

    private IEnumerator Destroing() 
    {
        while (_rendererCurrent.transform.localScale != Vector3.zero)
        {
            _rendererCurrent.transform.localScale = Vector3.MoveTowards(_rendererCurrent.transform.localScale, Vector3.zero, 200 * Time.deltaTime);
            yield return null;
        }
        Destroy(_rendererCurrent.gameObject);
    }
}
