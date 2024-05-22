using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletRemove : MonoBehaviour
{
    public static BulletRemove instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        //카메라 밖으로 나가면 자동으로 풀에 반환
        if (!IsInCameraView())
        {
            ReturnToPool();
        }
    }

    //총알이 카메라 시야 내에 있는지 확인하는 함수
    private bool IsInCameraView()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        return viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1;
    }

    //총알을 풀에 반환하는 함수
    public void ReturnToPool()
    {
        //BulletGenerator의 인스턴스를 통해 총알을 풀에 반환
        BulletGenerator.instance.ReturnBulletToPool(gameObject);
    }

    //플레이어와 충돌시 처리하는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어 체력 감소 로직 추가 필요!

            //총알을 풀에 반환
            ReturnToPool();
        }
    }
}
