using UnityEngine;

// 敵を左右に往復移動させるスクリプト
public class EnemyMoveHorizontal : MonoBehaviour
{
    public float moveRange = 3f;      // 左右に動く最大距離（その場を基準）
    public float roundTripTime = 3f;  // 左→右→左 の往復にかかる時間（秒）
    public float startDelay = 3f;     // 何秒後に往復を開始するか

    private Vector3 startPos;         // 往復開始時点の位置
    private float timer;
    private bool canMove = false;

    void Start()
    {
        // startDelay秒後に往復開始
        Invoke(nameof(StartMove), startDelay);
    }

    void Update()
    {
        // まだ往復開始していなければ何もしない
        if (!canMove) return;

        // 経過時間を加算
        timer += Time.deltaTime;

        // 0～1 を往復する値を作る
        float t = Mathf.PingPong(timer, roundTripTime) / roundTripTime;

        // -moveRange ～ +moveRange に変換
        float x = Mathf.Lerp(-moveRange, moveRange, t);

        // 「3秒後にいた場所」を基準に左右移動
        transform.position = new Vector3(
            startPos.x + x,
            startPos.y,
            startPos.z
        );
    }

    // 往復開始処理
    void StartMove()
    {
        canMove = true;
        timer = 0f;

        // ★ 3秒経過した時点の位置を保存
        startPos = transform.position;
    }
}
