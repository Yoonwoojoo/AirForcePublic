using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class BulletGenerator : MonoBehaviour
{
    //총알 프리팹
    public GameObject enemyBulletPrefab;
    //플레이어 위치값
    public Transform player;
    //총알 속도
    public float bulletSpeed = 1f;
    //총알 발사 간격
    public float bulletInterval = 3f;
    //풀 크기 설정
    public int poolSize = 20;

    public static BulletGenerator instance;
    //총알 풀을 관리하는 큐
    private Queue<GameObject> bulletPool;
    
    private Sprite currentBulletSprite; //현재 총알 스프라이트
    private Sprite beemBulletSprite; //10줄 광선 총알 스프라이트

    private void Awake()
    {
        if(instance == null)
            instance = this;
        //총알 풀 초기화
        bulletPool = new Queue<GameObject>();
        InitializePool();
    }

    //풀을 초기화하고 총알 객체를 미리 생성함
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    //총알 스프라이트 설정 함수
    public void SetBulletSprite(Sprite bulletSprite, Sprite beemSprite)
    {
        currentBulletSprite = bulletSprite;
        beemBulletSprite = beemSprite;
    }

    //적이 총알을 발사하도록 하는 함수
    public void EnemyFireBullet(Transform enemyTransform, StageType stageType)
    {   //보스 모드일 때 총알 발사 방식을 다르게 하기
        if (stageType == StageType.Boss)
        {
            StartCoroutine(FireBossPattern(enemyTransform));
        }
        else
        {
            StartCoroutine(FireBulletCoroutine(enemyTransform));
        }
    }

    IEnumerator FireBulletCoroutine(Transform enemyTransform)
    {
        while (true)
        {
            if (enemyTransform == null || !enemyTransform.gameObject.activeSelf)
                break;

            if (EnemyRemove.instance.destroyedEnemies.Contains(enemyTransform.gameObject))
                break;

            //풀에서 총알을 가져옴
            GameObject bullet = GetBulletFromPool();
            //총알 위치 설정
            bullet.transform.position = enemyTransform.position;
            bullet.SetActive(true);

            //총알 스프라이트 설정
            bullet.GetComponent<SpriteRenderer>().sprite = currentBulletSprite;

            //총알 방향 설정
            Vector2 direction = ((Vector2)player.position - (Vector2)enemyTransform.position).normalized;
            //총알 속도 설정
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            //총알 발사 간격
            yield return new WaitForSecondsRealtime(bulletInterval);

        }
    }

    IEnumerator FireBossPattern(Transform enemyTransform)
    {
        while (true)
        {
            if (enemyTransform == null || !enemyTransform.gameObject.activeSelf)
                break;

            if (EnemyRemove.instance.destroyedEnemies.Contains(enemyTransform.gameObject))
                    break;

            //10줄의 광선을 부채꼴 모양으로 발사
            FireFanPattern(enemyTransform, 10, 160f);

            //1초 대기
            yield return new WaitForSecondsRealtime(1f);

            //한 번에 20개의 총알을 원형으로 발사
            FireCircularPattern(enemyTransform, 20);

            yield return new WaitForSecondsRealtime(bulletInterval);
        }
    }
    //10줄의 광선
    void FireFanPattern(Transform enemyTransform, int bulletCount, float angleRange)
    {
        float startAngle = -angleRange / 2;
        float angleStep = angleRange / (bulletCount - 1);
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + (angleStep * i);
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down;
            FireBullet(enemyTransform, direction, beemBulletSprite);
        }
    }
    //한 번에 20개의 총알
    void FireCircularPattern(Transform enemyTransform, int bulletCount)
    {
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = angleStep * i;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;
            FireBullet(enemyTransform, direction, currentBulletSprite);
        }
    }

    void FireBullet(Transform enemyTransform, Vector2 direction, Sprite BulletSprite)
    {
        GameObject bullet = GetBulletFromPool();
        bullet.transform.position = enemyTransform.position;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        //총알 스프라이트 설정
        bullet.GetComponent<SpriteRenderer>().sprite = BulletSprite;
    }

    //총알 풀에서 사용 가능한 총알을 가져오는 함수
    private GameObject GetBulletFromPool()
    {
        //풀에 사용 가능한 총알이 있는 경우 반환
        if (bulletPool.Count > 0)
        {
            return bulletPool.Dequeue();
        }

        //풀에 사용 가능한 총알이 없는 경우 새로 생성하여 반환
        GameObject newBullet = Instantiate(enemyBulletPrefab, transform);
        newBullet.SetActive(false);
        return newBullet;
    }

    //총알을 풀로 반환하는 함수
    public void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }
}
