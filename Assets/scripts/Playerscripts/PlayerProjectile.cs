using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Transform ProjectilePoint;
    public float speed;
    public float speedMultiplier = 0;
    public Vector3 direction;
    public Vector3 mousePosition;
    public bool element;
    public int Damage = 3;
    public PlayerStats Player;
    public void Intialize(Transform Firepoint, int RangeAttackDamage, float RangeAttackSpeed)
    {
        ProjectilePoint = Firepoint;
        Damage = RangeAttackDamage;
        speed = RangeAttackSpeed;
    }
    void Start()
    {
        ProjectilePoint = FindObjectOfType<ProjectilePoint>().transform;
        Player = FindObjectOfType<PlayerStats>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        direction = (mousePosition - ProjectilePoint.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<BasicEnemy>().TakeDamage(Damage, element);
            PlayerStats.ProjectileCount--;
            Destroy(this.gameObject);
        }
        else if (other.GetComponent<FinalBossController>() != null && other.tag == "Boss")
        {
            if (other.GetComponent<FinalBossController>().BossPhase == 1)
            {
                if (!other.GetComponent<FinalBossController>().IsImmune)
                {
                    other.GetComponent<FinalBossController>().TakeDamage(Damage);
                    PlayerStats.ProjectileCount--;
                    Destroy(this.gameObject);
                }

            }
        }
        else if (GameObject.FindObjectOfType<AshenStalkerController>() != null && other.tag == "Boss")
        {
            other.GetComponent<AshenStalkerController>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else if (GameObject.FindObjectOfType<CalistaController>() != null && other.tag == "Boss")
        {
            other.GetComponent<CalistaController>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else if (GameObject.FindObjectOfType<HealingOrion>() != null && other.tag == "Boss")
        {
            if (other.GetComponent<HealingOrion>())
            {
                other.GetComponent<HealingOrion>().TakeDamage(Damage);
            }
            Destroy(this.gameObject);
        }
        else if (GameObject.FindObjectOfType<WyvernControler>() != null && other.tag == "Boss")
        {
            other.GetComponent<WyvernControler>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else if (GameObject.FindObjectOfType<SeraphineControler>() != null && other.tag == "Boss")
        {
            other.GetComponent<SeraphineControler>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else if (GameObject.FindObjectOfType<AttackingOrion>() != null && other.tag == "AttackOrion")
        {
            other.GetComponent<AttackingOrion>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }

        else if (other.tag == "Environment")
        {
            PlayerStats.ProjectileCount--;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Arise Enemy")
        {
            other.GetComponent<AriseEnemies>().TakeDamage(Damage);
            PlayerStats.ProjectileCount--;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Chains")
        {
            other.GetComponent<Chains>().TakeDamage(Damage, element);
            PlayerStats.ProjectileCount--;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Obelisk")
        {
            if (WardenObelisks.state)
            {
                other.GetComponent<WardenObelisks>().TakeDamage(Damage);
                PlayerStats.ProjectileCount--;
                Destroy(this.gameObject);
            }
        }
        else if (GameObject.FindObjectOfType<MonarchOfTimeController>() != null && other.tag == "Boss")
        {
            FindObjectOfType<MonarchOfTimeController>().TakeDamage(Damage);
            Destroy(this.gameObject);
        }
        else if (other.tag == "HourGlass")
        {
            other.GetComponent<HourGlass>().Takedamage(Damage);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Without element or Phases" && other.GetComponent<FakeOrionImage>() != null)
        {
            other.GetComponent<FakeOrionImage>().TakeDamage(Damage);
            PlayerStats.ProjectileCount--;
            Destroy(this.gameObject);
        }
    }
}
