using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus_M status; // ← ここを PlayerStatus_M に変更

    void Start()
    {
        // PlayerStatus_M の取得チェック
        status = GetComponent<PlayerStatus_M>();
        if (status == null)
        {
            Debug.LogError("PlayerStatus_M が Player にアタッチされていません！");
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
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))
        {
            if (status != null)
            {
                status.TakeDamage(1); // 敵や死んだ場所に当たったらダメージ
            }
        }
    }
}
