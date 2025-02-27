using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BettysSliderComponent : MonoBehaviour
{
    public Transform sliderHandle;
    public Transform sliderMin;
    public Transform sliderMax;
    public float minValue = 0f;
    public float maxValue = 1f;
    private bool isDragging = false;
    private Camera mainCamera;

    public string additionalTxt;
    public TMP_Text sliderValueTxt;

    public bool isMusic;
    public bool isSound;

    void Start()
    {
        mainCamera = Camera.main;
        if (sliderValueTxt != null)
            sliderValueTxt.text = additionalTxt + (CustomRound((int)(GetSliderValue() * 100))) + "%";
    }

    void Update()
    {
        if (isDragging && Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPoint = hit.point;
                Vector3 clampedPosition = ClampToSlider(hitPoint);
                sliderHandle.position = clampedPosition;
                if (sliderValueTxt != null)
                    sliderValueTxt.text = additionalTxt + (CustomRound((int)(GetSliderValue() * 100))) + "%";
                Debug.Log(GetSliderValue());
                if (isMusic)
                    ProfileData.BettersMusicVolume = GetSliderValue();
                if (isSound)
                    ProfileData.BettersSoundVolume = GetSliderValue();
            }
        }
    }

    private Vector3 ClampToSlider(Vector3 position)
    {
        Vector3 direction = sliderMax.position - sliderMin.position;
        float length = direction.magnitude;
        direction.Normalize();

        Vector3 toPoint = position - sliderMin.position;
        float dot = Mathf.Clamp(Vector3.Dot(toPoint, direction), 0, length);

        return sliderMin.position + direction * dot;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private int CustomRound(int number)
    {
        if (number % 5 == 0)
        {
            return number;
        }
        return (int)Math.Round(number / 10.0) * 10;
    }

    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPoint = hit.point;
                sliderHandle.position = ClampToSlider(hitPoint);
            }
        }
    }

    public float GetSliderValue()
    {
        float totalDistance = Vector3.Distance(sliderMin.position, sliderMax.position);
        float currentDistance = Vector3.Distance(sliderMin.position, sliderHandle.position);
        float normalizedValue = Mathf.Clamp01(currentDistance / totalDistance);
        return Mathf.Lerp(minValue, maxValue, normalizedValue);
    }
}
