/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        //����������Prefab�f�[�^
    public float delayTime = 0.5f;      //�x������
    public float fireSpeed = 4.0f;      //���ˑ��x

    Transform gateTransform;
    float passedTimes = 0;              //�o�ߎ���
    bool stopByWeb = false;

    void Start()
    {
        //���ˌ��I�u�W�F�N�g��Transform���擾
        gateTransform = transform.Find("gate");
    }       
    private void Update()
    {
        passedTimes += Time.deltaTime;
        if (passedTimes > delayTime)
        {
            passedTimes = 0;

            Vector2 pos = new Vector2(gateTransform.position.x, gateTransform.position.y);
            //�e����
            GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
            //�C�e�������Ă�������ɔ���
            Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
            Vector2 v = new Vector2(0, -1) * fireSpeed;
            rbody.AddForce(v, ForceMode2D.Impulse);
        }
    }
    

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject objPrefab;        // ���˂���e��Prefab
    public float delayTime = 0.5f;      // �e�𔭎˂���Ԋu�i�b�j
    public float fireSpeed = 4.0f;      // �e�̔��ˑ��x

    Transform gateTransform;            // ���ˌ���Transform
    float passedTimes = 0;              // �o�ߎ��Ԃ̌v���p
    bool stopByWeb = false;             // Web�ɐG��Ă���Ԃ�true

    void Start()
    {
        // ���ˌ��I�u�W�F�N�g��Transform���擾
        gateTransform = transform.Find("gate");
    }

    void Update()
    {
        // Web�ɐG��Ă��Ȃ��ꍇ�̂ݒe�𔭎�
        if (!stopByWeb)
        {
            // �o�ߎ��Ԃ����Z
            passedTimes += Time.deltaTime;

            // delayTime�𒴂�����e�𔭎�
            if (passedTimes > delayTime)
            {
                FireCannon();       // �e�̔���
                passedTimes = 0;    // �o�ߎ��Ԃ����Z�b�g
            }
        }
    }

    // �e�𐶐����Ĕ��˂��鏈��
    void FireCannon()
    {
        Vector2 pos = gateTransform.position;                        // ���ˌ��̈ʒu
        GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity); // �e�𐶐�
        Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();         // Rigidbody2D���擾
        rbody.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse);    // �������ɗ͂������Ĕ���
    }

    // Web�ɐG�ꂽ�u�ԂɌĂ΂�鏈��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            stopByWeb = true;                 // ���˂��~
            Debug.Log("Web�ɐG�ꂽ �� ���˒�~");
        }
    }

    // Web���痣�ꂽ�u�ԂɌĂ΂�鏈��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Web"))
        {
            stopByWeb = false;                // ���˂��ĊJ
            Debug.Log("Web���痣�ꂽ �� ���ˍĊJ");
        }
    }
}