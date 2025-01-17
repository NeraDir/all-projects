using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroyerRollerComponent : MonoBehaviour
{
    private Rigidbody rollerBody;

    [SerializeField]
    private float jumpStrenght;

    private bool rollerIsOnRoad;

    [SerializeField]
    private Material[] rollerColors;

    private MeshRenderer rollerMesh;

    private bool canChangeColor;

    private int colorIndex;

    private float moveSpeed = 8f;

    private float time;

    private void Start()
    {
        rollerBody = GetComponent<Rigidbody>();
        rollerMesh = GetComponent<MeshRenderer>();
    }

    public void OnClickChangeColor(int index) 
    {
        if (canChangeColor)
            return;
        canChangeColor = true;
        colorIndex = index;
        transform.DOScale(transform.localScale / 1.4f, 0.1f).OnComplete(() => { rollerMesh.material = rollerColors[colorIndex]; transform.DOScale(transform.localScale * 1.4f, 0.1f).OnComplete(() => OnColorChangeComplete()); });
    }

    private void OnColorChangeComplete() 
    {
        canChangeColor = false;
    }

    private void LateUpdate()
    {
        time += Time.deltaTime;
        if (time > 8)
        {
            moveSpeed+=1;
            time = 0;
        }
        rollerBody.velocity = new Vector3(1 * moveSpeed, rollerBody.velocity.y, rollerBody.velocity.z);
    }

    public void Jump() 
    {
        if (!rollerIsOnRoad)
            return;
        rollerBody.AddForce(Vector3.up * jumpStrenght, ForceMode.Impulse);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out DestroyerRoadMovementComponent road)) 
        {
            rollerIsOnRoad = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out WallsStars star))
        {
            star.GetMe();
        }
        else if (other.TryGetComponent(out DestroyWallTrapComponent wallTrap))
        {
            wallTrap.DestroyMe(colorIndex, rollerBody);
        }
        else if (other.TryGetComponent(out DestroySpikeComponent spik))
        {
            spik.OnTrigger(rollerBody);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        rollerIsOnRoad = false;
    }
}
