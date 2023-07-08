using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            return;
        }

        transform.position = transform.position + Vector3.up;
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
