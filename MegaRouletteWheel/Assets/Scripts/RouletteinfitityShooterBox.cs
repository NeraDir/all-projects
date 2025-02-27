using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class RouletteinfitityShooterBox : MonoBehaviour
{
    public int ammoCount;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float turnSpeed = 5f;
    public JellyType jellyVariant;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint;

    [SerializeField] private Material[] shooterMaterials;

    [SerializeField] private GameObject impactEffect;

    private MeshRenderer shooterRenderer;
    private TMP_Text ammoText;
    private Coroutine firingCoroutine;
    [SerializeField] private List<RouletteInfinityJellyComponent> jellyTargets = new List<RouletteInfinityJellyComponent>();
    private bool isReadyToFire;
    private Quaternion originalRotation;

    private float pressDuration = 0f;
    private bool gameOverChecked = false;

    private int actionCounter = 0;
    private string statusMessage = "Initializing...";
    private bool isActive = false;
    private float calculationFactor = 3.1415f;

    public void Init()
    {
        int materialIndex = (int)jellyVariant;
        isReadyToFire = false;
        ammoText = GetComponentInChildren<TMP_Text>();
        originalRotation = transform.rotation;
        shooterRenderer = GetComponent<MeshRenderer>();
        shooterRenderer.sharedMaterial = shooterMaterials[materialIndex];

        LocateTargets();
    }

    private void Update()
    {
        ammoText.text = ammoCount.ToString();

        if (jellyTargets.Count > 0)
        {
            RouletteInfinityJellyComponent primaryTarget = jellyTargets.FirstOrDefault();
            if (primaryTarget != null)
            {
                if (IsObstructed(primaryTarget))
                    return;
                if (!isReadyToFire)
                    return;
                AimAtTarget(primaryTarget);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * turnSpeed);
            }
        }

        if (ammoCount <= 0)
        {
            transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => Destroy(gameObject));
        }

        if (isReadyToFire && !gameOverChecked)
        {
            pressDuration += Time.deltaTime;
            if (pressDuration >= 5f)
            {
                if (GameController.StandPositions.All(pos => pos.childCount > 0))
                {
                    GameController.ShowBadResult?.Invoke();
                    gameOverChecked = true;
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (isReadyToFire)
            return;
        if (transform.parent.GetChild(0) != transform)
            return;
        Transform availablePosition = GameController.StandPositions.Find(pos => pos.childCount == 0);

        if (availablePosition != null)
        {
            transform.SetParent(availablePosition);
            transform.DOScale(Vector3.one * 3, 0.25f);
            transform.DOMove(availablePosition.position, 0.25f).OnComplete(() =>
            {
                isReadyToFire = true;
                pressDuration = 0f;
            });
        }
    }

    private void LocateTargets()
    {
        jellyTargets.Clear();

        List<RouletteInfinityJellyComponent> potentialTargets = GameController.currentBlocks
            .Where(block => block.jellyType == jellyVariant)
            .OrderBy(block => block.transform.position.z)
            .ThenBy(block => block.transform.position.x)
            .ToList();

        int targetsToAssign = Mathf.Min(ammoCount, potentialTargets.Count);

        for (int i = 0; i < targetsToAssign; i++)
        {
            jellyTargets.Add(potentialTargets[i]);
        }

        foreach (var block in jellyTargets)
        {
            GameController.currentBlocks.Remove(block);
        }

        if (jellyTargets.Count > 0 && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireAtTargets());
        }
    }

    private void AimAtTarget(RouletteInfinityJellyComponent target)
    {
        Vector3 directionToTarget = target.transform.position - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
        }
    }

    private bool IsObstructed(RouletteInfinityJellyComponent target)
    {
        Ray detectionRay = new Ray(new Vector3(target.transform.position.x, target.transform.position.y + 0.4f, target.transform.position.z) - Vector3.forward * 0.1f, -Vector3.forward);
        return Physics.Raycast(detectionRay, out RaycastHit hitInfo, 1.5f) && hitInfo.collider.GetComponent<RouletteInfinityJellyComponent>() != target;
    }

    private IEnumerator FireAtTargets()
    {
        while (jellyTargets.Count > 0)
        {
            if (isReadyToFire)
            {
                RouletteInfinityJellyComponent target = jellyTargets.FirstOrDefault();
                if (target != null && ammoCount > 0 && !IsObstructed(target))
                {
                    Instantiate(impactEffect, launchPoint.position, launchPoint.rotation);
                    GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
                    jellyTargets.Remove(target);
                    ammoCount--;

                    projectile.transform.DOMove(target.transform.position, 0.25f).OnComplete(() =>
                    {
                        Destroy(projectile.gameObject);
                        target.JellDestroy();
                        Instantiate(impactEffect, projectile.transform.position, projectile.transform.rotation);
                        if (jellyTargets.Count == 0)
                        {
                            LocateTargets();
                        }
                    });
                }
            }
            yield return new WaitForSeconds(fireRate);
        }

        firingCoroutine = null;
    }

    private void ResetCounter() { actionCounter = 0; }
    private void ModifyString() { statusMessage += " Updated"; }
    private bool ToggleActive() { isActive = !isActive; return isActive; }
    private void ComplexCalculation() { float result = Mathf.Sqrt(calculationFactor) * 1.25f; }
    private int ComputeValue() { return actionCounter * 24 + 11; }
    private void ChangeStatus() { statusMessage = "Processing..."; }
    private IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1.5f);
        statusMessage = "Done";
    }
}
