using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraMover : MonoBehaviour
{
    public Transform target;  // 중심점
    public float rotationSpeed = 5f;  // 회전 속도
    public float distance;
    public float height;

    private float horizontalAngle = 0f;

    private void Awake()
    {
        transform.position = (target.position + (Quaternion.Euler(0, horizontalAngle, 0) * Vector3.back * distance) + new Vector3(0, height, 0));   // 거리는 고정
    }

    void Update()
    {
        // 좌우 방향키 입력을 통한 회전
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalAngle += horizontalInput * rotationSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, horizontalAngle, 0);        // 카메라 회전 업데이트


        transform.position = (target.position + (rotation * Vector3.back * distance) + new Vector3(0, height, 0));   // 거리는 고정


        transform.LookAt(target);  // 카메라가 중심점을 바라보도록 설정
    }

}


