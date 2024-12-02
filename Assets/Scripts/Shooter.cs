using UnityEngine;
public class DirectedProjectileShooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // 발사체 프리팹 배열
    public Transform mainCamera;       // 발사 장소 (고정 위치)
    public Transform targetPoint;       // 목표 지점 (특정 지점)
    public float launchForce = 10f;         // 기본 발사 속도
    public float maxVerticalAngle = 60f;    // 최대 발사 각도 (60도)
    public int trajectoryResolution = 50;  // 궤적 점 개수
    public float radius;
    Vector3 displacement;

    void Update()
    {
        // 발사
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭으로 발사
        {
            ShootProjectile();
        }

        Vector3 targety0 = new Vector3(targetPoint.position.x, 0f, targetPoint.position.z);
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, mainCamera.position.z);
        displacement = (targety0 - cameray0).normalized * radius;
    }

    void ShootProjectile()
    {
        // 목표 지점과 발사 장소 간 거리 계산
        Vector3 targetDirection = (targetPoint.position - mainCamera.position).normalized;

        // 발사체 랜덤 지정(prefab 1~5 중에서 1개)
        if (projectilePrefabs.Length == 0) return; // 배열이 비어 있으면 반환

        int randomIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject selectedPrefab = projectilePrefabs[randomIndex];

   

        // 발사체 생성
        GameObject projectile = Instantiate(selectedPrefab, mainCamera.position + new Vector3(0.0f, -0.4200002f, 0.0f)+ displacement, Quaternion.identity); 
        Debug.Log(mainCamera.position + new Vector3(0.0f, -0.4200002f, 0.0f) + displacement); // 생성지점 테스트용 출력
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = targetDirection * launchForce; // 초기 속도 설정
        }


    }
}
