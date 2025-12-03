using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearChange : MonoBehaviour
{
    public string sceneName;   // クリア後に読み込むシーン名
    int hp = 3;                // 敵のHP
    public float stopDistance = 0.3f; // 中心にどれだけ近づいたら止まるか
    public float moveSpeed = 2f;      // 敵が中央に向かう速度

    private bool stop = false; // 止まったかどうか

    void Update()
    {
        // ★中央に向かって移動（まだ止まっていなければ）
        if (!stop)
        {
            MoveToCenter();
        }

        // HP が 0 以下 → 自身を削除してシーン遷移
        if (hp <= 0)
        {
            Destroy(gameObject);

            if (Application.CanStreamedLevelBeLoaded(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"Scene '{sceneName}' が Build Settings に登録されていません。");
            }
        }
    }

    void MoveToCenter()
    {
        // カメラ中心のワールド座標
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        center.z = 0; // 2DなのでZは0に

        // 中心との距離
        float dist = Vector3.Distance(transform.position, center);

        // ★距離が規定値以下なら停止
        if (dist <= stopDistance)
        {
            stop = true;
            return;
        }

        // ★中心に向かって移動
        transform.position = Vector3.MoveTowards(transform.position, center, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Web に当たったら HP 減少
        if (collision.CompareTag("Web"))
        {
            hp--;
        }
    }
}