using System.Collections;
<<<<<<< HEAD

using UnityEngine;

// 対象に当たったらスプライトを白フラッシュさせるクラス

=======
using UnityEngine;

// 対象に当たったらスプライトを白フラッシュさせるクラス
>>>>>>> d70c7e42da5ce9cc6fe5ffd68e67db23ed50507b
public class DamageWhiteFlash : MonoBehaviour

{
<<<<<<< HEAD

    // ===== フラッシュ演出の設定 =====

    [Header("Flash Settings")]

    [SerializeField] private float flashDuration = 0.2f;

    [SerializeField] private float flashInterval = 0.05f;

    // ===== 白フラッシュさせる画像 =====

    [Header("Target Sprite")]

    [SerializeField] private SpriteRenderer targetSprite;

    // ===== マテリアル関連 =====

    [Header("Material")]

    [SerializeField] private Material whiteFlashMaterial;

    // 元のマテリアル

=======
    // ===== フラッシュ演出の設定 =====
    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private float flashInterval = 0.05f;

    // ===== 白フラッシュさせる画像 =====
    [Header("Target Sprite")]
    [SerializeField] private SpriteRenderer targetSprite;

    // ===== マテリアル関連 =====
    [Header("Material")]
    [SerializeField] private Material whiteFlashMaterial;

    // 元のマテリアル
>>>>>>> d70c7e42da5ce9cc6fe5ffd68e67db23ed50507b
    private Material originalMaterial;

    // 多重起動防止

    private bool isFlashing = false;

    // ===== 当たり判定の設定 =====
<<<<<<< HEAD

    [Header("Hit Settings")]

    // 白フラッシュを発生させる相手のタグ（複数設定可）

    [SerializeField] private string[] hitTargetTags;

    void Awake()

    {

        if (targetSprite != null)

        {

            // 元のマテリアル（共有）を保存

            originalMaterial = targetSprite.sharedMaterial;

        }

        else

        {

            Debug.LogWarning("DamageWhiteFlash: targetSprite が設定されていません");

        }

    }

    // ===== Trigger による当たり判定 =====

    void OnTriggerEnter2D(Collider2D other)

    {

        if (IsHitTarget(other.gameObject))

        {

            TryFlash();

        }

    }

    // ===== 通常の衝突判定 =====

    void OnCollisionEnter2D(Collision2D collision)

    {

        if (IsHitTarget(collision.gameObject))

        {

            TryFlash();

        }

    }

    // ===== タグ判定処理 =====

    bool IsHitTarget(GameObject obj)

    {

        // 設定された複数タグをチェック

        foreach (string tag in hitTargetTags)

        {

            if (obj.CompareTag(tag))

            {

                return true;

            }

        }

        return false;

    }

    // フラッシュ開始チェック

    void TryFlash()

    {

        if (!isFlashing && targetSprite != null && whiteFlashMaterial != null)

        {

            StartCoroutine(WhiteFlash());

        }

    }

    // ===== 白フラッシュ処理 =====

    IEnumerator WhiteFlash()

    {

        isFlashing = true;

        float elapsed = 0f;

        while (elapsed < flashDuration)

        {

            // 白フラッシュ

            targetSprite.material = new Material(whiteFlashMaterial);

            yield return new WaitForSeconds(flashInterval);

            // 元に戻す

            targetSprite.material = originalMaterial;

            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;

=======
    [Header("Hit Settings")]

    // 白フラッシュを発生させる相手のタグ（複数設定可）
    [SerializeField] private string[] hitTargetTags;

    void Awake()
    {
        if (targetSprite != null)
        {
            // 元のマテリアル（共有）を保存
            originalMaterial = targetSprite.sharedMaterial;
        }
        else
        {
            Debug.LogWarning("DamageWhiteFlash: targetSprite が設定されていません");
        }
    }

    // ===== Trigger による当たり判定 =====
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsHitTarget(other.gameObject))
        {
            TryFlash();
        }
    }

    // ===== 通常の衝突判定 =====
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsHitTarget(collision.gameObject))
        {
            TryFlash();
        }
    }

    // ===== タグ判定処理 =====
    bool IsHitTarget(GameObject obj)
    {
        // 設定された複数タグをチェック
        foreach (string tag in hitTargetTags)
        {
            if (obj.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    // フラッシュ開始チェック
    void TryFlash()
    {
        if (!isFlashing && targetSprite != null && whiteFlashMaterial != null)
        {
            StartCoroutine(WhiteFlash());
        }
    }

    // ===== 白フラッシュ処理 =====
    IEnumerator WhiteFlash()
    {
        isFlashing = true;
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            // 白フラッシュ
            targetSprite.material = new Material(whiteFlashMaterial);
            yield return new WaitForSeconds(flashInterval);

            // 元に戻す
            targetSprite.material = originalMaterial;
            yield return new WaitForSeconds(flashInterval);

            elapsed += flashInterval * 2;
>>>>>>> d70c7e42da5ce9cc6fe5ffd68e67db23ed50507b
        }

        targetSprite.material = originalMaterial;

        isFlashing = false;

    }

}

