using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCon : MonoBehaviour
{
    public static string gameState = "playing";
    public string sceneName;
    // �v���C���[�̈ړ����x
    private int moveSpeed = 7;

    Animator animator;
    public string stop0 = "gade0";
    public string stop1 = "gade1";
    public string stop2 = "gade2";
    public string stop3 = "gade3";
    public string stop4 = "gade4";
    public string stop10 = "stopstop";
    string nowAnime = "";
    string oldAnime = "";
    int cnt = 0;


    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }
    void Update()
    {
      
        Move();
    }

    private void FixedUpdate()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("�Q�[���I�[�o�[");

        if (collision.gameObject.tag == "Dead")
        {
            Debug.Log("�Q�[���I�[�o�[");
            Destroy(this.gameObject);
           
            SceneManager.LoadScene(sceneName);

        }
        if (collision.gameObject.tag == "item")
        {
            cnt++;
            if (cnt == 0)
                nowAnime = stop10;
            else if (cnt == 1)
                nowAnime = stop0;






        }









    }
    public void Goal()
    {
        gameState = "gameclear";
       


    }

    public void GameOver()
    {
        gameState = "gameover";
       
        GetComponent<CapsuleCollider2D>().enabled = false;
            //�V�[����ǂݍ���
  

    
    // rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

     }
 
    // �v���C���[���ړ�������
    private void Move()
    {
        // �L�[�̓��͒l���擾
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float y = Input.GetAxis("Vertical") * moveSpeed;

        // �擾�������͒l���v���C���[�̈ʒu�ɔ��f������
        transform.position += new Vector3(x, y, 0) * Time.deltaTime;
    }
}

