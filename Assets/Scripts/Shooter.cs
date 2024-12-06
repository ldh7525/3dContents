using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;
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
    public GameObject currentProjectile;
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
        GameObject projectile = Instantiate(firstProjectile, transform.position, Quaternion.identity);
        projectile.transform.parent = gameObject.transform;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; //projectile ����

        currentProjectile = projectile;
        nextProjectile = RandomPrefab(); //���� �߻�ü �����صα�
    }

    void Update()
    {
        // �߻�
        if (Input.GetMouseButtonDown(0) && isShootable) // ���콺 ���� Ŭ������ �߻�
        {
            ShootProjectile(currentProjectile);
            isShootable = false; //�߻� ��Ÿ�� 0.6�� 
            VeggiesCombine veggiesCombine = currentProjectile.GetComponent<VeggiesCombine>();
            if (veggiesCombine != null && veggiesCombine.canCombine)
            {
                veggiesCombine.canCombine = true;
            }
            currentProjectile.transform.SetParent(null);
            StartCoroutine(WaitForShootableAndGenerateProjectile()); // ��� �� ���������־����Ÿ� ����/�ű��
        }
    }

    void ShootProjectile(GameObject projectile)
    {
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.None; //������ �����ϵ��� Ȱ��ȭ

        Vector3 targetDirection = (targetPoint.position - transform.position).normalized;        // ��ǥ ������ �߻� ��� �� �Ÿ� ���
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
        Vector3 cameray0 = new Vector3(mainCamera.position.x, 0f, transform.position.z);
        displacement = (targety0 - cameray0).normalized * radius; //xz����� ���� ���� - radius �������� ���� ���� ����

        // ������ ���� �߻�ü ����
        GameObject projectile = Instantiate(currentProjectile, transform.position, Quaternion.identity);
        projectile.transform.parent = transform;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; //projectile ����

        currentProjectile = projectile;
        VeggiesCombine veggiesCombine = currentProjectile.GetComponent<VeggiesCombine>();
    }

    void SetNextProjectile()
    {
        nextProjectile = RandomPrefab(); //���� �߻�ü �����ϱ�
    }

    GameObject RandomPrefab()
    {
        // �߻�ü ���� ����(prefab 1~5 �߿��� 1��)
        int randomIndex = Random.Range(0, projectilePrefabs.Length);
        return projectilePrefabs[randomIndex];
    }

    IEnumerator WaitForShootableAndGenerateProjectile()
    {
        yield return new WaitForSeconds(0.6f);
        currentProjectile = nextProjectile;
        GenerateProjectile();
        isShootable = true;
        SetNextProjectile();
    }
}
