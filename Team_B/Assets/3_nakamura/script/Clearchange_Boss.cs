using UnityEngine;

using UnityEngine.SceneManagement;

using System.Collections;

public class ClearChange : MonoBehaviour

{

    public string sceneName;

    public float appearDelay = 0;

    private float appearDuration = 0f;

    public int maxHP = 6;

    private int hp;

    public float stopDistance = 0.3f;

    public float moveSpeed = 30f;

    public float stopYOffset = 1.0f;   // š’â~ˆÊ’u‚ğ­‚µã‚É‚·‚é’l

    private SpriteRenderer sr;

    private bool stop = false;

    private bool isAppeared = false;

    private bool isDead = false;  // š‰‰o’†‚©”»’è

    void Start()

    {

        hp = maxHP;

        sr = GetComponent<SpriteRenderer>();

        UpdateColor();

        StartCoroutine(AppearBoss());

    }

    void Update()

    {

        if (!isAppeared || isDead) return;

        if (!stop)

        {

            MoveToUpperCenter();

        }

    }

    IEnumerator AppearBoss()

    {

        transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(appearDelay);

        float t = 0;

        while (t < appearDuration)

        {

            t += Time.deltaTime;

            float p = t / appearDuration;

            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, p);

            yield return null;

        }

        transform.localScale = Vector3.one;

        isAppeared = true;

    }

    // š’†‰›‚Å‚Í‚È‚­u’†‰›‚æ‚è­‚µãv‚Å~‚Ü‚é

    void MoveToUpperCenter()

    {

        if (Camera.main == null) return;

        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        center.z = 0;

        center.y += stopYOffset;  // š’†‰›‚æ‚èã‚É•â³

        float dist = Vector3.Distance(transform.position, center);

        if (dist <= stopDistance)

        {

            stop = true;

            return;

        }

        transform.position = Vector3.MoveTowards(transform.position, center, moveSpeed * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D collision)

    {

        if (isDead) return;

        if (collision.CompareTag("BOSSdamage"))

        {

            hp--;

            UpdateColor();

            if (hp <= 0)

            {

                StartCoroutine(BossDeath());

            }

        }

    }

    // šHP‚É‰‚¶‚Ä”’¨Ô‚ÌF•Ï‰»

    void UpdateColor()

    {

        if (sr == null) return;

        float ratio = 1f - ((float)hp / maxHP);

        sr.color = Color.Lerp(Color.white, Color.red, ratio);

    }

    // š“|‚ê‚é‰‰o ¨ ƒV[ƒ“‘JˆÚ

    IEnumerator BossDeath()

    {

        isDead = true;

        // ‰‰oF1•b‚©‚¯‚Äk¬

        float duration = 1f;

        float t = 0;

        Vector3 startScale = transform.localScale;

        while (t < duration)

        {

            t += Time.deltaTime;

            float p = t / duration;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, p);

            yield return null;

        }

        Destroy(gameObject);

        // ƒV[ƒ“‘JˆÚ

        if (Application.CanStreamedLevelBeLoaded(sceneName))

        {

            SceneManager.LoadScene(sceneName);

        }

        else

        {

            Debug.LogError($"Scene '{sceneName}' ‚ª Build Settings ‚É“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB");

        }

    }

}

