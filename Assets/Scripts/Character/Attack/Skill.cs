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
    /// �����ϴ� �Լ�
    /// </summary>
    public virtual void Attack() {}
    public virtual void StopAttack() { }

    /// <summary>
    /// ���� ȿ���� �ܰ����� �ƴ� �⺻ �������� ���
    /// </summary>
    /// <param name="damage"></param>
    public void DamageUp(float damage)
    {
        addtionalDamage += damage;
    }

    /// <summary>
    /// ���� ȿ���� �ܰ����� �ƴ� �⺻ ���� �ӵ��� ���
    /// </summary>
    public void SpeedUp(float term)
    {
        addtionalDamage -= term;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void StartAttack()
    {
        startAttack = true;
    }

    
}
