using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Movement : MonoBehaviour
{
    public static Movement Instance;

    public TMP_Text StarsShowAmount;
    public TMP_Text MetresShowAmount;

    [SerializeField] private float _InitialVelocity;
    [SerializeField] private float _Angle;

    [SerializeField] private GameObject _bullet;

    [SerializeField]
    public Transform _FirePoint;

    public Transform _targetTransform;
    private Vector3 TargetPos;

    [SerializeField]
    private GameObject _shootingEffect;

    [SerializeField]
    private GameObject _boomEffect;

    private float _timer = 0;
    public bool InMovement = false;
    public bool InMovementPlane = false;

    public static bool InFly = false;

    public float num = 4f;

    public Joystick joystick;

    public bool FirstZalp = false;

    private List<GameObject> rocketEngine = new();

    private int currerentStars = 0;
    private int currerentMetres = 0;

    public int CurrerentStars
    {
        get
        {
            return currerentStars;
        }
        set
        {
            currerentStars = value;
            StarsShowAmount.text = $"x{value}";
        }
    }

    public int CurrentMetres
    {
        get
        {
            return currerentMetres;
        }
        set
        {
            currerentMetres = value;
            MetresShowAmount.text = $"{value} m";
        }
    }

    private void Start()
    {
        if (Instance == null)
            Instance = this;

        StarsShowAmount.text = $"x0";

        _FirePoint = transform;
        CurrentMetres = 0;
    }

    public void StopMove()
    {
        InMovementPlane = true;
        StopAllCoroutines();
    }

    private void Update()
    {
        //_targetTransform.position = new Vector3(_targetTransform.position.x, _targetTransform.position.y, _targetTransform.position.z + joystick.Horizontal * 5f);
        TargetPos = new Vector3(_targetTransform.position.x - num, 2.15f, _targetTransform.position.z + joystick.Horizontal * 5f);
    }

    public void Move()
    {
        InFly = true;
        TargetPos = new Vector3(_targetTransform.position.x - num, 2.15f, _targetTransform.position.z + joystick.Horizontal * 5f);
        rocketEngine.Add(Instantiate(_shootingEffect, transform));

        foreach (var it in rocketEngine)
            it.transform.position = new Vector3(transform.position.x/* + 1.5f*/, transform.position.y, transform.position.z);
        //TargetPos = new Vector3(transform.position.x - 4f, 2.15f, transform.position.z);

        Vector3 direction = TargetPos - _FirePoint.position;
        Vector3 groundDirection = new Vector3(direction.x, 0, direction.z);
        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0);

        float height = targetPos.y + targetPos.magnitude / 2f;
        height = Mathf.Max(0.01f, height);
        float angle;
        float v0;
        float time;

        CalculatePathWithHight(targetPos, height, out v0, out angle, out time);

        StartCoroutine(CouratineMovement(_FirePoint.position, angle, groundDirection.normalized, v0, time));

        //GameObject Bullet = Instantiate(_bullet);
    }

    public void SpawnEffect()
    {

    }

    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHight(Vector3 targetPos, float h, out float v0, out float angle, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = -yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);

        time = tplus > tmin ? tplus : tmin;
        angle = Mathf.Atan(b * time / xt);
        v0 = b / Mathf.Sin(angle);
    }

    private void ColculatePath(Vector3 targetPos, float angle, out float v0, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float v1 = Mathf.Pow(xt, 2) * g;
        float v2 = 2 * xt * Mathf.Sin(angle) * Mathf.Cos(angle);
        float v3 = 2 * yt * Mathf.Pow(Mathf.Cos(angle), 2);
        v0 = Mathf.Sqrt(v1 / (v2 - v3));

        time = xt / (v0 * Mathf.Cos(angle));
    }

    IEnumerator CouratineMovement(Vector3 firePoint, float angle, Vector3 direction, float velocity, float time)
    {
        float t = 0;
        while (t < time)
        {
            float x = velocity * t * Mathf.Cos(angle);
            float y = velocity * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);

            transform.position = firePoint + direction * x + Vector3.up * y;

            CurrentMetres++;

            t += Time.deltaTime;
            yield return null;
        }
        // Instantiate(_boomEffect, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);

        if (InFly)
        {
            t = 0;
            while (t < time)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z), 15f * Time.deltaTime);
                yield return null;
            }
            InFly = false;
        }
        else if (!InFly)
        {
            Instantiate(_boomEffect, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        }

        if (rocketEngine != null)
        {
            foreach (var it in rocketEngine)
            {
                Destroy(it);
            }

            rocketEngine.Clear();
        }

        InMovement = false;
        InMovementPlane = false;
    }
}
