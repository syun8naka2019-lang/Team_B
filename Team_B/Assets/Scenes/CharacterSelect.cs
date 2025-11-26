using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] characters;       // 選択できるキャラPrefab
    public Transform displayPoint;        // 表示位置（例：空のオブジェクト）
    

    private int currentIndex = 0;
    private GameObject currentPreview;

    void Start()
    {
        ShowCharacter(currentIndex);
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characters.Length;
        ShowCharacter(currentIndex);
    }

    public void PrevCharacter()
    {
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        ShowCharacter(currentIndex);
    }

    void ShowCharacter(int index)
    {
        // すでに表示中のキャラを削除
        if (currentPreview != null)
            Destroy(currentPreview);

        // 新しいキャラを表示
        currentPreview = Instantiate(characters[index], displayPoint.position, Quaternion.identity);
        currentPreview.transform.SetParent(displayPoint);

    
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SelectCharacter", currentIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}