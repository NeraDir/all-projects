using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SuperGameTutor : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject gamePageController;

    public delegate void TutorComplete();
    public static event TutorComplete TutorCompleted;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (TutorCompleted != null)
        {
            TutorCompleted();
        }

        gamePageController.SetActive(true);
        gameObject.SetActive(false);
    }
}
