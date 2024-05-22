using UnityEngine;

public class EnemyInfoHandler: MonoBehaviour
{
    public int attackPower;
    public int hp;

    public void SetAttributes(int attackPower, int hp)
    {
        this.attackPower = attackPower;
        this.hp = hp;
    }
}