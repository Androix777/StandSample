using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float moveSpeed = .2f;
    private Transform mainCameraTransform;
    void Start()
    {
        mainCameraTransform = gameObject.transform;
    }

    void Update()
    {
        // Передвижение камеры
        if (Input.GetMouseButton(1)) 
        {
            mainCameraTransform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * moveSpeed);
            mainCameraTransform.Translate(Vector3.up * -Input.GetAxis("Mouse Y") * moveSpeed);
        }
        // Приближение камеры
        Vector3 pos = mainCameraTransform.position;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            pos = pos - mainCameraTransform.forward;
            mainCameraTransform.position = pos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            pos = pos + mainCameraTransform.forward;
            mainCameraTransform.position = pos;
        }
    }
}
