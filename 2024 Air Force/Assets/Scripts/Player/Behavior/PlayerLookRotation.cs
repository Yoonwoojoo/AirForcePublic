using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerLookRotation : MonoBehaviour
{
    private AirForceController airForceController;
    private Vector2 lookDirection = Vector2.zero; // 정적인 초기값

    private void Awake()
    {
        airForceController = GetComponent<AirForceController>();
    }

    private void Start()
    {
        airForceController.OnLookEvent += Look;
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return; // 게임이 멈춘 상태에서는 로직을 실행하지 않음
        }
        RotatePlayer(lookDirection);
    }

    private void Look(Vector2 direction)
    {
        lookDirection = direction; // InputSystem의 입력값을 RotatePlayer 메서드에 적용
    }

    private void RotatePlayer(Vector2 direction)
    {
        float z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // 각도를 구해서 방향을 읽기 위한 변수
        this.transform.rotation = Quaternion.Euler(0, 0, z);
    }
}
