using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionFollow : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance > 1){
        speed = 4;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }else if(distance<=1){
            speed = 0;
        }

        animator.SetFloat("speed",speed);
    }
}
