using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesDefeated : MonoBehaviour
{
    public bool AshenStalker;
    public bool Calista;
    public bool chainedgirl;
    public bool FinalBoss;
    public bool Harlequinking;
    public bool Igris; 
    public bool Monarchoftime;
    public bool orion;
    public bool seraphine;
    public bool warden;
    public bool wyvern;
    public bool WeaponHallway;
    public bool ChainedGirl;
    public bool PostHarlquin;
    public bool OutsideTheLabyrinth;
    public static BossesDefeated instance;
    void Awake()
    {
        
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
