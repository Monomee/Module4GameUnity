using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackFireball : MonoBehaviour
{
    private Vector3 direction;
    private float damage;
    private float speed;
    float duration = 5f; 
    public void Initialize(Vector3 direction, Transform startPosition, float damage, float speed)
    {
        this.direction = direction.normalized;
        transform.position = startPosition.position + Vector3.up;
        transform.rotation = Quaternion.LookRotation(direction);
        this.damage = damage;
        this.speed = speed;       
    }
    private void OnEnable()
    {
        Invoke("Deactivate", duration);
    }
    void Deactivate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CanTakeDmg"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.OnTakeDmg(damage);
                Debug.Log(other.name + " took " + damage + " damage from the projectile. Current HP: " + health.health);
            }
            Deactivate();
        }

    }
}
