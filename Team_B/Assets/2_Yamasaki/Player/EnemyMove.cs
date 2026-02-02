using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour
{
    // 移動先
    private Vector3 targetPos = new Vector3(-0.3f, 2.3f, 0f);

    // 移動速度
    public float moveSpeed = 2f;

    // 往復の幅
    public float horizontalRange = 1.5f;

    // 往復速度
    public float horizontalSpeed = 2f;

    private bool arrived = false;
    private Vector3 centerPos;

    void Start()
    {
        // 3秒後に移動開始
        StartCoroutine(MoveToTargetAfterDelay());
    }

    IEnumerator MoveToTargetAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        // 指定位置へ移動
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        // 到達
        transform.position = targetPos;
        arrived = true;
        centerPos = transform.position;
    }

    void Update()
    {
        if (arrived)
        {
            // 左右に往復（Sin波）
            float x = Mathf.Sin(Time.time * horizontalSpeed) * horizontalRange;
            transform.position = new Vector3(
                centerPos.x + x,
                centerPos.y,
                centerPos.z
            );
        }
    }
}
