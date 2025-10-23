using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifetime = 0.5f;  // �������\������Ă��鎞�ԁi�b�j
    public float radius = 2.0f;    // �������͂��͈́i���a�j
    private SpriteRenderer sr;     // �����摜�iSprite�j�̕`��R���|�[�l���g��ێ�����ϐ�

    void Start()
    {
        // �����摜�iSprite Renderer�j���擾
        sr = GetComponent<SpriteRenderer>();

        // ===============================
        // �����͈͓̔��ɂ���G��T���Ĕj�󂷂�
        // ===============================
        // �����n�_�𒆐S�ɔ��a radius �̉~�����A
        // ���͈̔͂ɂ��邷�ׂĂ�Collider2D���擾����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D hit in hits)
        {
            // �����uEnemy�v�^�O�̂����I�u�W�F�N�g���͈͓��ɂ���Δj��
            if (hit.CompareTag("Web_stop"))
            {
                Destroy(hit.gameObject);
                Debug.Log("�����œG��j��I: " + hit.name);
            }
        }

        // ===============================
        // �����ڂ̔����G�t�F�N�g�i�g��ƃt�F�[�h�A�E�g�j���J�n
        // ===============================
        StartCoroutine(ExplosionEffect());
    }

    // ========================================================
    // �����̌����ڂ����o����R���[�`��
    // �g�債�Ȃ��珙�X�ɓ����ɂ��āA�Ō�ɏ���
    // ========================================================
    IEnumerator ExplosionEffect()
    {
        // ������Ԃ̑傫���ƐF���L�^���Ă���
        Vector3 startScale = transform.localScale;
        Color startColor = sr.color;

        float elapsed = 0;  // �o�ߎ���
        while (elapsed < lifetime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / lifetime; // 0 �� 1 �ɐi�ފ���

            //���Ԃ��o�ɂ�Ĕ�����1.5�{�Ɋg��
            transform.localScale = startScale * (1 + t * 1.5f);

            //���X�ɓ����i�A���t�@�l�����炷�j
            sr.color = new Color(startColor.r, startColor.g, startColor.b, 1 - t);

            yield return null; // ���̃t���[���܂őҋ@
        }

        //�Ō�ɔ����I�u�W�F�N�g���폜
        Destroy(gameObject);
    }

    // ========================================================
    //  Scene�r���[�Ŕ����͈͂��������邽�߂̕⏕��
    // �i�Q�[�����ɂ͕\������Ȃ��j
    // ========================================================
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
