using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _airplane;
    [SerializeField] private CircleCollider2D _magnet;
    [SerializeField] private Transform _left;
    [SerializeField] private Transform _right;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private GameObject[] _bullet;
    [SerializeField] private GameObject[] _sounds;
    private int _bulletLevel= 0;

    private bool _death = false;
    private bool _inviscible = false;
    private bool _shoot = false;
    private float _shootTime = 7f;


    private float _reloadTime = 1f;  
    private float _speed = 1f;



    private float _maxHitPoints= 3f;
    private float _hitPoints= 3f;



    private float _actualTime = 0f;

    public void Initialize() 
    {
        staticInfo.money = PlayerPrefs.GetInt("money",0);
        _bulletLevel =     PlayerPrefs.GetInt("bulletLevel",0);
        _maxHitPoints =    staticInfo.maxHp[PlayerPrefs.GetInt("maxHp",0)];
        _shootTime=        staticInfo.shootTime[PlayerPrefs.GetInt("shootTime",0)];
        _reloadTime=       staticInfo.reloadTime[PlayerPrefs.GetInt("reloadTime",0)];
        _speed=            staticInfo.speed[PlayerPrefs.GetInt("speed",0)];
        _magnet.radius =   staticInfo.magnetSize[PlayerPrefs.GetInt("magnetSize",0)];

        _hitPoints= _maxHitPoints;
        _hpBar = FindObjectOfType<HpBar>();
        _hpBar.UpdateHpBar(_maxHitPoints, _hitPoints);
        _moneyText.text = staticInfo.money+"";
    }
    public void MoveLeft()
    {
        if(_death) return;
        _sp.flipX = true;
        _animator.SetBool("moving", true);
        _airplane.transform.position = Vector3.MoveTowards(_airplane.transform.position, _left.position, _speed * Time.deltaTime);
    }

    public void MoveRight()
    {
        if(_death) return;
        _sp.flipX = false;
        _animator.SetBool("moving", true);
        _airplane.transform.position = Vector3.MoveTowards(_airplane.transform.position, _right.position, _speed * Time.deltaTime);
    }
    
    public void StopMoving() 
    {
        _animator.SetBool("moving", false);
    }

    public void GetDamage(int damage) 
    {
        if (_inviscible) return;
        _animator.SetTrigger("getDamage");
        _hitPoints--;
        _hpBar.UpdateHpBar(_maxHitPoints, _hitPoints);
        Instantiate(_sounds[1]);
        if (_hitPoints<=0) 
        {
            _death = true;
            Instantiate(_effect,_airplane.transform.position,Quaternion.identity);
            _hpBar.UpdateHpBar(_maxHitPoints, 0);
            Destroy(_airplane);
            FindObjectOfType<GameManager>().EndGame();
        }
        StartCoroutine(Damaged());
    }

    private IEnumerator Damaged()
    {
        
        _inviscible = true;
        yield return new WaitForSeconds(0.3f);
        _inviscible = false;

    }
    public void Update() 
    {
        _actualTime-=Time.deltaTime;
        if (!_death && _shoot && _actualTime<0)
        {
            _actualTime = _reloadTime;
            var inst = Instantiate(_bullet[_bulletLevel],_airplane.transform.position,Quaternion.identity);
            Instantiate(_sounds[0]);
            Destroy(inst,3f);
        }
    }

    public void Heal() 
    {
        Instantiate(_sounds[3]);

        if (_maxHitPoints<_hitPoints+2f) 
        {
            _hitPoints = _maxHitPoints;
        }
        else 
        {
            _hitPoints += 2f;
        }
        _hpBar.UpdateHpBar(_maxHitPoints, _hitPoints);
    }

    public void Attack() 
    {
        StartCoroutine(Shoot());
    }
    private IEnumerator Shoot()
    {
        _shoot = true;
        Instantiate(_sounds[4]);
        yield return new WaitForSeconds(_shootTime);
        _shoot = false;
        Instantiate(_sounds[5]);

    }

    public void GetMoney() 
    {
       staticInfo.money++;
       Instantiate(_sounds[2]);
       PlayerPrefs.SetInt("money",staticInfo.money);
       _moneyText.text = staticInfo.money+"";
    }

}
