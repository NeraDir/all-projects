using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private SpriteRenderer _superForceSpriteRenderer;
    [SerializeField] private SpriteRenderer _springinessSpriteRenderer;
    [SerializeField] private SpriteRenderer _stickinessSpriteRenderer;
    [SerializeField] private string _springinessPhysicsMaterial;
    [SerializeField] private string _normalPhysicsMaterial;

    private Rigidbody2D _rb2D;
    public bool waitForJump = true;

    public bool _superForceMode = false;
    public bool _springinessMode = false;
    public bool _stickinessMode = false;

    [SerializeField] private int _numSuperJumps = 0;
    [SerializeField] private int _numSpringinessJumps = 0;
    [SerializeField] private int _numStickinessJumps = 0;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _superForceSpriteRenderer.enabled = false;
        _springinessSpriteRenderer.enabled = false;
        _stickinessSpriteRenderer.enabled = false;
        GetComponent<PolygonCollider2D>().sharedMaterial = Resources.Load(_normalPhysicsMaterial) as PhysicsMaterial2D;
    }
    public void Jump(Vector2 dir)
    {
        _rb2D.constraints = RigidbodyConstraints2D.None;
        if (_superForceMode)
        {
            _rb2D.AddForce(dir * _force * 2);
        }
        else
        {
            _rb2D.AddForce(dir * _force);
        }
        Main.instance.Jump();
    }
    private void FixedUpdate()
    {
        if (_rb2D.velocity.magnitude <= 0.005f && _rb2D.angularVelocity < 0.005f)
        {
            _rb2D.velocity = Vector2.zero;
            waitForJump = true;
        }
        else
        {
            waitForJump = false;
        }
    }
    public void SwitchSuperForceMode()
    {
        if (!waitForJump)
            return;

        _superForceMode = !_superForceMode;
        
        if (_superForceMode)
        {
            _superForceSpriteRenderer.enabled = true;
        }
        else
        {
            _superForceSpriteRenderer.enabled = false;
        }
    }
    public void SwitchSpringinessMode()
    {
        if (!waitForJump)
            return;

        _springinessMode = !_springinessMode;
        PhysicsMaterial2D material;
        if (_springinessMode)
        {
            _springinessSpriteRenderer.enabled = true;
            material = Resources.Load(_springinessPhysicsMaterial) as PhysicsMaterial2D;
        }
        else
        {
            _springinessSpriteRenderer.enabled = false;
            material = Resources.Load(_normalPhysicsMaterial) as PhysicsMaterial2D;
        }
        GetComponent<PolygonCollider2D>().sharedMaterial = material;
    }
    public void SwitchStickinessMode()
    {
        if (!waitForJump)
            return;

        _stickinessMode = !_stickinessMode;
        if (_stickinessMode)
        {
            _stickinessSpriteRenderer.enabled = true;
        }
        else
        {
            _stickinessSpriteRenderer.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_springinessMode)
        {
            _springinessSpriteRenderer.enabled = false;
            _springinessMode = false;
            StartCoroutine(SwitchSpringinessModeTimer());
            return;
        }
        if (_stickinessMode)
        {
            _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    IEnumerator SwitchSpringinessModeTimer()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<PolygonCollider2D>().sharedMaterial = Resources.Load(_normalPhysicsMaterial) as PhysicsMaterial2D;
    }
}