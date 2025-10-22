using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController_P : MonoBehaviour
{
    public GameObject objPrefab;        //����������Prefab�f�[�^
    public float delayTime = 1f;      //�x������
    public float fireSpeed = 4.0f;      //���ˑ��x

    Transform gateTransform;
    float passedTimes = 0;              //�o�ߎ���

    void Start()
    {
        //���ˌ��I�u�W�F�N�g��Transform���擾
        gateTransform = transform.Find("playergate");
    }

    private void Update()
    {
        // Prefab�������Ă��Ȃ����m�F
        if (objPrefab == null)
        {
            Debug.LogError("objPrefab ���j�󂳂�Ă��܂��IInspector��Prefab���Đݒ肵�Ă��������B");
            return;
        }

        passedTimes += Time.deltaTime;
        if (passedTimes >= 1f)
        {
            if (Input.GetKey(KeyCode.K))
            {                
                Vector2 pos = new Vector2(gateTransform.position.x, gateTransform.position.y);
                //�e����
                GameObject obj = Instantiate(objPrefab, pos, Quaternion.identity);
                //�C�e�������Ă�������ɔ���
                Rigidbody2D rbody = obj.GetComponent<Rigidbody2D>();
                Vector2 v = new Vector2(0, 1) * fireSpeed;
                rbody.AddForce(v, ForceMode2D.Impulse);
                passedTimes = 0;
            }
        }
       
    }
}