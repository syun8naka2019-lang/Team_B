/*using System.Collections;
using UnityEngine;

/// <summary>
/// ���ʂ̓G�R���g���[���X�N���v�g
/// - �������ɗ���
/// - �e�iWeb�j������������~�܂�
/// - L�L�[�Ŕ����\
/// </summary>
public class EnemyBaseController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float speed = 2f;                  // �������x

    private bool isStopped = false;           // ��~��Ԃ�
    private Rigidbody2D rb;                   // Rigidbody2D�R���|�[�l���g

    [Header("�����ݒ�")]
    public GameObject explosionPrefab;        // ����Prefab
    public float explosionRadius = 2f;        // �����͈́i���a�j

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // ��~���Ă��Ȃ���Ή������Ɉړ�
        if (!isStopped)
        {
            rb.velocity = new Vector2(0, -speed);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// �e�������������ɌĂ�
    /// </summary>
    public void Stop()
    {
        if (!isStopped)
        {
            isStopped = true;
            rb.velocity = Vector2.zero;
        }
    }

    void Update()
    {
        // ��~����L�L�[�Ŕ���
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// ��������
    /// �͈͓���Web��Enemy��j��
    /// ���g���j��
    /// </summary>
    private void Explode()
    {
        // �����ڗp�̔���Prefab����
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // �����͈͓��̃I�u�W�F�N�g���擾
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

        // ���g���j��
        Destroy(gameObject);
    }

    // Scene�r���[�Ŕ����͈͂�����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}*/

using System.Collections;
using UnityEngine;

/// <summary>
/// ���ʂ̓G�R���g���[���X�N���v�g
/// - �ړ�������Inspector�Ŏ��R�ɐݒ�\
/// - �e�iWeb�j������������~�܂�
/// - L�L�[�Ŕ����\
/// </summary>
public class EnemyBaseController : MonoBehaviour
{
    [Header("�ړ��ݒ�")]
    public float speed = 2f;                      // �ړ����x
    public Vector2 moveDirection = Vector2.down;  // Inspector�Őݒ�ł���ړ�����

    private bool isStopped = false;               // �G���~�܂��Ă��邩
    private Rigidbody2D rb;                       // Rigidbody2D�R���|�[�l���g

    [Header("�����ݒ�")]
    public GameObject explosionPrefab;            // ����Prefab
    public float explosionRadius = 2f;            // �����͈�

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // ��~���Ă��Ȃ���Ύw������Ɉړ�
        if (!isStopped)
        {
            rb.linearVelocity = moveDirection.normalized * speed;
        }
        else
        {
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
        // ��~����L�L�[�Ŕ���
        if (isStopped && Input.GetKeyDown(KeyCode.L))
        {
            Explode();
        }
    }

    /// <summary>
    /// ��������
    /// �͈͓���Web��Enemy��j��
    /// ���g���j��
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
            if (hit.CompareTag("Web") || hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);
                Debug.Log("�����Ŕj��: " + hit.name);
            }
        }

        // ���g���j��
        Destroy(gameObject);
    }

    // Scene�r���[�Ŕ����͈͂�����
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}