using System.Collections;
using UnityEngine;
public class Shooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs; // �߻�ü ������ �迭
    public Transform mainCamera;       // �߻� ��� (���� ��ġ)
    public Transform targetPoint;       // ��ǥ ���� (Ư�� ����)
    public float launchForce;         // �⺻ �߻� �ӵ�
    public float radius;
    public bool isShootable = true;
    public float height;

    [SerializeField] private GameObject nextProjectile;
    [SerializeField] private GameObject currentProjectile;
    [SerializeField] Vector3 displacement; //��ǥ ���� ����

    private void Awake()
    {
        Vector3 targety0 = new Vector3(targetPoint.position.x, 0f, targetPoint.position.z);
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, mainCamera.position.z);
        displacement = (targety0 - cameray0).normalized * radius;
    }

    void Start()
    {
        // �߻�ü ���� ����(prefab 1~5 �߿��� 1��)
        GameObject firstProjectile = RandomPrefab();
        // ������ �߻�ü ����
        GameObject projectile = Instantiate(firstProjectile, mainCamera.position + new Vector3(0.0f, height, 0.0f) + displacement, Quaternion.identity);
        currentProjectile = projectile;
        Rigidbody rigid = currentProjectile.GetComponent<Rigidbody>();
        rigid.useGravity = false; //projectile�� �߷� ��Ȱ��ȭ

        nextProjectile = RandomPrefab(); //���� �߻�ü �����صα�
    }

    void Update()
    {
        // �߻�
        if (Input.GetMouseButtonDown(0) && isShootable) // ���콺 ���� Ŭ������ �߻�
        {
            ShootProjectile(currentProjectile);
            isShootable = false; //�߻� ��Ÿ�� 0.6�� 
            StartCoroutine(WaitForShootableAndGenerateProjectile()); // ��� �� ���������־����Ÿ� ����/�ű��

            //���������ִ��Ÿ� current�οű��, ������ �����س���
        }
    }

    void ShootProjectile(GameObject projectile)
    {
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        rigid.useGravity = true;//�߷� Ȱ��ȭ

        Vector3 targetDirection = (targetPoint.position - mainCamera.position).normalized;        // ��ǥ ������ �߻� ��� �� �Ÿ� ���
        if (rigid != null)
        {
            rigid.velocity = targetDirection * launchForce; // �ʱ� �ӵ� ����
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
        displacement = (targety0 - cameray0).normalized * radius; //xz����� ���� ���� - radius �������� ���� ���� ����


        // ������ ���� �߻�ü ����
        GameObject projectile = Instantiate(currentProjectile, mainCamera.position + new Vector3(0.0f, height, 0.0f) + displacement, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.useGravity = false;
        currentProjectile = projectile;
    }

    void SetNextProjectile()
    {
        nextProjectile = RandomPrefab(); //���� �߻�ü �����ϱ�
    }

    GameObject RandomPrefab()
    {
        // �߻�ü ���� ����(prefab 1~5 �߿��� 1��)
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
