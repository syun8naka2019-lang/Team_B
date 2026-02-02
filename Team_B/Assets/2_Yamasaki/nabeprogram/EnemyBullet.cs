using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;   // �e�̑���
    private Vector2 direction;

    // ���ˎ��ɕ������Z�b�g����
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // Rigidbody2D �̐ݒ�`�F�b�N
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = direction * speed;
    }
}
