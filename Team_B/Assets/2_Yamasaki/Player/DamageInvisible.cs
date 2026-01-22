using System.Collections;
using UnityEngine;

// ダメージを受けたら一定時間姿を消すクラス
public class DamageInvisible : MonoBehaviour
{
    // 消えている時間（秒）
    [SerializeField] private float invisibleTime = 1.0f;

    // Inspectorで設定する SpriteRenderer
    [SerializeField] private SpriteRenderer targetSprite;

    // 連続で呼ばれないようにするフラグ
    private bool isInvisible = false;

    // ダメージを受けたときに呼ぶ
    public void TakeDamage()
    {
        if (!isInvisible && targetSprite != null)
        {
            StartCoroutine(Invisible());
        }
    }

    // 一定時間姿を消すコルーチン
    IEnumerator Invisible()
    {
        isInvisible = true;

        // スプライトを非表示
        targetSprite.enabled = false;

        yield return new WaitForSeconds(invisibleTime);

        // スプライトを表示
        targetSprite.enabled = true;

        isInvisible = false;
    }
}
