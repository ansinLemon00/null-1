using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("설정")]
    public GameObject[] enemyPrefabs; // 소환할 적 프리팹 리스트
    public Transform[] spawnPoints;  // 적이 생성될 위치 리스트
    public float spawnDelay = 2.0f;  // 생성 간격 (초)

    void Start()
    {
        // 게임 시작과 동시에 스폰 루틴 시작
        StartCoroutine(SpawnEnemyRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true) // 무한 루프 (게임 종료 전까지 계속 실행)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay); // 설정한 시간만큼 대기
        }
    }

    void SpawnEnemy()
    {
        // 1. 랜덤하게 적 종류 선택
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

        // 2. 랜덤하게 스폰 위치 선택
        int randomPointIndex = Random.Range(0, spawnPoints.Length);

        // 3. 적 생성 (Instantiate)
        Instantiate(enemyPrefabs[randomEnemyIndex],
                    spawnPoints[randomPointIndex].position,
                    Quaternion.identity);
    }
}