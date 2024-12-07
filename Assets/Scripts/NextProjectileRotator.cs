using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextProjectileRotator : MonoBehaviour
{
    public Shooter shooter;
    public GameObject nextProjectileRotatePosition;
    public GameObject[] PrefabsForRotate;
    public float rotationSpeed;

    private GameObject currentActivePrefab; // 현재 활성화된 Prefab을 추적

    void Update()
    {
        // nextProjectileRotatePosition을 회전시킴
        nextProjectileRotatePosition.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Shooter의 nextProjectile이 업데이트되었는지 확인
        if (shooter != null && shooter.nextProjectile != null)
        {
            string nextProjectileName = shooter.nextProjectile.name;

            // 현재 활성화된 Prefab과 새로운 nextProjectile 이름 비교
            if (currentActivePrefab == null || currentActivePrefab.name != nextProjectileName)
            {
                // 이전 Prefab 비활성화
                if (currentActivePrefab != null)
                {
                    currentActivePrefab.SetActive(false);
                }

                // 새로운 Prefab 활성화
                currentActivePrefab = FindPrefabByName(nextProjectileName);
                if (currentActivePrefab != null)
                {
                    currentActivePrefab.SetActive(true);
                }
            }
        }
    }

    private GameObject FindPrefabByName(string name)
    {
        // PrefabsForRotate 배열에서 이름으로 Prefab을 찾음
        foreach (GameObject prefab in PrefabsForRotate)
        {
            if (prefab != null && prefab.name == name)
            {
                return prefab;
            }
        }
        Debug.LogWarning("Prefab with name " + name + " not found in PrefabsForRotate.");
        return null;
    }
}
