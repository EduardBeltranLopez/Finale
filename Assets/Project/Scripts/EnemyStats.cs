using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    
    #region Handles EnemyAttack
    [Header("AjustesDelDisparo")]
    [SerializeField, Tooltip("Time Between shoots")]
    public float waitTimeBetweenShoot;
    public float timer;

    [SerializeField, Tooltip("The bullet speed")]
    public float speedBullet;

    [SerializeField, Tooltip("Prefab of The Bullet")]
    public GameObject enemyBulletPrefab;

    [SerializeField, Tooltip("Point of reference to shoot")]
    public Transform shootPointEnemy;
    #endregion
     

   public GameObject InstantShootEnemyBullet()
    {
        GameObject bulletInstant = Instantiate(enemyBulletPrefab, shootPointEnemy.position, shootPointEnemy.rotation);
        bulletInstant.GetComponent<Rigidbody>().AddForce(shootPointEnemy.forward * speedBullet);
        return bulletInstant;
    }
}
