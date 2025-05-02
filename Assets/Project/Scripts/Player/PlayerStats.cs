using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    #region Handles VidaPlayer
    [Header("VidaDelPlayer")]
    [Tooltip("How many Hp has the player")]public int hp;
    [Tooltip("Current Hp the player has")]public int currentHp;
    [Tooltip("Los UI del daño")]public GameObject[] onScreenDamage;

    EnemyStats enemyStats;
    #endregion

    private void Start()
    {
        currentHp = hp;
    }

    private void Update()
    {
        RespawnConRestart();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Balloon")
        {
            Destroy(other.gameObject);
            ChangeDamage();
        }
    }

    void ChangeDamage()
    {
        currentHp--;
        onScreenDamage[currentHp].SetActive(true);
        onScreenDamage[currentHp + 1].SetActive(false);
    }

    void RespawnConRestart()
    {
        if (currentHp == -1)
        {
            onScreenDamage[0].SetActive(false);
            currentHp = hp;
            onScreenDamage[currentHp].SetActive(true);

        }
    }
}

