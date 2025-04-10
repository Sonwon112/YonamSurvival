using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] protected int skillId;
    [Header("Step Content")]
    [SerializeField] protected GameObject[] stepEffects;
    [SerializeField ]protected float[] stepDamage;
    [SerializeField] protected float[] stepTerm;

    [Header("UI")]
    [SerializeField] private Sprite skillProfile;
    [SerializeField] private string skillDescript;
    [SerializeField] protected int step = 0;

    protected bool startAttack = false;
    

    private float currDamage = 0f;
    private float currTerm = 0f;

    private float addtionalDamage = 0f;
    private float addtionalTerm = 0f;

    public Sprite getSkillProfile() { return skillProfile; }
    public string getSkillDescript() { return skillDescript; }

    /// <summary>
    /// 공격하는 함수
    /// </summary>
    public virtual void Attack() {}
    public virtual void StopAttack() { }

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

    /// <summary>
    /// 공격 시작
    /// </summary>
    public void StartAttack()
    {
        startAttack = true;
    }

    
}
