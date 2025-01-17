using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class FirstSkill : Skill
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float visibilityRadius;
    public float damage;
    [SerializeField]
    private int reflectionsCount;

    public Enemy[] enemyInScene;

    private Transform myTransform;

    public Enemy target;

    private float minDistance;

    private bool camTransform;
    private bool canTrigger;



    public override void Apply(Enemy target)
    {

        if (target != null)
        {
            myTransform = GetComponent<Transform>();
            minDistance = 100000000f;
            camTransform = false;

            enemyInScene = FindObjectsOfType<Enemy>();

            StartCoroutine(transformLightning());
            reflectionsCount++;
        }
    }

    private void SetSkillConfigsByLevel()
    {
        if (GamePlayConfigs.firstSkillLevel < 5)
        {
            reflectionsCount += GamePlayConfigs.firstSkillLevel - 1;
        }
        damage += (GamePlayConfigs.firstSkillLevel - 1);
    }


    private void Start()
    {
            myTransform = GetComponent<Transform>();
            minDistance = 100000000f;
            camTransform = false;

            enemyInScene = FindObjectsOfType<Enemy>();
            FindFirstTarget();
            StartCoroutine(transformLightning());
            reflectionsCount++;
       
    }


    public void FindFirstTarget()
    {
        for (int i = 0; i < enemyInScene.Length; i++)
        {
            if (Vector3.Distance(myTransform.position, enemyInScene[i].transform.position) < minDistance && enemyInScene[i].gameObject.activeInHierarchy)
            {
                target = enemyInScene[i];
                minDistance = Vector3.Distance(myTransform.position, enemyInScene[i].transform.position);
            }
        }
        if (target != null)
        {

        }
      
    }


    private IEnumerator transformLightning()
    {
        if (reflectionsCount == 0)
        {
            Destroy(gameObject, 6);
        }
        else
        {
            while (myTransform.position != new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z))
            {
                myTransform.position = Vector3.MoveTowards(myTransform.position, new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z), speed);
                myTransform.LookAt(target.transform);
                yield return null;
            }
            target.TakeDamage(damage);
            CheckEnemyAround();
            reflectionsCount--;

            yield return null;
        }

        

        yield return null;
        
    }

    private List<Collider> allCollidersHistory = new();

    public void CheckEnemyAround()
    {
        Enemy newTarget = null;
        float distance = 0;
        float minDistance = 10000000;
        var colliders = Physics.OverlapSphere(myTransform.position, visibilityRadius);
        Collider ignoreCollider = null;

        if (colliders.Length == 0)
            return;

        

        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy) && !allCollidersHistory.Contains(collider))
            {
                distance = Vector3.Distance(myTransform.position, enemy.transform.position);
                if (distance <  minDistance)
                {
                    minDistance = distance;
                    newTarget = enemy;
                    ignoreCollider = collider;
                }
            }
        }

        

        if (newTarget != null)
        {
            target = newTarget;
            AddColliderToHistory(ignoreCollider);
            StartCoroutine(transformLightning());
        }
        else
        {
            Destroy(gameObject,6);
        }
    }

    public void AddColliderToHistory(Collider collider)
    {
        if (!allCollidersHistory.Contains(collider))
        {
            allCollidersHistory.Add(collider);
        }
        
    }

}
