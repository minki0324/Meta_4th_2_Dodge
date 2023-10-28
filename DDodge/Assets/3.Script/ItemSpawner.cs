using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;
    public Transform playerTransform;

    public float maxDistance = 5f;

    public float timeBetSpawnMax = 7f;
    public float timeBetSpawnMin = 2f;

    private float timeBetSpawn;

    private float lastSpawnTime;

    private void Start()
    {
        timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;
    }

    private void Update()
    {
        if (Time.time >= lastSpawnTime + timeBetSpawn && playerTransform != null)
        {
            lastSpawnTime = Time.time;
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Item_Spawner();
        }
    }

    private void Item_Spawner()
    {
        float randomAngle = Random.Range(0f, 360f);

        // 랜덤 각도와 maxDistance를 사용하여 위치 계산
        Vector3 offset = new Vector3(Mathf.Sin(randomAngle * Mathf.Deg2Rad), 0, Mathf.Cos(randomAngle * Mathf.Deg2Rad)) * maxDistance;
        Vector3 spawnPos = new Vector3(playerTransform.position.x + offset.x, 1.2f, playerTransform.position.z + offset.z); 

        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPos, Quaternion.identity);

        Destroy(item, 5f);
    }
}
