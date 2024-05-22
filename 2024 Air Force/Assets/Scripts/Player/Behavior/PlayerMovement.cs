using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private AirForceController airForceController;
    private Rigidbody2D rb;
    private Vector2 movementDirection = Vector2.zero; // 정적인 초기값
    private PlayerInfoHandler playerInfoHandler;

    private void Awake()
    {
        airForceController = GetComponent<AirForceController>();
        rb = GetComponent<Rigidbody2D>();
        playerInfoHandler = GetComponent<PlayerInfoHandler>();
    }

    private void Start()
    {
        airForceController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        ApplyMove(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction; // InputSystem 의 입력값을 적용
    }

    private void ApplyMove(Vector2 direction)
    {
        direction *= playerInfoHandler.UpdatedInfo.speed; // 벡터에 곱셈을 해줌으로서 속도 생성
        rb.velocity = direction;
    }
}
