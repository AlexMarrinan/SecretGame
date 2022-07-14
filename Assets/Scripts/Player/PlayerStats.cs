using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP = 10;
    public static PlayerStats instance;
    void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void IncreaseHP(){
        IncreaseHP(4);
    }
    public void IncreaseHP(int amount){
        maxHP += amount;
        currentHP += amount;
    }
    public void TakeDamage(int damage){
        currentHP -= damage;
    }
}