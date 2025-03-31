using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float skillCooldown = 1f;
    public float projectileSpeed = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(CastSkill), 0f, skillCooldown);
    }

    private void CastSkill()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(horizontal, vertical).normalized;

        // Ű �Է��� ���� ���� �߻����� ����
        if (inputDirection == Vector2.zero)
            return;

        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        projectile.GetComponent<Projectile>().SetDirection(inputDirection, projectileSpeed);
    }
}
