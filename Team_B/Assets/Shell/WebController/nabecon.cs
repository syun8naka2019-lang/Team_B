
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ’eiWebj‚ÌƒRƒ“ƒgƒ[ƒ‰
/// - ã•ûŒüi‚Ü‚½‚Í©—R•ûŒüj‚ÉˆÚ“®
/// - 1‰ñƒqƒbƒg‚µ‚½“G‚Å~‚Ü‚é
/// - ~‚Ü‚Á‚½’e‚Í‘¼‚Ì“G‚É‰e‹¿‚µ‚È‚¢
/// </summary>
public class nabecon : MonoBehaviour
{
    [Header("ˆÚ“®İ’è")]
    public float speed = 10f;              // ’e‚Ì‘¬“x

    private Rigidbody2D rb;                // Rigidbody2D
    private bool isStopped = false;        // ’e‚ª~‚Ü‚Á‚½‚©
    private bool hasHitEnemy = false;      // 1‰ñƒqƒbƒgÏ‚İ‚©


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ’â~‚µ‚Ä‚¢‚È‚¯‚ê‚Îã•ûŒü‚ÉˆÚ“®
        if (!isStopped)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }

}