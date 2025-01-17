using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private Line line_prefab;

    [SerializeField] private AudioClip audio_clip;
    
    private Camera cam;

    public const float Resolution = .1f;

    private Line curr_line;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) curr_line = Instantiate(line_prefab, mousePos, Quaternion.identity);

        if (Input.GetMouseButton(0))
        {
            /*if (MainController.singleton._sound_value == 0)
            {
                audioSource.PlayOneShot(audio_clip);
            }*/
            curr_line.SetPosition(mousePos);
        }
    }
}
