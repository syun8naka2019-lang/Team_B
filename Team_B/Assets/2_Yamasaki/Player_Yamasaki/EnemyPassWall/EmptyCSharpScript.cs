using UnityEngine;

public class EnemyPassWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy�^�O�����I�u�W�F�N�g�͒ʉ�
        if (other.CompareTag("Enemy"))
        {
            // �������Ȃ��i�ʂ蔲��OK�j
        }
        else
        {
            // ����ȊO�͎~�߂����ꍇ �� ���������I�u�W�F�N�g���~�߂� or �o�E���h
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}