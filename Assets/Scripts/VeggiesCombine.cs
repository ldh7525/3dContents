using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggiesCombine : MonoBehaviour
{
    public GameObject nextFruit;
    public ParticleSystem particle_circle;
    private ParticleSystem particle_shiny;
    private bool isShiny = false;
    public int combinationScore;
    public bool isCombined;
    public bool canCombine;

    // rotten vegitable implement
    public Renderer rend;
    public ParticleSystem particle_rot;
    public float rotDelay = 0f;
    public float elapsedTime = 0f;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        particle_shiny = GetComponentInChildren<ParticleSystem>();
        if (particle_shiny != null) particle_shiny.gameObject.SetActive(false);
        // Shiny vegitable determined when it instantiated with the probability of 20%
        if (Random.Range(1, 101) > 95) 
        {
            isShiny = true;
            if (particle_shiny != null) particle_shiny.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (canCombine) elapsedTime += Time.deltaTime;
        if (elapsedTime >= rotDelay)
        {
            canCombine = false;
            elapsedTime = -1.0f;
            particle_shiny.gameObject.SetActive(false);
            isShiny = false;
            Instantiate(particle_rot, transform.position, Quaternion.identity).Play();

            if (rend != null) {rend.material.color *= 0.3f; return;}
            
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                // 텍스처를 반 정도 어둡게 변환
                renderer.material.color *= 0.5f;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "ClassicRoundTable1" && (GameManager.Instance.isGameOver == false))
        {
            Debug.Log("collision to " + other.gameObject.name);
            GameManager.Instance.OnGameOver();
        }

        if (name == other.gameObject.name) HandleCollision(other);
    }
    /*
    private void OnCollisionStay(Collision other)
    {
        HandleCollision(other);
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            GameManager.Instance.OnGameOver();
            Destroy(gameObject);
        }
    }

    private void HandleCollision(Collision other)
    {
        if (GameManager.Instance.isGameOver) return;
        
        VeggiesCombine otherVeggie = other.gameObject.GetComponent<VeggiesCombine>();
        if (otherVeggie != null && isCombined != true && canCombine && otherVeggie.canCombine)
        {
            if (isShiny)
                GameManager.Instance.shinyCombine(combinationScore);
            if (otherVeggie.isShiny)
                GameManager.Instance.shinyCombine(combinationScore);
            isCombined = true;
            otherVeggie.Combine();
            ScoreManager.Instance.AddScore(combinationScore);
            Instantiate(particle_circle, Vector3.Lerp(transform.position, other.transform.position, 0.5f), Quaternion.identity).Play();

            if (nextFruit != null) 
            {
                GameObject spawnedFruit = Instantiate(nextFruit, Vector3.Lerp(transform.position, other.transform.position, 0.5f), Quaternion.identity);
                spawnedFruit.transform.localScale = nextFruit.transform.localScale * 0.5f;
                spawnedFruit.GetComponent<VeggiesCombine>().canCombine = true;
                spawnedFruit.GetComponent<VeggiesCombine>().StartGrowing(0.15f);
            }

            Destroy(gameObject);
        }
    }

    public void Combine()
    {
        isCombined = true;
        Destroy(gameObject);
    }

    public void StartGrowing(float duration)
    {
        // Grow time = 0.15f
        StartCoroutine(GrowToOriginalSize(this.gameObject, duration));
    }

    private IEnumerator GrowToOriginalSize(GameObject obj, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;
        Vector3 targetScale = obj.transform.localScale * 2;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.localScale = Vector3.Slerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.transform.localScale = targetScale;
    }

}