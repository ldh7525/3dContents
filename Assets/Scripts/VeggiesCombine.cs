using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeggiesCombine : MonoBehaviour
{
    public GameObject nextFruit;
    public int combinationScore;
    public bool isCombined;


    private void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.isGameOver)
        {
            if (name == other.gameObject.name)
            {
                if (!isCombined)
                {
                    isCombined = true;
                    other.gameObject.GetComponent<VeggiesCombine>().Combine();
                    ScoreManager.Instance.AddScore(combinationScore);
                    if (nextFruit != null)
                    {
                        // 상위 과일을 1/4 크기로 생성
                        GameObject spawnedFruit = Instantiate(nextFruit, Vector3.Lerp(transform.position, other.transform.position, 0.5f), Quaternion.identity);
                        spawnedFruit.transform.localScale = nextFruit.transform.localScale * 0.5f; // 초기 크기 설정

                        // 새 과일의 VeggiesCombine 스크립트를 가져와서 코루틴 시작
                        spawnedFruit.GetComponent<VeggiesCombine>().StartGrowing(0.15f);
                    }
                    Debug.Log("Starting Coroutine");
                    Destroy(gameObject);
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
        // 0.15초 동안 점진적으로 원래 크기로 돌아오게 하는 코루틴 시작
        StartCoroutine(GrowToOriginalSize(this.gameObject, duration));
    }

    // 점진적으로 원래 크기로 돌아오는 코루틴
    private IEnumerator GrowToOriginalSize(GameObject obj, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;  // 초기 크기 (1/2 크기(=부피는 1/8.))
        Vector3 targetScale = (obj.transform.localScale) * 2;  // 목표 크기 (원래 크기)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            obj.transform.localScale = Vector3.Slerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 루프 종료 후 목표 크기로 명확히 설정
        obj.transform.localScale = targetScale;
        Debug.Log("1");
    }

}
