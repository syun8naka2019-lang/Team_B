using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RPG_Controller : MonoBehaviour
{
    public float speed = 7.0f;                // �ړ����x
    public Vector2 direction = Vector2.left;  // ���������i���j
    float cout = 0;                           // �o�ߎ��ԃJ�E���g

    private float originalSpeed;              // ���̑��x��ۑ�
    public bool isOnWeb = false;              // Web�ɐG��Ă��邩�ǂ���
    private List<GameObject> webObjects = new List<GameObject>(); // �G��Ă���SWeb

    void Start()
    {
        originalSpeed = speed; // �ŏ��̑��x��ۑ�
    }

    void Update()
    {
        // Web�ɕ߂܂��Ă��Ȃ��������ړ�
        if (!isOnWeb)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }

        // �����ύX�����i�e�X�g�p�j
        cout += Time.deltaTime;
        if (cout > 1)
        {
            direction = new Vector2(1, -1);
        }

        // L�L�[�œG��Web������
        if (isOnWeb && Input.GetKey(KeyCode.L))
        {
            // ���X�g�̃R�s�[������āA��������[�v����
            List<GameObject> websToDestroy = new List<GameObject>(webObjects);

            foreach (GameObject web in websToDestroy)
            {
                if (web != null)
                    Destroy(web);
            }

            webObjects.Clear();  // ���ׂč폜�������ƂɃ��X�g����ɂ���

            Destroy(gameObject);
            Debug.Log("L�L�[���� �� �G�ƑSWeb������");
        }
    }

    // Web�ɐG�ꂽ�Ƃ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            isOnWeb = true;
            speed = 0.0f; // �������~�߂�
            if (!webObjects.Contains(collision.gameObject))
            {
                webObjects.Add(collision.gameObject);
            }

            Debug.Log("Web�ɐڐG �� �G��~");
        }
    }

    // Web���痣�ꂽ�Ƃ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            webObjects.Remove(collision.gameObject);
            if (webObjects.Count == 0)
            {
                isOnWeb = false;
                speed = originalSpeed; // �������ĊJ
                Debug.Log("Web���痣�ꂽ �� �Ďn��");
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
