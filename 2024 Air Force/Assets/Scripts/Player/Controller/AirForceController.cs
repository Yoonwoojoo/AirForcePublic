using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirForceController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action<AttackSO> OnAttackEvent; // 마우스 왼쪽 버튼 누르면 공격 정보(AttackSO) 를 들고 옴

    private float timeSinceLastAttack = float.MaxValue; // 초기값은 float 타입이 가질 수 있는 가장 큰 값 (제일 무난함)
    protected bool IsAttacking { get; set; }

    protected PlayerInfoHandler stats { get; private set; }



    protected virtual void Awake()
    {
        stats = GetComponent<PlayerInfoHandler>();
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return; // 게임이 멈춘 상태에서는 로직을 실행하지 않음
        }
        AttackDelay();
    }

    private void AttackDelay() // 공격 쿨타임 설정 + 공격 실행할 메서드를 호출
    {
        if(timeSinceLastAttack < stats.UpdatedInfo.attackSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        if(IsAttacking && timeSinceLastAttack >= stats.UpdatedInfo.attackSO.delay)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(stats.UpdatedInfo.attackSO);
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
}
