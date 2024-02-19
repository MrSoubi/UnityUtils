using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Camera Controller
 * 
 * Put this script on a camera.
 *
 * Use ZQSD to move and the mouse to rotate the camera.
 * Mouse wheel to change the move speed
 * Right click + drag to move on the Y axis
 * Right click + left ctrl + drag to move precisely on the the Y axis
 */

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;
    public float accelerationRate;

    private Vector2 cameraRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (mouseSensitivity == 0)
        {
            mouseSensitivity = 500;
        }

        if (moveSpeed == 0)
        {
            moveSpeed = 15;
        }

        if (accelerationRate == 0)
        {
            accelerationRate = 1000;
        }
    }

    private void Update()
    {
        moveSpeed += Input.GetAxis("Mouse ScrollWheel") * accelerationRate * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseX = -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveSideways = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveUpwards = -mouseX;

        if (Input.GetMouseButton(1))
        {
            float scale = 1.0f;
            if (Input.GetKey(KeyCode.LeftControl))
            {
                scale = 0.05f;
            }
            transform.position += transform.up * moveUpwards * scale;
        }
        else
        {
            cameraRotation.x += mouseX;
            cameraRotation.y += mouseY;

            transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
            transform.position += transform.forward * moveForward + transform.right * moveSideways;
        }
    }
}
