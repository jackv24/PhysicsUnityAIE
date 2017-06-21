using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float sensitivity = 5.0f;

    public float smoothing = 0.5f;

    void Update()
    {
        Vector3 rot = transform.eulerAngles;

        rot.x = Mathf.Lerp(rot.x, rot.x + Input.GetAxis("Mouse Y") * sensitivity * -1, smoothing);
        rot.y = Mathf.Lerp(rot.y, rot.y + Input.GetAxis("Mouse X") * sensitivity, smoothing);

        transform.eulerAngles = rot;
    }
}
