using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Transform player;
    //적 비행기 프리팹
    public GameObject enemyPrefab;
    //적 비행기들이 배치될 반경
    private float radius = 4f;

    public static StageManager instance;

    //각 스테이지의 EnemyInfo를 인스펙터에서 설정
    public EnemyInfo randomStageInfo;
    public EnemyInfo circleStageInfo;
    public EnemyInfo bossStageInfo;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //각 스테이지의 적 스탯을 반환하는 메서드
    public EnemyInfo GetStageStats(StageType stageType)
    {
        switch (stageType)
        {
            case StageType.Random:
                return randomStageInfo;
            case StageType.Circle:
                return circleStageInfo;
            case StageType.Boss:
                return bossStageInfo;
            default:
                return null;
        }
    }

    //랜덤모드 - 1그라운드
    public Vector2 RandomMode(Vector2 enemyPos)
    {
        //카메라 시야 범위
        Vector2 min = Enemy.instance.CameraViewPointMin();
        Vector2 max = Enemy.instance.CameraViewPointMax();

        enemyPos = new Vector2(
            Random.Range(min.x + 1, max.x - 1),
            Random.Range(min.y + 1, max.y - 1)
        );

        // 적 비행기가 플레이어 반경 4f 이내에 있는지 확인
        float distanceToPlayer = Vector2.Distance(enemyPos, player.position);
        if (distanceToPlayer < radius)
        {
            // 반경 4f 밖으로 이동
            Vector2 directionFromPlayer = (enemyPos - (Vector2)player.position).normalized;
            enemyPos = (Vector2)player.position + directionFromPlayer * radius;
        }
        return enemyPos;
    }

    //보스모드 - 3그라운드
    public Vector2 BossMode(Vector2 enemyPos)
    {
        //카메라 시야 범위
        Vector2 min = Enemy.instance.CameraViewPointMin();
        Vector2 max = Enemy.instance.CameraViewPointMax();

        //적 비행기를 카메라 시야 범위 상단 부분에서 랜덤으로 등장시킴
        enemyPos = new Vector2(
            Random.Range(min.x, max.x),
            max.y - 2
        );

        return enemyPos;
    }
}
