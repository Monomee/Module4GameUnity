using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected float speed;
    protected float damage;
    protected Vector3 direction;

    public void Init(Vector3 direction, float speed, float damage)
    {
        this.direction = direction.normalized;
        this.speed = speed;
        this.damage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Player target = other.GetComponent<Player>();
        if (target != null && !target.IsDead())
        {
            target.OnTakeDmg(damage);
            gameObject.SetActive(false); 
        }
    }


}
