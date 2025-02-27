using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVEMENTCOMPONENT : MonoBehaviour
{
    private Rigidbody _body;
    private bool canJump = false;
    private bool isHolding = false;
    private float lastTapTime = 0f;
    private const float doubleTapTime = 0.3f;
    public float jumpForce = 10f; 
    public float moveSpeed = -5f;
    public float radius = 1.7f;
    public LayerMask groundMask;
    private bool onGround;
    public float speedMultiplayer;
    private float defaultSpeed;
    public bool isPlayer;
    private bool _launched;
    private MeshRenderer _renderer;
    public string myName;

    public static List<MOVEMENTCOMPONENT> finishedPlayers = new List<MOVEMENTCOMPONENT>();
    [SerializeField] private AudioClip _jump;

    private void Start()
    {
        finishedPlayers.Clear();
        _body = GetComponent<Rigidbody>();
        defaultSpeed = moveSpeed;
        _launched = true;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = FindObjectOfType<PLAYERSSETUPERCOMPONENT>().GetRandomMaterial();
        if (!isPlayer)
            myName = FindObjectOfType<PLAYERSSETUPERCOMPONENT>().GetRandomName();
        else
            myName = "PLAYER";
    }

    private void Update()
    {
        if (!_launched)
            return;
        if (!GAMEMANAGER.runLaunched)
            return;
        if (!isPlayer)
            return;
        if (Input.GetMouseButtonDown(0)) 
        {
            if (Time.time - lastTapTime < doubleTapTime)
            {
                if(onGround)
                    canJump = true; 
            }
            lastTapTime = Time.time;
        }

        isHolding = Input.GetMouseButton(0);
    }

    private void LateUpdate()
    {
        if (!GAMEMANAGER.runLaunched)
            return;
        if (!_launched)
            return;
        if (speedMultiplayer < -0.1f)
        {
            speedMultiplayer = Mathf.Clamp(speedMultiplayer, -3, 0);
            moveSpeed = defaultSpeed + speedMultiplayer;

            speedMultiplayer += 1 * Time.deltaTime;
        }
        else
        {
            moveSpeed = defaultSpeed;
        }

        if (!isHolding) 
        {
            Stay();
        }
        onGround = Physics.CheckSphere(transform.position, radius, groundMask);

        if (canJump)
        {
            Jump();
            canJump = false;
        }
    }

    public void Stay()
    {
        _body.linearVelocity = new Vector3(_body.linearVelocity.x, _body.linearVelocity.y, moveSpeed);
    }

    public void Jump()
    {
        if (onGround)
        {
            _body.linearVelocity = new Vector3(_body.linearVelocity.x, jumpForce, moveSpeed);
            AudioSource.PlayClipAtPoint(_jump,transform.position);
        }
    }

    public IEnumerator BoostSpeed()
    {
        moveSpeed += speedMultiplayer; 
        yield return new WaitForSeconds(5f); 
        moveSpeed = defaultSpeed; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FINISHCOMPONENT finish))
        {
            _launched = false;
            finishedPlayers.Add(this);
            if (isPlayer)
            {
                int place = finishedPlayers.Count;
                var records = PLAYERDATA.RECORDS;
                records.Add(place.ToString());
                PLAYERDATA.RECORDS = records;
                GAMEMANAGER.finished?.Invoke(place, false);
            }
        }
        if (other.TryGetComponent(out TRAPCOMPONENT trap))
        {
            if (!_launched)
                return;
            _launched = false;
            if (isPlayer)
            {
                GAMEMANAGER.finished?.Invoke(0, true);
            }
        }
    }
}
