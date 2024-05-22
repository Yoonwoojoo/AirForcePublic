using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRemove : MonoBehaviour
{
    private PlayerInfoHandler playerInfoHandler;
    private StageManager stageManager;
    public Image playerHpBar;
    private float initialHp;  // 초기 체력을 저장할 변수
    public GameObject resultUI;
    private void Awake()
    {
        playerInfoHandler = GetComponent<PlayerInfoHandler>();
        stageManager = FindObjectOfType<StageManager>();
    }

    private void Start()
    {
        initialHp = playerInfoHandler.UpdatedInfo.hp;  // 초기 체력을 저장
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Enemy 클래스의 인스턴스 가져오기
            Enemy enemyScript = FindObjectOfType<Enemy>();

            // 현재 스테이지 타입 가져오기
            StageType currentStageType = enemyScript.currentStageType;

            // 해당 스테이지의 적 정보 가져오기
            EnemyInfo enemyInfo = StageManager.instance.GetStageStats(currentStageType);

            // 플레이어 체력 감소
            playerInfoHandler.UpdatedInfo.hp -= enemyInfo.attackPower;

            if (playerInfoHandler.UpdatedInfo.hp <= 0)
            {
                playerInfoHandler.UpdatedInfo.hp = 0;
                this.gameObject.SetActive(false);
                resultUI.SetActive(true);
                Time.timeScale = 0; //게임 시간 멈추기
            }
            else
            {
                // 남은 체력의 비율을 계산하여 fillAmount에 설정
                playerHpBar.fillAmount = playerInfoHandler.UpdatedInfo.hp / initialHp;
            }
        }
    }
}
