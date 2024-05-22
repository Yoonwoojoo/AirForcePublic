using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : AirForceController
{
    protected override void Awake()
    {
        base.Awake(); // 상속 관계이므로 코드의 안정성을 위해 넣음. 부모의 Awake도 빼먹지 말고 실행
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 mousePosition = value.Get<Vector2>(); // 마우스의 스크린 좌표
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition); // 월드 좌표
        Vector2 direction = (worldPosition - (Vector2)transform.position).normalized; // 캐릭터가 마우스를 바라보는 좌표 = 월드 좌표 - 오브젝트의 좌표
        CallLookEvent(direction);
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed; // 버튼이 눌렸는지 판단
    }
}
