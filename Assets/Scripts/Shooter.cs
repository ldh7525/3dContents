using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
public class Shooter : MonoBehaviour
{
    public GameObject[] projectilePrefabs;
    public Transform mainCamera;
    public Transform targetPoint;
    public float launchForce;
    public float maxForce = 4.9f;
    public float minForce = 4.5f;
    public float elevation =  0f;
    public float maxElevation = 5.0f;
    public float minElevation = 1.0f;
    public float verticalInput = 0f;
    public bool isShootable = true;
    public GameObject dirLine;
    private bool shouldShoot = false;

    public GameObject nextProjectile;
    public GameObject currentProjectile;

    //Force Gauge
    public ImgsFillDynamic imgsFillDynamic;

    void Start()
    {
        elevation = maxElevation;
        launchForce = minForce;
        // Randomly Choose vegitables form 1 to 4
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
        // Stop when Gameover
        if (GameManager.Instance.isGameOver) return;

        // ↑, ↓ or W, S to adjust launch direction
        verticalInput = Input.GetAxis("Vertical");
        elevation = elevation - (Time.deltaTime * 3.0f * verticalInput);
        elevation = Mathf.Clamp(elevation, minElevation, maxElevation);
        launchForce = launchForce + (Time.deltaTime * 0.3f * verticalInput);
        launchForce = Mathf.Clamp(launchForce, minForce, maxForce);
        dirLine.transform.LookAt(targetPoint.position + new Vector3(0, elevation, 0)); //direction line rotate
        imgsFillDynamic.SetValue(Mathf.Max(0.01f, (launchForce - minForce) / 0.4f), true);

        // LClick to Shoot
        // 입력 처리
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isShootable && !GameManager.Instance.isPaused)
        {
            shouldShoot = true;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver) return;

        // 물리 처리
        if (shouldShoot)
        {
            shouldShoot = false; // 플래그 초기화
            VeggiesCombine.turn += 1;
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
        if (rigid == null) {Debug.Log("No rigidbody to shoot"); return;}

        rigid.constraints = RigidbodyConstraints.None; //let vegitables move
        Vector3 targetDirection = (targetPoint.position + new Vector3(0, elevation, 0) - transform.position).normalized; //base direction
        rigid.AddForce(targetDirection * launchForce, ForceMode.VelocityChange); //set velocity

        VeggiesCombine veggiesCombine = projectile.GetComponent<VeggiesCombine>();
        if (veggiesCombine != null)
        {
            veggiesCombine.canCombine = true;
        }
    }

    void GenerateProjectile()
    {
        // projectile instantiation
        GameObject projectile = Instantiate(currentProjectile, transform.position, Quaternion.identity);
        projectile.transform.parent = transform;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll; //projectile freeze

        currentProjectile = projectile;
    }

    void SetNextProjectile()
    {
        nextProjectile = RandomPrefab();
    }

    GameObject RandomPrefab()
    {
        // Randomly Choose vegitables form 1 to 4
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
