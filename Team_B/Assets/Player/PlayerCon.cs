using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour

{

    public static string gameState = "playing";

    public string sceneName;

<<<<<<< HEAD
    private PlayerStatus status;

    private Hp playerHp; // ← 追加



    // ←ここは実際のクラス名に合わせて！
=======
    private PlayerStatus_M status; // ← ここを PlayerStatus_M に変更
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49

    void Start()

    {
<<<<<<< HEAD

        // PlayerStatus の取得チェック

        status = GetComponent<PlayerStatus>();

        playerHp = GetComponent<Hp>(); // PlayerHp を取得

=======
        // PlayerStatus_M の取得チェック
        status = GetComponent<PlayerStatus_M>();
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
        if (status == null)

        {
<<<<<<< HEAD

            Debug.LogError("PlayerStatus が Player にアタッチされていません！");

        }

        // HP管理の取得チェック

        playerHp = GetComponent<Hp>(); // ←または PlayerHp

        if (playerHp == null)

        {

            Debug.LogError("Hp（または PlayerHp） スクリプトが Player にアタッチされていません！");

=======
            Debug.LogError("PlayerStatus_M が Player にアタッチされていません！");
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
        }

    }

    void Update()

    {

        Move();

    }

    private void Move()

    {
        if (status == null) return;  // Null 防止

        float x = Input.GetAxis("Horizontal") * status.moveSpeed;

        float y = Input.GetAxis("Vertical") * status.moveSpeed;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;

    }

    void OnTriggerEnter2D(Collider2D collision)

    {
<<<<<<< HEAD

        // 何かに当たったとき

=======
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))

        {
<<<<<<< HEAD

            playerHp.TakeDamage(1); // PlayerHp の TakeDamage を呼ぶ

=======
            if (status != null)
            {
                status.TakeDamage(1); // 敵や死んだ場所に当たったらダメージ
            }
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
        }

    }
<<<<<<< HEAD

    public void Goal()

    {

        gameState = "gameclear";

    }

    public void GameOver()

    {

        gameState = "gameover";

        GetComponent<CapsuleCollider2D>().enabled = false;

    }




=======
>>>>>>> 3f693ca58de3b5d593ac4d6ce3cfa3cf3a5ccc49
}

