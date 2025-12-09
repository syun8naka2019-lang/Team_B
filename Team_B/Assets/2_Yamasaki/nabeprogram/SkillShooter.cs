using UnityEngine;

public class SkillShooter : MonoBehaviour
{
    public GameObject nabeSkillPrefab; // 発射するスキルのプレハブ
    public Transform firePoint;        // 発射位置

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(nabeSkillPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("なべスキル発射！！");
    }
}