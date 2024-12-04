using System.Collections;
using UnityEngine;
public class Shooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // 발사체 프리팹 배열
    public Transform mainCamera;       // 발사 장소 (고정 위치)
    public Transform targetPoint;       // 목표 지점 (특정 지점)
    public float launchForce;         // 기본 발사 속도
    public float radius;
    public bool isShootable = true;
    public float height;

    [SerializeField] private GameObject nextProjectile;
    [SerializeField] private GameObject currentProjectile;
    [SerializeField] Vector3 displacement; //목표 향한 벡터

    private void Awake()
    {
        Vector3 targety0 = new Vector3(targetPoint.position.x, 0f, targetPoint.position.z);
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, mainCamera.position.z);
        displacement = (targety0 - cameray0).normalized * radius;
    }

    void Start()
    {
        // 발사체 랜덤 지정(prefab 1~5 중에서 1개)
        GameObject firstProjectile = RandomPrefab();
        // 지정된 발사체 생성
        GameObject projectile = Instantiate(firstProjectile, mainCamera.position + new Vector3(0.0f, height, 0.0f) + displacement, Quaternion.identity);
        currentProjectile = projectile;
        Rigidbody rigid = currentProjectile.GetComponent<Rigidbody>();
        rigid.useGravity = false; //projectile의 중력 비활성화

        nextProjectile = RandomPrefab(); //다음 발사체 지정해두기
    }

    void Update()
    {
        // 발사
        if (Input.GetMouseButtonDown(0) && isShootable) // 마우스 왼쪽 클릭으로 발사
        {
            ShootProjectile(currentProjectile);
            isShootable = false; //발사 쿨타임 0.6초 
            StartCoroutine(WaitForShootableAndGenerateProjectile()); // 대기 후 다음꺼에있었던거를 생성/옮기기

            //다음꺼에있던거를 current로옮기고, 다음꺼 지정해놓기
        }
    }

    void ShootProjectile(GameObject projectile)
    {
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        rigid.useGravity = true;//중력 활성화

        Vector3 targetDirection = (targetPoint.position - mainCamera.position).normalized;        // 목표 지점과 발사 장소 간 거리 계산
        if (rigid != null)
        {
            rigid.velocity = targetDirection * launchForce; // 초기 속도 설정
        }
        VeggiesCombine veggiesCombine = projectile.GetComponent<VeggiesCombine>();
        if (veggiesCombine != null)
        {
            veggiesCombine.canCombine = true;
        }
    }

    void GenerateProjectile()
    {
        Vector3 targety0 = new Vector3(targetPoint.position.x, 0f, targetPoint.position.z);
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, mainCamera.position.z);
        displacement = (targety0 - cameray0).normalized * radius; //xz평면의 간격 지정 - radius 수정으로 비율 조정 가능


        // 지정된 다음 발사체 생성
        GameObject projectile = Instantiate(currentProjectile, mainCamera.position + new Vector3(0.0f, height, 0.0f) + displacement, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.useGravity = false;
        currentProjectile = projectile;
    }

    void SetNextProjectile()
    {
        nextProjectile = RandomPrefab(); //다음 발사체 지정하기
    }

    GameObject RandomPrefab()
    {
        // 발사체 랜덤 지정(prefab 1~5 중에서 1개)
        int randomIndex = Random.Range(0, projectilePrefabs.Length);
        GameObject selectedPrefab = projectilePrefabs[randomIndex];
        return selectedPrefab;
    }

    IEnumerator WaitForShootableAndGenerateProjectile()
    {
        yield return new WaitForSeconds(0.6f);
        currentProjectile = nextProjectile;
        GenerateProjectile();
        isShootable = true;
        SetNextProjectile();
        VeggiesCombine veggiesCombine = currentProjectile.GetComponent<VeggiesCombine>();
        if (veggiesCombine != null)
        {
            veggiesCombine.canCombine = false;
        }
    }
}
