using UnityEngine;

using System.Collections;

public class random : MonoBehaviour

{

    public GameObject spawnPrefab;

    public float spawnInterval = 1.5f;

    public float spawnYOffset = 1f;

    public float startDelay = 3f;

    // ★ 横の範囲（ワールド座標で指定）

    public float minX = -2f;

    public float maxX = 2f;

    private Camera cam;

    void Start()

    {

        cam = Camera.main;

        StartCoroutine(SpawnLoop());

    }

    IEnumerator SpawnLoop()

    {

        yield return new WaitForSeconds(startDelay);

        while (true)

        {

            SpawnObject();

            yield return new WaitForSeconds(spawnInterval);

        }

    }

    void SpawnObject()

    {

        if (cam == null) return;

        float randomX = Random.Range(minX, maxX);

        // 画面上端の高さだけ取得

        Vector3 top = cam.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0));

        Vector3 spawnPos = new Vector3(randomX, top.y + spawnYOffset, 0);

        Instantiate(spawnPrefab, spawnPos, Quaternion.identity);

    }

}
