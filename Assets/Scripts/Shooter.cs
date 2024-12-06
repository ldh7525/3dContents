using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder;
public class Shooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public Transform mainCamera;
    public Transform targetPoint;
    public float launchForce;
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
        // Randomly Choose vegitables form 1 to 5
        GameObject firstProjectile = RandomPrefab();
        // first projectile instantiation
        GameObject projectile = Instantiate(firstProjectile, transform.position, Quaternion.identity);
        projectile.transform.parent = gameObject.transform;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; //projectile freeze

        currentProjectile = projectile;
        SetNextProjectile(); //determine a next vegitable
    }

    void Update()
    {
        // LClick to Shoot
        if (Input.GetMouseButtonDown(0) && isShootable) // isShootable is cool-down
        {
            ShootProjectile(currentProjectile);
            isShootable = false; //prevent continuous shoot
            VeggiesCombine veggiesCombine = currentProjectile.GetComponent<VeggiesCombine>();
            if (veggiesCombine != null && veggiesCombine.canCombine)
            {
                veggiesCombine.canCombine = true;
            }
            currentProjectile.transform.SetParent(null);
            StartCoroutine(WaitForShootableAndGenerateProjectile()); // cool-down start
        }
    }

    void ShootProjectile(GameObject projectile)
    {
        Rigidbody rigid = projectile.GetComponent<Rigidbody>();
        rigid.constraints = RigidbodyConstraints.None; //let vegitables move

        Vector3 targetDirection = (targetPoint.position - transform.position).normalized; //base direction
        if (rigid != null)
        {
            rigid.velocity = targetDirection * launchForce; //set velocity
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
        displacement = (targety0 - cameray0).normalized * radius;

        // projectile instantiation
        GameObject projectile = Instantiate(currentProjectile, transform.position, Quaternion.identity);
        projectile.transform.parent = transform;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; //projectile freeze

        currentProjectile = projectile;
        VeggiesCombine veggiesCombine = currentProjectile.GetComponent<VeggiesCombine>();
    }

    void SetNextProjectile()
    {
        nextProjectile = RandomPrefab();
    }

    GameObject RandomPrefab()
    {
        // Randomly Choose vegitables form 1 to 5
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
