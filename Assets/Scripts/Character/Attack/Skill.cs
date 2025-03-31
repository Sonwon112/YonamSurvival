using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Step Content")]
    [SerializeField] protected GameObject[] stepEffects;
    [SerializeField ]protected float[] stepDamage;
    [SerializeField] protected float[] stepTerm;

    protected int step = 1;

    private float currDamage = 0f;
    private float currTerm = 0f;

    private float addtionalDamage = 0f;
    private float addtionalTerm = 0f;

    /// <summary>
    /// 공격하는 함수
    /// </summary>
    public virtual void Attack() {}

    /// <summary>
    /// 버프 효과로 단계상승이 아닌 기본 데미지가 상승
    /// </summary>
    /// <param name="damage"></param>
    public void DamageUp(float damage)
    {
        addtionalDamage += damage;
    }

    /// <summary>
    /// 버프 효과로 단계상승이 아닌 기본 공격 속도가 상승
    /// </summary>
    public void SpeedUp(float term)
    {
        addtionalDamage -= term;
    }
}
