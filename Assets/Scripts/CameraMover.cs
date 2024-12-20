using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    Camera mainCamera;
    Shooter shooter;
    public float rotationSpeed = 30f;  // rotationSpeed
    public float distance = 1.25f;
    public float height = 0.25f;
    public float shooterDistance = 0.6f;
    private float horizontalAngle = 0f;
    float horizontalInput = 0f; //for Cashing

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
        shooter = GetComponentInChildren<Shooter>();
        mainCamera.transform.position = transform.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * distance) + new Vector3(0, height, 0);   // mainCameraPos
        shooter.transform.position = transform.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * shooterDistance) - new Vector3(0, transform.position.y - 1.15f, 0);   // ShooterPos
    }

    void Update()
    {
        //Stop when Gameover
        if (GameManager.Instance.isGameOver == true) return;
        
        // rotate with <-, -> or A, D
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            horizontalAngle -= horizontalInput * rotationSpeed * 2.0f * Time.deltaTime;
        }
        else
        {
            horizontalAngle -= horizontalInput * rotationSpeed * Time.deltaTime;
        }
        Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0); 
        transform.rotation = rotation;  // rotate

        mainCamera.transform.LookAt(transform);  // mainCamera should look target posizion when its global position moved
    }
}


