using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Music_Controller : MonoBehaviour
{
    public float speed = 7.0f;                // �ړ����x
    public Vector2 direction = Vector2.left;  // ���������i���j
    private float cout = 0;                    // �o�ߎ��ԃJ�E���g�p

    private float originalSpeed;              // ���̑��x��ۑ�
    public bool isOnWeb = false;              // Web�ɐG��Ă��邩�ǂ���
    private List<GameObject> webObjects = new List<GameObject>(); // �G��Ă���SWeb
    [Header("�����G�t�F�N�g��Prefab��o�^����")]
    public GameObject explosionPrefab;        // �����G�t�F�N�gPrefab

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

        // �e�X�g�p�̕����ύX�i�C�Ӂj
        cout += Time.deltaTime;
        if (cout > 1)
        {
            direction = new Vector2(-1, -1);
        }

        // L�L�[�œG��Web�𓯎��ɏ���
        if (isOnWeb && Input.GetKey(KeyCode.L))
        {
            // �߂܂��Ă���Web�����ׂč폜
            List<GameObject> websToDestroy = new List<GameObject>(webObjects);
            foreach (GameObject web in websToDestroy)
            {
                if (web != null)
                    Destroy(web);
            }
            webObjects.Clear();

            // �����G�t�F�N�g�𐶐�
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Debug.Log("���������I�iMusic�j");
            }

            // �G���g���폜
            Destroy(gameObject);
            Debug.Log("L�L�[���� �� Music�G�ƑSWeb������");
        }
    }

    // Web�ɐG�ꂽ�Ƃ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Tag�� "Web_stop" ��Web�ɓ��������ꍇ
        if (collision.gameObject.CompareTag("Web_stop"))
        {
            isOnWeb = true;       // Web�ߊl���
            speed = 0.0f;         // �ړ����~�߂�

            // webObjects�ɒǉ��i�d���h�~�j
            if (!webObjects.Contains(collision.gameObject))
            {
                webObjects.Add(collision.gameObject);
            }

            Debug.Log("Web�ɐڐG �� Music�G��~");
        }
    }

    // Web���痣�ꂽ�Ƃ�
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web_stop"))
        {
            webObjects.Remove(collision.gameObject);

            // ���ׂĂ�Web���痣�ꂽ��Ďn��
            if (webObjects.Count == 0)
            {
                isOnWeb = false;
                speed = originalSpeed;
                Debug.Log("Web���痣�ꂽ �� Music�G�Ďn��");
            }
        }
    }

    private void OnBecameInvisible()
    {
        // ��ʊO�ɏo����폜
        Destroy(gameObject);
    }
}