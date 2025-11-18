using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;

    private PlayerStatus status;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * status.moveSpeed;
        float y = Input.GetAxis("Vertical") * status.moveSpeed;

        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName);
        }
    }
}
