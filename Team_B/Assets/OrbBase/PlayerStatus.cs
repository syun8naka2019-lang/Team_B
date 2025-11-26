using System.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("ステータス")]
    public int score = 0;
    public int hp = 5;

    [Header("移動速度（基本値）")]
    public float moveSpeed = 7f;

    private float baseSpeed;  // 元のスピードを保持

    private void Start()
    {
        baseSpeed = moveSpeed; // 初期スピードを記録
    }

    // スコア加算
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

    // HP回復
    public void Heal(int currentHp)
    {
        hp += currentHp;
        Debug.Log("HP: " + hp);
    }

    // 永続スピードアップ（必要なら）
    public void AddSpeed(float amount)
    {
        moveSpeed += amount;
        Debug.Log("Speed Up! 現在: " + moveSpeed);
    }

    // 一時的スピードアップ（青オーブ用）
    public void AddSpeedTemporary(float amount, float duration)
    {
        StartCoroutine(SpeedUpCoroutine(amount, duration));
    }

    private IEnumerator SpeedUpCoroutine(float amount, float duration)
    {
        moveSpeed += amount;
        Debug.Log("Speed Up! 現在: " + moveSpeed);

        yield return new WaitForSeconds(duration);

        moveSpeed -= amount;
        Debug.Log("Speed Down… 現在: " + moveSpeed);
    }
}
