using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Enemy와 충돌혔을 때 사라지면서 이펙트 나오게 하려면 Layer를 알아야 함
    [SerializeField] private LayerMask EnemyCollisionLayer;
    [SerializeField] private AudioClip attackSound;

    private AttackSO attackSO; 
    private float currentDuration;  // 발사체 존재하는 시간
    private Vector2 direction;      // 발사 방향
    private bool isReady;           // 발사 가능 여부
    private Rigidbody2D rb;         // 발사체 물리 법칙 적용
    private TrailRenderer trailRenderer; // 발사체 특수 효과
    public bool fxOnDestory = true; // 발사체 파괴시 이펙트 생성 여부

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;          // 일단 발사체가 존재해야할 시간을 증가 

        if (currentDuration > attackSO.duration)    //  발사체가 일정 시간을 초과하면
        {
            DestroyProjectile(transform.position, false); // 발사체를 파괴하라
        }

        rb.velocity = direction * attackSO.speed;
    }
    private void Start()
    {
        // 오디오 소스 컴포넌트를 동적으로 추가합니다.
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        // 발사 소리를 재생합니다.
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
            audioSource.volume = 0.03f; // 볼륨을 조절 필요에 따라 변경가능!
            audioSource.Play();
        }

        // 발사 소리 재생 후 오디오 소스를 파괴합니다.
        Destroy(audioSource, attackSound.length);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsLayerMatched(EnemyCollisionLayer.value, collision.gameObject.layer))                  // 충돌한 대상이 Enemy 레이어 라면
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f; // 충돌한 지점으로부터 약간 앞 쪽에서
            DestroyProjectile(destroyPosition, fxOnDestory);                                        // 이펙트와 함께 발사체를 파괴
        }
    }

    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer)); // 레이어 마스크와 객체의 레이어를 비교하여 일치하는지 확인
    }

    public void InitializeAttack(Vector2 direction, AttackSO attackData)
    {
        // 발사체의 초기 설정
        this.attackSO = attackData;
        this.direction = direction;

        trailRenderer.Clear();
        currentDuration = 0;
        transform.right = this.direction;

        isReady = true;
    }

    // 발사체 파괴 메소드
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // 발사체를 파괴하고, 필요에 따라 파괴 이펙트를 생성
        }
        Destroy(this.gameObject); // 발사체 파괴
    }
}
