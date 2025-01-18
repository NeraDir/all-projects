using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class jetCharacterComponent : MonoBehaviour
{
    public static UnityEvent<int,float> onTriggerWithModel = new UnityEvent<int, float>();

    public jetTxtAnimation[] txtAnimations;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out jetRotateModel jetModel))
        {
            Debug.Log(transform.rotation.eulerAngles.z + " Jet");
            Debug.Log(jetModel.transform.rotation.eulerAngles.z + " Model");


            if (transform.rotation.eulerAngles.z < jetModel.transform.rotation.eulerAngles.z +5 && transform.rotation.eulerAngles.z > jetModel.transform.rotation.eulerAngles.z - 5)
            {
                onTriggerWithModel?.Invoke(100, 25);
                Destroy(jetModel.gameObject);
                txtAnimations[0].gameObject.SetActive(true);
            }
            else if ((transform.rotation.eulerAngles.z < jetModel.transform.rotation.eulerAngles.z + 10 && transform.rotation.eulerAngles.z > jetModel.transform.rotation.eulerAngles.z - 10))
            {
                onTriggerWithModel?.Invoke(50, 10);
                Destroy(jetModel.gameObject);
                txtAnimations[1].gameObject.SetActive(true);
            }
            else
            {
                onTriggerWithModel?.Invoke(0, 0);
                txtAnimations[2].gameObject.SetActive(true);
                Destroy(jetModel.gameObject);
            }
        }
    }
}
