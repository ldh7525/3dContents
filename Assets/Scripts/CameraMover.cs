using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    Camera mainCamera;
    Shooter shooter;
    public float rotationSpeed = 30f;  // ȸ�� �ӵ�
    public float distance;
    public float height;
    public float shooterDistance;
    private float horizontalAngle = 0f;
    float horizontalInput = 0f; //for Cashing

    private void Awake()
    {
        mainCamera = GetComponentInChildren<Camera>();
        shooter = GetComponentInChildren<Shooter>();
        mainCamera.transform.position = transform.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * distance) + new Vector3(0, height, 0);   // �Ÿ��� ����
        shooter.transform.position = transform.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * shooterDistance) - new Vector3(0, transform.position.y - 1.2f, 0);   // �Ÿ��� ����
    }

    void Update()
    {
        // �¿� ����Ű �Է��� ���� ȸ��
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalAngle -= horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0);        // ī�޶� ȸ�� ������Ʈ

        transform.rotation = rotation;   // �Ÿ��� ����
        mainCamera.transform.LookAt(transform);  // ī�޶� �߽����� �ٶ󺸵��� ����
    }

}


