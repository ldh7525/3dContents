using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextProjectileRotator : MonoBehaviour
{
    public Shooter shooter;
    public GameObject nextProjectileRotatePosition;
    public GameObject[] PrefabsForRotate;
    public float rotationSpeed;

    private GameObject currentActivePrefab; // ���� Ȱ��ȭ�� Prefab�� ����

    void Update()
    {
        // nextProjectileRotatePosition�� ȸ����Ŵ
        nextProjectileRotatePosition.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Shooter�� nextProjectile�� ������Ʈ�Ǿ����� Ȯ��
        if (shooter != null && shooter.nextProjectile != null)
        {
            string nextProjectileName = shooter.nextProjectile.name;

            // ���� Ȱ��ȭ�� Prefab�� ���ο� nextProjectile �̸� ��
            if (currentActivePrefab == null || currentActivePrefab.name != nextProjectileName)
            {
                // ���� Prefab ��Ȱ��ȭ
                if (currentActivePrefab != null)
                {
                    currentActivePrefab.SetActive(false);
                }

                // ���ο� Prefab Ȱ��ȭ
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
        // PrefabsForRotate �迭���� �̸����� Prefab�� ã��
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
