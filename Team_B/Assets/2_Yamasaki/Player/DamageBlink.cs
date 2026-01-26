using System.Collections;
using UnityEngine;

// ダメージを受けたときにスプライトの色を点滅させるクラス
public class DamageBlink : MonoBehaviour
{
    // 点滅する合計時間（秒）
    [SerializeField] private float blinkDuration = 1.0f;

    // 点滅の間隔（秒）
    [SerializeField] private float blinkInterval = 0.1f;

    // ダメージ時に変える色
    [SerializeField] private Color damageColor = Color.red;

    // Inspectorで設定する対象のSpriteRenderer
    [SerializeField] private SpriteRenderer targetSprite;

    // 元の色を保存する変数
    private Color originalColor;

    // すでに点滅中かどうか（多重起動防止）
    private bool isBlinking = false;

    void Start()
    {
        if (targetSprite != null)
        {
            // 元の色を保存
            originalColor = targetSprite.color;
        }
        else
        {
            Debug.LogWarning("DamageBlink: targetSprite が設定されていません");
        }
    }

    // ダメージを受けたときに外部から呼ぶ
    public void TakeDamage()
    {
        if (!isBlinking && targetSprite != null)
        {
            StartCoroutine(Blink());
        }
    }

    // 色点滅処理
    IEnumerator Blink()
    {
        isBlinking = true;
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            // ダメージ色に変更
            targetSprite.color = damageColor;
            yield return new WaitForSeconds(blinkInterval);

            // 元の色に戻す
            targetSprite.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);

            elapsed += blinkInterval * 2;
        }

        // 念のため元の色に戻す
        targetSprite.color = originalColor;
        isBlinking = false;
    }
}
