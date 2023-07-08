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

        transform.right = mainCamera.ScreenToWorldPoint(Vector3.right);
        transform.forward = -mainCamera.ScreenToWorldPoint(Vector3.up);
    }
}
