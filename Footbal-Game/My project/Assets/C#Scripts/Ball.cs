using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _maxVelosity;
    [SerializeField] private int _startDmg = 0;
    [SerializeField] private int _growUpDmg = 1;
    [SerializeField] private float _maxCounterPower = 5;
    [SerializeField] private float _maxCounter = 100;
    [SerializeField] private TextMeshPro _textDmg;

    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private GameObject _particlesOnDestroyPrefab;

    private int state = 0;// 0  - stop; 1 - fly
    private Rigidbody2D _rb2D;
    
    private int dmg;
    private float startFors = 0;
    private int counter = 0;
    private int powerCounter = 0;
    private bool dead = false; 

    Color startBallColor;
    Color ballColor;
    float timer = 5;
    private void Awake()
    {
        startBallColor = GetComponent<SpriteRenderer>().color;
        ballColor = startBallColor;

        dmg = _startDmg;
        _textDmg.text = dmg.ToString();

        _rb2D = GetComponent<Rigidbody2D>();
        startFors = 0;
    }
    private void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            DestroyThis();
        }
    }
    public void Jump(Vector2 dir,float force)
    {
        _rb2D.AddForce(dir * force);
        startFors = force;
        state = 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 5;
        counter++;
        powerCounter++;

        GateBlock gb = collision.GetComponent<GateBlock>();
        GoldBlock goldb = collision.GetComponent<GoldBlock>();
        if (gb != null || goldb != null)
        {
            if (goldb != null)
            {
                goldb.GetDmg(dmg);
            }
            else
            {
                gb.GetDmg(dmg);
            }
            
            dmg = _startDmg;
            _textDmg.text = dmg.ToString();

            Vector2 v = _rb2D.velocity.normalized * startFors;
            _rb2D.velocity = Vector2.zero;
            _rb2D.AddForce(v);

            if (powerCounter >= _maxCounterPower)
            {
                DestroyThis();
                return;
            }
            powerCounter = 0;
            ballColor = startBallColor;
        }
        

        for (int i = 0; i < 4; i++)
        {
            ballColor[i] += (1 - startBallColor[i]) / _maxCounterPower;
        }
        GetComponent<SpriteRenderer>().color = ballColor;
        _particles.startColor = ballColor;



        if (counter >= _maxCounter)
        {
            DestroyThis();
            return;
        }

        if (gb == null && goldb == null && (_rb2D.velocity * 1.05f).magnitude < _maxVelosity)
        {
            dmg += _growUpDmg;
            _rb2D.velocity *= 1.05f;
            _textDmg.text = dmg.ToString();
        }
    }
    public void DestroyThis()
    {
        if (dead)
            return;
        
        RaundMenager.istance.SpawnBall();
        dead = true;

        GameObject g = Instantiate(
            _particlesOnDestroyPrefab,
            transform.position,
            _particlesOnDestroyPrefab.transform.rotation
            );
        Destroy(gameObject);
    }
}
