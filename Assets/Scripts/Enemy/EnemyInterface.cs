using UnityEngine;

public interface EnemyInterface
{
    int HP { get; set; }
    float MoveSpeed { get; set; }
    void Move();
    void TakeDamage(int damage);
}
