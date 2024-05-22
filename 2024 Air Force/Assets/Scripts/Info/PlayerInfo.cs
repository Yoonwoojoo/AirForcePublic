using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InfoChangeType // 정보를 변경하는 방식 (3개 중 1개 선택)
{
    Add,      // 1. 덧셈 형식
    Multiple, // 2. 곱셈 형식
    Override, // 3. 덮어쓰기 형식
}

[Serializable]
public class PlayerInfo // 플레이어의 상태를 관리하고, 변경 방식을 정의
{
    public InfoChangeType infoChangeType;
    [Range(1, 100)] public int hp;
    [Range(1, 20)] public int speed;
    public AttackSO attackSO;
}
