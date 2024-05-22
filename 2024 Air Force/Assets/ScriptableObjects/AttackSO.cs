using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "AirForceController/Attacks", order = 0)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")] // 일단 사용해 볼 만한 옵션들만 넣음
    public float size;
    public float delay;
    public float power;
    public float speed;
    public float duration; 
    public float spread;
    public float multipleProjectilesAngle;
    public int numberofProjectilesPerShot;
    public string bulletNameTag;
    public LayerMask target;
}
