/*using System.Collections;
using UnityEngine;

// ダメージ時に白フラッシュさせるクラス
public class DamageWhiteFlash : MonoBehaviour
{
    // フラッシュする合計時間
    [SerializeField] private float flashDuration = 0.2f;

    // フラッシュ間隔
    [SerializeField] private float flashInterval = 0.05f;

    // 対象のSpriteRenderer（Inspectorで設定）
    [SerializeField] private SpriteRenderer targetSprite;

    // 白フラッシュ用マテリアル（Inspectorで設定）
    [SerializeField] private Material whiteFlashMaterial;

    // 元のマテリアル保存用
    private Material originalMaterial;

    // 多重起動防止
    private bool isFlashing = false;

    void Start()
    {
        if (targetSprite != null)
        {
            originalMaterial = targetSprite.material;
        }
        else
        {
            Debug.LogWarning("DamageWhiteFlash: targetSprite が設定されていません");
        }
    }

    // ダメージ時に呼ぶ
    public void TakeDamage()
    {
        if (!isFlashing && targetSprite != null && whiteFlashMaterial != null)
        {
            StartCoroutine(WhiteFlash());
        }
    }

    IEnumerator WhiteFlash()
    {
        isFlashing = true;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            // 白フラッシュ
            targetSprite.material = whiteFlashMaterial;
            yield return new WaitForSeconds(flashInterval);

            // 元に戻す
            targetSprite.material = originalMaterial;
            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;
        }

        // 念のため元に戻す
        targetSprite.material = originalMaterial;
        isFlashing = false;
    }
}*/

using System.Collections;
using UnityEngine;

// 対象に当たったら白フラッシュさせるクラス
public class DamageWhiteFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private float flashInterval = 0.05f;

    [Header("Target Sprite")]
    [SerializeField] private SpriteRenderer targetSprite;

    [Header("Material")]
    [SerializeField] private Material whiteFlashMaterial;

    [Header("Hit Settings")]
    [SerializeField] private string hitTargetTag = "Enemy"; // ← Inspectorで変更可

    private Material originalMaterial;
    private bool isFlashing = false;

    void Awake()
    {
        if (targetSprite != null)
        {
            originalMaterial = targetSprite.sharedMaterial;
        }
        else
        {
            Debug.LogWarning("DamageWhiteFlash: targetSprite が設定されていません");
        }
    }

    // 何かに当たったら呼ばれる（Trigger）
    void OnTriggerEnter2D(Collider2D other)
    {
        // Inspectorで指定したTagに当たったら
        if (other.CompareTag(hitTargetTag))
        {
            TryFlash();
        }
    }

    //通常の衝突を使いたい場合はこちら
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(hitTargetTag))
        {
            TryFlash();
        }
    }

    void TryFlash()
    {
        if (!isFlashing && targetSprite != null && whiteFlashMaterial != null)
        {
            StartCoroutine(WhiteFlash());
        }
    }

    IEnumerator WhiteFlash()
    {
        isFlashing = true;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            targetSprite.material = new Material(whiteFlashMaterial);
            yield return new WaitForSeconds(flashInterval);

            targetSprite.material = originalMaterial;
            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;
        }

        targetSprite.material = originalMaterial;
        isFlashing = false;
    }
}
