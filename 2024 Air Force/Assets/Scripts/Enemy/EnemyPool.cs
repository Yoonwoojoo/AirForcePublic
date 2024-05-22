using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //적 비행기 프리팹
    public GameObject enemyPrefab;
    //생성할 적 비행기 수
    public int enemyCount = 8;

    //적 비행기 풀을 관리하는 큐
    private Queue<GameObject> enemyPool;

    public static EnemyPool instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        //적 비행기 풀 초기화
        enemyPool = new Queue<GameObject>();
        InitializeEnemyPool();
    }

    //적 비행기 풀 초기화
    void InitializeEnemyPool()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Enqueue(enemy);
        }
    }

    //적 비행기 풀에서 사용 가능한 적 비행기를 가져오는 함수
    public GameObject GetEnemyFromPool()
    {
        if (enemyPool.Count > 0)
        {
            return enemyPool.Dequeue();
        }

        //풀에 사용 가능한 적 비행기가 없는 경우 새로 생성하여 반환
        GameObject newEnemy = Instantiate(enemyPrefab);
        newEnemy.SetActive(false);
        return newEnemy;
    }

    //적 비행기를 풀로 반환하는 함수
    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
