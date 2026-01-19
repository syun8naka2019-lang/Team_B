using UnityEngine;

public class ScrollObjects : MonoBehaviour
{
    public float speed = 5f;           // スクロール速度
    public GameObject[] backgrounds;   // Inspectorでシーン内の背景オブジェクトをセット

    private Transform[] bgTransforms;
    private float spriteHeight;

    void Start()
    {
        if (backgrounds.Length == 0)
        {
            Debug.LogError("背景オブジェクトが設定されていません！");
            return;
        }

        // Transform 配列に変換
        bgTransforms = new Transform[backgrounds.Length];

        // 高さは 1 枚目の Renderer から取得（BoxColliderやSpriteRendererなどで計算可能）
        Renderer rend = backgrounds[0].GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("背景オブジェクトに Renderer がありません！");
            return;
        }
        spriteHeight = rend.bounds.size.y;

        // 配置：カメラ上端から順番に並べる
        float camTop = Camera.main.transform.position.y + Camera.main.orthographicSize;
        for (int i = 0; i < backgrounds.Length; i++)
        {
            bgTransforms[i] = backgrounds[i].transform;
            float yPos = camTop - i * spriteHeight - spriteHeight / 2;
            bgTransforms[i].position = new Vector3(0, yPos, bgTransforms[i].position.z);
        }
    }

    void Update()
    {
        for (int i = 0; i < bgTransforms.Length; i++)
        {
            // 下方向にスクロール
            bgTransforms[i].position -= new Vector3(0, speed * Time.deltaTime, 0);

            // 下に出たら一番上に移動
            if (bgTransforms[i].position.y <= Camera.main.transform.position.y - Camera.main.orthographicSize - spriteHeight / 2)
            {
                float maxY = float.MinValue;
                for (int j = 0; j < bgTransforms.Length; j++)
                    if (bgTransforms[j].position.y > maxY) maxY = bgTransforms[j].position.y;

                bgTransforms[i].position = new Vector3(bgTransforms[i].position.x, maxY + spriteHeight, bgTransforms[i].position.z);
            }
        }
    }
}
