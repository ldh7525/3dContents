using UnityEngine;
public class DirectedProjectileShooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // �߻�ü ������ �迭
    public Transform mainCamera;       // �߻� ��� (���� ��ġ)
    public Transform targetPoint;       // ��ǥ ���� (Ư�� ����)
    public float launchForce = 10f;         // �⺻ �߻� �ӵ�
    public float maxVerticalAngle = 60f;    // �ִ� �߻� ���� (60��)
    public int trajectoryResolution = 50;  // ���� �� ����
    public float radius;
    Vector3 displacement;

    void Update()
    {
        // �߻�
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� Ŭ������ �߻�
        {
            ShootProjectile();
        }

        Vector3 targety0 = new Vector3(targetPoint.position.x, 0f, targetPoint.position.z);
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, mainCamera.position.z);
        displacement = (targety0 - cameray0).normalized * radius;
    }

    void ShootProjectile()
    {
        // ��ǥ ������ �߻� ��� �� �Ÿ� ���
        Vector3 targetDirection = (targetPoint.position - mainCamera.position).normalized;

        // �߻�ü ���� ����(prefab 1~5 �߿��� 1��)
        if (projectilePrefabs.Length == 0) return; // �迭�� ��� ������ ��ȯ

        int randomIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject selectedPrefab = projectilePrefabs[randomIndex];

   

        // �߻�ü ����
        GameObject projectile = Instantiate(selectedPrefab, mainCamera.position + new Vector3(0.0f, -0.4200002f, 0.0f)+ displacement, Quaternion.identity); 
        Debug.Log(mainCamera.position + new Vector3(0.0f, -0.4200002f, 0.0f) + displacement); // �������� �׽�Ʈ�� ���
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = targetDirection * launchForce; // �ʱ� �ӵ� ����
        }


    }
}
