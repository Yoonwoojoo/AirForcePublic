using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyRemove : MonoBehaviour
{
    public Transform front;
    [SerializeField] private AudioClip deathSound; // 파괴 소리

    public static EnemyRemove instance;

    //파괴된 적들의 리스트
    public List<GameObject> destroyedEnemies = new List<GameObject>();

    //동기화를 위한 락 오브젝트
    private readonly object lockObject = new object();

    private float maxHP; //초기 체력 저장용 변수
    private float currentHP; //현재 체력

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Initialize(float hp)
    {
        currentHP = hp; // 현재 체력 초기화
        maxHP = hp; // 초기 체력 저장
        front.localScale = new Vector3(1.0f, 1.0f, 1.0f); // 체력바 초기화
    }

    private void Update()
    {
        AreAllEnemiesDestroyed();
        int num = Enemy.instance.enemies.Count;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // 플레이어 정보 가져오기
            PlayerInfoHandler playerInfoHandler = FindObjectOfType<PlayerInfoHandler>();

            // 아이템을 먹든지 말든지 간에 지속적으로 업데이트되는 플레이어 정보에서 AttackSO의 power만큼 Enemy의 체력을 감소시킬 것이다
            currentHP -= playerInfoHandler.UpdatedInfo.attackSO.power;

            if (currentHP <= 0)
            {
                // 파괴된 적 리스트에 추가
                AddDestroyedEnemy(gameObject);
                Enemy.instance.enemies.Remove(gameObject);
                //파괴된 적을 풀링하여 재사용 가능하게 함
                EnemyPool.instance.ReturnEnemyToPool(gameObject);


                // 적이 파괴되었을 때 소리를 재생합니다.
                if (deathSound != null)
                {
                    AudioManager.instance.PlayOneShot(deathSound);
                }
                //파괴된 적을 리스트에서 제거했는지 다시 한 번 확인
                if (!Enemy.instance.enemies.Contains(gameObject))
                    Enemy.instance.enemies.Remove(gameObject);
                Enemy.instance.HandleEnemyDestroyed(gameObject);
                //UI 텍스트 업데이트
                Enemy.instance.UpdateEnemyCountUI();
            }
            else
            {
                // 체력바 업데이트
                front.localScale = new Vector3(currentHP / maxHP, 1.0f, 1.0f);
            }
        }
    }

    // 파괴된 적을 리스트에 추가하는 메서드
    public void AddDestroyedEnemy(GameObject enemy)
    {
        // 이미 리스트에 있는 적인지 확인
        if (!destroyedEnemies.Contains(enemy))
        {
            destroyedEnemies.Add(enemy);
        }
    }

    //파괴한 적 리스트를 클리어하는 메서드
    public void ClearDeadEnemyList()
    {
        lock (lockObject)
        {
            destroyedEnemies.Clear();
        }
    }

    //모든 적이 파괴되었는지 확인하는 메서드
    public bool AreAllEnemiesDestroyed()
    {
        lock (lockObject)
        {
            return Enemy.instance.EnemyCount() == 0;
        }
    }
}
