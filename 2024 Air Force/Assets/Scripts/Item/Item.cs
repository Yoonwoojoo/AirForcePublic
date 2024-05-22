using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Item : MonoBehaviour
{
    ItemType itemType;
    
    PlayerInfoHandler playerInfo;

    Animator animator;

    public GameObject item;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        int rndNum = Random.Range(0, 4);
        // 아이템 타입 결정
        GenerateItemType(rndNum);
        DestroyItemDelayed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 태그 비교해서 false면 실행X
        if (!collision.gameObject.CompareTag("Player")) return;
        // attackSO = collision.gameObject.GetComponent<PlayerInfoHandler>().UpdatedInfo.attackSO;
        playerInfo = collision.gameObject.GetComponent<PlayerInfoHandler>();
        if (itemType == ItemType.HPRegen)
        {
            // 체력 회복
            RegenHP();
        }
        if (itemType == ItemType.AtkBoost)
        {
            // 데미지 증가
            Atkboost();
            SetTransparency(ItemManager.Instance.atk, 1f);
        }
        if (itemType == ItemType.MultiShot)
        {
            // 총알 증가
            MultiShot();
            SetTransparency(ItemManager.Instance.multishot, 1f);
        }
        else if(itemType == ItemType.SpeedBoost)
        {
            // player 이동 속도 증가
            SpeedBoost();
            SetTransparency(ItemManager.Instance.speed, 1f);
        }
        Destroy(gameObject);
    }

    // Itemtype 랜덤 생성
    public void GenerateItemType(int typeNum)
    {
        if (typeNum == (int)ItemType.HPRegen)
        {
            itemType = ItemType.HPRegen;


            animator.SetTrigger("HPRegenTrigger");
        }
        else if (typeNum == (int)ItemType.AtkBoost)
        {
            itemType = ItemType.AtkBoost;
            animator.SetTrigger("AtkBoostTrigger");
        }
        else if (typeNum == (int)ItemType.MultiShot)
        {
            itemType = ItemType.MultiShot;
            animator.SetTrigger("MultiShotTrigger");
        }
        else
        {
            itemType = ItemType.SpeedBoost;
            animator.SetTrigger("SpeedBoostTrigger");
        }
    }

    // 아이템 생성 5초 후 파괴
    public void DestroyItemDelayed()
    {
        Destroy(gameObject, 5f);
    }

    public void RegenHP()
    {
        // TODO: Player HP 증가

        // HP Bar 회복
        ItemManager.Instance.HPBarImage.fillAmount += 0.5f;
        playerInfo.UpdatedInfo.hp += 40;
    }

    public void Atkboost()
    {
        playerInfo.UpdatedInfo.attackSO.power += 1;
    }

    public void MultiShot()
    {
        if(playerInfo.UpdatedInfo.attackSO.numberofProjectilesPerShot < 6)
        playerInfo.UpdatedInfo.attackSO.numberofProjectilesPerShot += 2;
    }

    public void SpeedBoost()
    {
        playerInfo.UpdatedInfo.speed += 3;
    }

    public void SetTransparency(Image image, float alpha)
    {
        // 이미지의 현재 색상을 가져옵니다.
        Color color = image.color;
        // 알파 값을 새로운 투명도로 설정합니다.
        color.a = Mathf.Clamp01(alpha);  // alpha 값은 0에서 1 사이여야 합니다.
                                         // 변경된 색상을 다시 이미지에 적용합니다.
        image.color = color;
    }
}
