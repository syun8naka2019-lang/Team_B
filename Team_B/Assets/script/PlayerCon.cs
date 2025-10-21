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
    


    private void Start()
    {
       
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

