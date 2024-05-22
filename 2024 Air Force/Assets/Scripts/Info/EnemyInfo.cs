using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfo", menuName = "ScriptableObjects/EnemyInfo", order = 1)]
public class EnemyInfo : ScriptableObject
{
    public Sprite enemySprite;
    public int attackPower;
    public int hp;
    public int enemyCount;
    public Vector2 spriteSize; //스프라이트 크기
    public Sprite bulletSprite; //일반 총알 스프라이트
    public Sprite beemSprite; //광선 총알 스프라이트
}
