using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour
{
    public float speed = 6f;                 // Webの移動速度
    private Rigidbody2D rb;
    private bool hasHitEnemy = false;        // すでに敵に当たったか
    private GameObject caughtEnemy = null;   // 捕まえた敵

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasHitEnemy)
        {
            // 通常は前進
            rb.linearVelocity = transform.right * speed;
        }
        else
        {
            // 敵に当たったらWebも止まる
            rb.linearVelocity = Vector2.zero;

            // 敵を止める
            if (caughtEnemy != null)
            {
                Enemy_RPG_Controller enemy = caughtEnemy.GetComponent<Enemy_RPG_Controller>();
                if (enemy != null)
                {
                    enemy.StopEnemy(); // 敵の停止処理
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHitEnemy && collision.CompareTag("Enemy"))
        {
            // 敵に当たった
            hasHitEnemy = true;
            caughtEnemy = collision.gameObject;

            // 必要ならここでWebのアニメーション停止やエフェクト
        }
    }
}