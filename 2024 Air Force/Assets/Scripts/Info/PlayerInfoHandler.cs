using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoHandler : MonoBehaviour
{
    [SerializeField] private PlayerInfo baseInfo; // 기본 정보
    public PlayerInfo UpdatedInfo { get; private set; } // 업데이트된 정보
    public List<PlayerInfo> infoEditor = new List<PlayerInfo>();

    private void Awake()
    {
        UpdatePlayerInfo();
    }

    private void UpdatePlayerInfo()
    {
        AttackSO attackSO = null;
        if (baseInfo.attackSO != null)
        {
            attackSO = Instantiate(baseInfo.attackSO);
        }

        UpdatedInfo = new PlayerInfo { attackSO = attackSO };

        // 지금은 기본 능력치만 적용되고 있지만, 향후 아이템을 먹었을 때 능력치를 강화 시킬 여지가 있음
        UpdatedInfo.infoChangeType = baseInfo.infoChangeType;
        UpdatedInfo.hp = baseInfo.hp;
        UpdatedInfo.speed = baseInfo.speed;
    }
}
