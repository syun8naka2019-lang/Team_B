using System.Collections;
using UnityEngine;

public class Enemy_Music_Controller : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float speed = 2f;                  // �G�̗������x

    private bool isStopped = false;           // �G���~�܂��Ă��邩�ǂ���
    private Rigidbody2D rb;                   // Rigidbody2D�R���|�[�l���g

    [Header("�����ݒ�")]
    public GameObject explosionPrefab;        // ����Prefab
    public float explosionRadius = 2f;        // �����͈́i���a�j

    void Awake()
    {
        // Rigidbody2D�擾
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isStopped)
        {
            // �������Ɉړ�
            rb.linearVelocity = new Vector2(0, -speed);
        }
        else
        {
            // ��~���͑��x0
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// �e�����������Ƃ��ɌĂ�
    /// �G���~������
    /// </summary>
    public void Stop()
    {
        if (!isStopped)
        {
            isStopped = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Update()
    {
        // ��~����L�L�[�������ꂽ�甚��
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// ��������
    /// �͈͓��̓G��e��j�󂵁A���g���j�󂷂�
    /// </summary>
    private void Explode()
    {
        // �����ڗp�̔���Prefab����
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // �����͈͓���Collider2D���擾
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            // Web�܂���Enemy�^�O�̃I�u�W�F�N�g��j��
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("�����Ŕj��: " + hit.name);
            }
        }

        // �������j��
        Destroy(gameObject);
    }

    // Scene�r���[�Ŕ����͈͂�����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}