using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("íeÇÃê›íË")]
    public GameObject objPrefab;        // î≠éÀíePrefab
    public float delayTime = 0.5f;      // íeî≠éÀä‘äu
    public float fireSpeed = 4.0f;      // íeÇÃë¨ìx

    private Transform gateTransform;    // î≠éÀå˚
    private float passedTimes = 0f;     // î≠éÀÉ^ÉCÉ}Å[
    private bool stopByWeb = false;     // WebÇ…êGÇÍÇΩÇÁí‚é~
    private int webTouchCount = 0;      // Webê⁄êGêî

    void Start()
    {
        gateTransform = transform.Find("gate"); // éqÉIÉuÉWÉFÉNÉg "gate" ÇéÊìæ
        if (gateTransform == null)
            Debug.LogWarning("î≠éÀå˚ 'gate' Ç™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅI");
    }

    void Update()
    {
        // WebÇ…êGÇÍÇƒÇ¢Ç»ÇØÇÍÇŒî≠éÀ
        if (!stopByWeb)
        {
            passedTimes += Time.deltaTime;
            if (passedTimes > delayTime)
            {
                FireCannon();
                passedTimes = 0f;
            }
        }
    }

    void FireCannon()
    {
        if (objPrefab == null || gateTransform == null) return;

        GameObject obj = Instantiate(objPrefab, gateTransform.position, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(Vector2.down * fireSpeed, ForceMode2D.Impulse); // â∫ï˚å¸Ç…î≠éÀ
    }

    // WebÇ…êGÇÍÇΩèuä‘
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            webTouchCount++;
            stopByWeb = true;  // î≠éÀí‚é~
        }
    }

    // WebÇ©ÇÁó£ÇÍÇΩèuä‘
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Web"))
        {
            webTouchCount--;
            if (webTouchCount <= 0)
            {
                webTouchCount = 0;
                stopByWeb = false; // î≠éÀçƒäJ
            }
        }
    }
}