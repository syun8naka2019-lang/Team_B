using UnityEngine;
using UnityEngine.UI;

public class SkillImageChanger : MonoBehaviour
{
    [Header("切り替える画像を順番に入れてね")]
    public Sprite[] skillSprites; // 順番に切り替える画像を格納する配列

    [Header("画像を表示するUIのImage")]
    public Image displayImage; // 実際に画面に表示するUI Image

    [Header("各画像の表示時間（秒）")]
    public float changeInterval = 1.0f; // 画像を切り替える間隔（秒）

    private int currentIndex = 0; // 現在表示中の画像番号
    private float timer = 0f; // 経過時間をカウントするためのタイマー
    private int loopCount = 0; // 画像を何周したかカウント

    [Header("Iキーで発射するスキル画像")]
    public GameObject nabeSkillPrefab; // 「なべスキル」のプレハブ（発射される画像）
    public Transform shootPoint; // 発射位置（ここからスキルが飛び出す）
    public float shootSpeed = 5f; // 発射するスピード（Rigidbody2Dが必要）

    void Start()
    {
        // スプライトまたは表示Imageが設定されていない場合はエラーを出して止める
        if (skillSprites.Length == 0 || displayImage == null)
        {
            Debug.LogError("画像またはImageが設定されていません！");
            enabled = false; // このスクリプトを無効化
            return;
        }

        // 最初の画像を表示する
        displayImage.sprite = skillSprites[0];
    }

    void Update()
    {
        // ============================
        // 🔫 スキル発射処理（Iキー）
        // ============================
        if (Input.GetKeyDown(KeyCode.I) && currentIndex == 5)
        {
            // Iキーを押した瞬間にスキルを発射する関数を呼ぶ
            ShootNabeSkill();

            // 画像ループが止まっていた場合（enabled = false）なら再開させる
            if (!enabled)
            {
                enabled = true;
            }

            // ループカウントなどをリセットして、もう一度最初から画像を再生できるようにする
            loopCount = 0;           // 「1周終わった」状態を解除
            timer = 0f;              // 経過時間をリセット
            currentIndex = 0;        // 最初の画像からやり直す
            displayImage.sprite = skillSprites[0]; // 最初の画像を表示
        }

        // ============================
        // 🔁 画像の切り替え処理
        // ============================
        timer += Time.deltaTime; // フレームごとに経過時間を加算

        // 一定時間（changeInterval）経過したら次の画像へ
        if (timer >= changeInterval)
        {
            timer = 0f;      // タイマーをリセット
            currentIndex++;  // 次の画像へ切り替え

            // 画像の最後まで行ったら最初に戻る
            if (currentIndex >= skillSprites.Length)
            {
                currentIndex = 5; // 
                //loopCount++;      // 1周完了をカウント
            }

            // 画像が1周したらループを止める
            if (loopCount >= 1)
            {
                //enabled = false; // Update()の動作を止める（＝一時停止状態）
                //return;          // これ以降の処理は実行されない
            }

            // 実際にImageの画像を変更
            displayImage.sprite = skillSprites[currentIndex];
        }
    }


    /// <summary>
    /// 「なべスキル」を発射する関数
    /// </summary>
    private void ShootNabeSkill()
    {
        // プレハブが設定されていない場合はエラー
        if (nabeSkillPrefab == null)
        {
            Debug.LogError("nabeSkillPrefab が設定されていません！");
            return;
        }

        // 発射位置を決定（shootPointが設定されていなければ自分の位置）
        Vector3 spawnPos = shootPoint != null ? shootPoint.position : transform.position;

        // 「なべスキル」を生成
        GameObject skill = Instantiate(nabeSkillPrefab, spawnPos, Quaternion.identity);

        // Rigidbody2Dがついていれば右方向に飛ばす
        Rigidbody2D rb = skill.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.right * shootSpeed;
        }

        // 3秒後に自動で消す（メモリ節約）
        Destroy(skill, 3f);
    }
}
