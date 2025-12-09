using UnityEngine;

public class LayerChanger2D : MonoBehaviour
{
    // WebEnemy レイヤー番号
    private int webEnemyLayer;

    // 一度だけ反応させるためのフラグ
    private bool hasChangedLayer = false;

    private void Awake()
    {
        webEnemyLayer = LayerMask.NameToLayer("WebEnemy");
        if (webEnemyLayer == -1)
        {
            Debug.LogWarning("Layer 'WebEnemy' が見つかりません。UnityのLayer設定で追加してください。");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // すでにレイヤー変更済みならスキップ（※ここが重要）
        if (hasChangedLayer)
        {
            // Debug.Log("既にLayer変更済みなので無視します");
            return;
        }

        // 相手のタグが Web の場合のみ実行
        if (other.CompareTag("Web"))
        {
            hasChangedLayer = true; // 最初に即フラグを立てる（連続反応防止）

            Debug.Log($"{gameObject.name} が {other.name} に触れました（最初の1回のみ）");

            if (webEnemyLayer != -1)
            {
                gameObject.layer = webEnemyLayer;
                Debug.Log($"{gameObject.name} のLayerを WebEnemy に変更しました");
            }
        }
    }
}
