using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("�e�̐ݒ�")]
    public GameObject objPrefab;        // ���˒ePrefab
    public float delayTime = 0.5f;      // �e���ˊԊu
    public float fireSpeed = 4.0f;      // �e�̑��x

    private Transform gateTransform;    // ���ˌ�
    private float passedTimes = 0f;     // ���˃^�C�}�[
    private bool stopByWeb = false;     // Web�ɐG�ꂽ���~
    private int webTouchCount = 0;      // Web�ڐG��

    void Start()
    {
        gateTransform = transform.Find("gate"); // �q�I�u�W�F�N�g "gate" ���擾
        if (gateTransform == null)
            Debug.LogWarning("���ˌ� 'gate' ��������܂���I");
    }

    void Update()
    {
        // Web�ɐG��Ă��Ȃ���Δ���
        if (!stopByWeb)
        {
            passedTimes += Time.deltaTime;
            if (passedTimes > delayTime)
            {
                FireCannon();
                passedTimes = 0f;
            }
        }
    }

    void FireCannon()
    {
        if (objPrefab == null || gateTransform == null) return;

        GameObject obj = Instantiate(objPrefab, gateTransform.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse); // �������ɔ���
    }

    // Web�ɐG�ꂽ�u��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            webTouchCount++;
            stopByWeb = true;  // ���˒�~
        }
    }

    // Web���痣�ꂽ�u��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            webTouchCount--;
            if (webTouchCount <= 0)
            {
                webTouchCount = 0;
                stopByWeb = false; // ���ˍĊJ
            }
        }
    }
}