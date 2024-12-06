using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggiesCombine : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject nextFruit;
    public int combinationScore;
    public bool isCombined;
    public bool canCombine;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleCollision(other);
    }

    private void OnCollisionStay(Collision other)
    {
        HandleCollision(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
            gameManager.OnGameOver();
    }

    private void HandleCollision(Collision other)
    {
        if (!GameManager.Instance.isGameOver) 
        {
            if (name == other.gameObject.name)
            {
                VeggiesCombine otherVeggie = other.gameObject.GetComponent<VeggiesCombine>();
                if (otherVeggie != null)
                {
                    if (!isCombined && canCombine && otherVeggie.canCombine)
                    {
                        isCombined = true;
                        otherVeggie.Combine();
                        ScoreManager.Instance.AddScore(combinationScore);

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
            }
        }
    }

    public void Combine()
    {
        isCombined = true;
        Destroy(gameObject);
    }

    public void StartGrowing(float duration)
    {
        // 0.15�� ���� ���������� ���� ũ��� ���ƿ��� �ϴ� �ڷ�ƾ ����
        StartCoroutine(GrowToOriginalSize(this.gameObject, duration));
    }

    // ���������� ���� ũ��� ���ƿ��� �ڷ�ƾ
    private IEnumerator GrowToOriginalSize(GameObject obj, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;  // �ʱ� ũ�� (1/2 ũ��(=���Ǵ� 1/8.))
        Vector3 targetScale = (obj.transform.localScale) * 2;  // ��ǥ ũ�� (���� ũ��)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.localScale = Vector3.Slerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ���� ���� �� ��ǥ ũ��� ��Ȯ�� ����
        obj.transform.localScale = targetScale;
    }

}