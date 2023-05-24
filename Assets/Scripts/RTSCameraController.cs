using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    [SerializeField] private float _cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        if (mousePosition.x <= 1)
        {
            transform.position = transform.position - Vector3.right * _cameraSpeed * Time.deltaTime;
        }
        if (mousePosition.y <= 1)
        {
            transform.position = transform.position - Vector3.forward * _cameraSpeed * Time.deltaTime;
        }
        if (mousePosition.x >= Screen.width - 10)
        {
            transform.position = transform.position - Vector3.left * _cameraSpeed * Time.deltaTime;
        }
        if (mousePosition.y >= Screen.height - 10)
        {
            transform.position = transform.position - Vector3.back * _cameraSpeed * Time.deltaTime;
        }


    }
}
