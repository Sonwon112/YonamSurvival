using UnityEngine;

public interface EnemyInterface
{
    float MoveSpeed { get; set; }
    void Move();
    void Attack(int atDamage);
}
