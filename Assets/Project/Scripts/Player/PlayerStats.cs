using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class AttackStats : MonoBehaviour
{
    #region Handles VidaPlayer
    [Header("VidaDelPlayer")]
    [SerializeField, Tooltip("How many Hp has the player")]
    public int hp;

    [SerializeField, Tooltip("Current Hp the player has")]
    public int currentHp;

    [SerializeField, Tooltip("Los UI del daño")]
    public GameObject[] onScreenDamage;
    #endregion

    #region Handles EnemyAttack
    [Header("AjustesDelDisparo")]
    [SerializeField, Tooltip("Time Between shoots")]
    public float waitTimeBetweenShoot;

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

