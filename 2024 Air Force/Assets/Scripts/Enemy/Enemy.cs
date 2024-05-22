using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //현재 스테이지 타입
    public StageType currentStageType;
    //플레이어 비행기 위치
    public Transform player;
    //이동 간격 시간
    public float moveInterval = 4f;
    //이동 시간
    public float moveDuration = 3f;
    //이동 거리
    public float moveDistance = 2f;
    //플레이어 안전 범위 반경
    private float radius = 4f;
    public Text randomRoundTxt;
    public Text circleRoundTxt;
    public Text bossRoundTxt;
    public Text allClearTxt;
    public GameObject ResultUI;

    //게임화면 적 이미지, 남은 수 UI
    public Image enemyUIImage;
    public Text enemyCountText;

    //생성된 적 비행기 리스트
    public List<GameObject> enemies = new List<GameObject>();
    //동기화를 위한 락 오브젝트
    private readonly object lockObject = new object(); 

    public static Enemy instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    //제거된 적이 아직 있다면 제거하는 메서드
    public void HandleEnemyDestroyed(GameObject enemy)
    {
        lock (lockObject)
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    void Start()
    {
        //프레임 속도 설정
        Application.targetFrameRate = 60;

        //게임 시작 시 스테이지 타입이 설정되지 않은 경우 랜덤 스테이지로 시작
        if (currentStageType == StageType.None)
        {
            randomRoundTxt.gameObject.SetActive(true);
            GenerateEnemies(StageType.Random);
        }
        //MoveEnemiesPeriodically 코루틴 실행
        StartCoroutine(MoveEnemiesPeriodically());
    }

    void Update()
    {
        UpdateEnemyDirections();
        // 스테이지가 종료되었는지 확인
        if (currentStageType != StageType.None)
        {
            CheckForStageCompletion(currentStageType);
        }
    }

    public int EnemyCount()
    {
        lock (lockObject)
        {
            return enemies.Count;
        }
    }

    //적의 방향 업데이트 메서드
    void UpdateEnemyDirections()
    {
        if (EnemyRemove.instance.destroyedEnemies.Contains(gameObject))
            return;

        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                // 보스 모드일 때는 회전하지 않음
                if (currentStageType == StageType.Boss)
                    continue;

                //플레이어를 향하는 방향 벡터 계산
                Vector2 direction = ((Vector2)player.position - (Vector2)enemy.transform.position).normalized;
                //적 비행기의 방향 설정
                enemy.transform.up = direction;
            }
        }
    }

    // 스테이지 타입에 따라 적 비행기를 생성하는 코루틴
    public void StartNextStage(StageType nextStage)
    {
        currentStageType = nextStage;
        GenerateEnemies(nextStage);
        StartCoroutine(MoveEnemiesPeriodically());
    }

    //적 비행기 생성
    public void GenerateEnemies(StageType stageType)
    {
        enemies.Clear();
        currentStageType = stageType; // 현재 스테이지 타입 설정
        //스테이지 타입에 따른 적 비행기 정보 가져오기
        EnemyInfo info = StageManager.instance.GetStageStats(stageType);

        //BulletGenerator에 총알 스프라이트 설정
        BulletGenerator.instance.SetBulletSprite(info.bulletSprite, info.beemSprite);

        //UI 이미지 업데이트
        UpdateEnemyImageUI(info.enemySprite);

        for (int i = 0; i < info.enemyCount; i++)
        {
            //적 비행기의 위치 계산
            Vector2 enemyPos = GetEnemyPosition(stageType, i, info.enemyCount);
            //적 비행기 생성 및 리스트 추가
            GameObject enemy = SetupEnemy(enemyPos, info);
            enemies.Add(enemy);

            //적 비행기가 파괴되지 않은 경우 총알 발사
            if (!EnemyRemove.instance.destroyedEnemies.Contains(enemy))
                BulletGenerator.instance.EnemyFireBullet(enemy.transform, stageType);
        }
        UpdateEnemyCountUI();
    }

    //스테이지 별 적 비행기 위치 계산 메서드
    Vector2 GetEnemyPosition(StageType stageType, int index, int enemyCount)
    {
        Vector2 enemyPos = Vector2.zero;
        switch (stageType)
        {
            case StageType.Random:
                enemyPos = StageManager.instance.RandomMode(enemyPos);
                break;
            case StageType.Circle:
                float angle = (360f / enemyCount) * index;
                float radianAngle = angle * Mathf.Deg2Rad;
                Vector3 cameraCenter = Camera.main.transform.position;
                enemyPos = cameraCenter + new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)) * radius;
                break;
            case StageType.Boss:
                enemyPos = StageManager.instance.BossMode(enemyPos);
                break;
        }
        return enemyPos;
    }

    //적 비행기 설정 메서드
    GameObject SetupEnemy(Vector2 position, EnemyInfo info)
    {   //적 비행기 풀에서 가져오기
        GameObject enemy = EnemyPool.instance.GetEnemyFromPool();
        enemy.transform.position = position;
        enemy.transform.up = Vector2.up;

        //적 비행기 스프라이트 및 크기 설정
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = enemy.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = info.enemySprite;
        //스프라이트 크기 설정
        spriteRenderer.transform.localScale = info.spriteSize; 

        //적 비행기 스탯 설정
        SetEnemyStats(enemy, info);

        //적 HP 설정
        EnemyRemove enemyRemove = enemy.GetComponent<EnemyRemove>();
        if (enemyRemove == null)
        {
            enemyRemove = enemy.AddComponent<EnemyRemove>();
        }
        enemyRemove.Initialize(info.hp);

        enemy.SetActive(true);
        return enemy;
    }

    //적 비행기 스탯 설정 메서드
    void SetEnemyStats(GameObject enemy, EnemyInfo info)
    {
        EnemyInfoHandler enemyInfoHandler = enemy.GetComponent<EnemyInfoHandler>();
        if (enemyInfoHandler == null)
        {   
            enemyInfoHandler = enemy.AddComponent<EnemyInfoHandler>();
        }

        enemyInfoHandler.SetAttributes(info.attackPower, info.hp);
    }

    // 스테이지 종료 확인 후, 다음 스테이지 진행 메서드
    public void CheckForStageCompletion(StageType currentStageType)
    {
        if (EnemyRemove.instance.AreAllEnemiesDestroyed())
        {
            OnAllEnemiesDestroyed(currentStageType);
        }
    }

    // 모든 적 제거 이벤트 처리 메서드
    private void OnAllEnemiesDestroyed(StageType currentStageType)
    {
        EnemyRemove.instance.ClearDeadEnemyList();
        randomRoundTxt.gameObject.SetActive(false);
        circleRoundTxt.gameObject.SetActive(false);
        bossRoundTxt.gameObject.SetActive(false);

        switch (currentStageType)
        {
            case StageType.Random:
                circleRoundTxt.gameObject.SetActive(true);
                StartNextStage(StageType.Circle);
                break;
            case StageType.Circle:
                bossRoundTxt.gameObject.SetActive(true);
                StartNextStage(StageType.Boss);
                break;
            case StageType.Boss:
                allClearTxt.gameObject.SetActive(true);
                ResultUI.gameObject.SetActive(true);
                break;
            default:
                return;
        }
    }

    // 적 비행기들을 주기적으로 이동시키는 코루틴
    IEnumerator MoveEnemiesPeriodically()
    {
        while (true)
        {
            //파괴된 적인 경우 코루틴 종료
            if (EnemyRemove.instance.destroyedEnemies.Contains(gameObject))
                yield break;
            //이동 간격 시간만큼 대기
            yield return new WaitForSeconds(moveInterval);

            //각 적 비행기를 이동시키는 코루틴 시작
            foreach (var enemy in enemies)
            {
                //파괴된 적인 경우 코루틴 종료
                if (EnemyRemove.instance.destroyedEnemies.Contains(enemy))
                    yield break;
                //적이 아직 파괴되지 않은 경우에만 이동시키는 코루틴 실행
                StartCoroutine(MoveEnemy(enemy));
            }
        }
    }

    //개별 적 비행기를 서서히 이동시키는 코루틴
    IEnumerator MoveEnemy(GameObject enemy)
    {
        //적의 현재 위치 저장
        Vector2 startPosition = enemy.transform.position;
        //랜덤한 끝 위치 계산
        Vector2 endPosition = GetRandomEndPosition(startPosition);

        //이동한 시간
        float elapsedTime = 0f;

        //이동 시간 동안 서서히 이동
        while (elapsedTime < moveDuration && !EnemyRemove.instance.destroyedEnemies.Contains(enemy))
        {
            //적을 서서히 이동
            enemy.transform.position = Vector2.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //이동이 끝나면 정확한 끝 위치로 설정
        if (!EnemyRemove.instance.destroyedEnemies.Contains(enemy))
            enemy.transform.position = endPosition;
    }

    //랜덤한 끝 위치 계산하는 메서드
    Vector2 GetRandomEndPosition(Vector2 startPosition)
    {
        // 보스 모드인 경우 x축으로만 이동
        if (StageManager.instance.GetStageStats(StageType.Boss) != null)
        {
            // 카메라 뷰포트의 최소, 최대 좌표 계산
            Vector2 min = CameraViewPointMin();
            Vector2 max = CameraViewPointMax();

            // 랜덤한 x축 위치 설정
            float randomX = Random.Range(min.x + 2, max.x - 2);

            // 끝 위치는 y축은 동일, x축만 랜덤 이동
            return new Vector2(randomX, startPosition.y);
        }
        else
        {
            // 카메라 뷰포트의 최소, 최대 좌표 계산
            Vector2 min = CameraViewPointMin();
            Vector2 max = CameraViewPointMax();

            // 시작 위치에서 이동 가능한 최소, 최대 거리 계산
            Vector2 minMove = min - startPosition;
            Vector2 maxMove = max - startPosition;

            // 랜덤한 이동 방향과 거리 설정
            Vector2 randomDirection = new Vector2(Random.Range(minMove.x + 1, maxMove.x - 1), Random.Range(minMove.y + 1, maxMove.y - 1)).normalized;
            randomDirection *= moveDistance;

            // 랜덤한 끝 위치 반환
            return startPosition + randomDirection;
        }
    }

    public Vector2 CameraViewPointMin()
    {
        //카메라 뷰포트의 네 점의 좌표 계산(최소, 최대)
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        return min;
    }
    public Vector2 CameraViewPointMax()
    {
        //카메라 뷰포트의 네 점의 좌표 계산(최소, 최대)
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        return max;
    }
    //업데이트 게임화면 적 이미지, 적의 수
    void UpdateEnemyImageUI(Sprite enemySprite)
    {
        if (enemyUIImage != null)
        {
            enemyUIImage.sprite = enemySprite;
        }
    }

    // Update the enemy count UI element
    public void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = $"{enemies.Count}";
        }
    }
}