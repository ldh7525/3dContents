using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    public Transform target;  // �߽���
    public float rotationSpeed = 5f;  // ȸ�� �ӵ�
    public float distance;
    public float height;

    private float horizontalAngle = 0f;

    private void Awake()
    {
        transform.position = (target.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * distance) + new Vector3(0, height, 0));   // �Ÿ��� ����
    }

    void Update()
    {
        // �¿� ����Ű �Է��� ���� ȸ��
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0);        // ī�޶� ȸ�� ������Ʈ


        transform.position = (target.position + (rotation * Vector3.back * distance) + new Vector3(0, height, 0));   // �Ÿ��� ����


        transform.LookAt(target);  // ī�޶� �߽����� �ٶ󺸵��� ����
    }

}


