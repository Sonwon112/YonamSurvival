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
    /// �����ϴ� �Լ�
    /// </summary>
    public virtual void Attack() {}

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
}
