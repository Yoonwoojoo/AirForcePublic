using UnityEngine;
using UnityEngine.UI;

enum ItemType
{
    HPRegen,        // 체력 회복
    AtkBoost,       // 데미지 강화
    MultiShot,      // 총알 증가
    SpeedBoost      // 속도 증가
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    public GameObject Item;

    public Image HPBarImage;

    public Image multishot;
    public Image atk;
    public Image speed;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        InvokeRepeating("GenerateItem", 0f, 10f);
        // TODO: 난이도 별 아이템 생성시간 변경   
    }

    public void GenerateItem()
    {
        Instantiate(Item, new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f)), Quaternion.identity);
    }
}
