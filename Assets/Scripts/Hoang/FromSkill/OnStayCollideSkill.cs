using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStayCollideSkill : MonoBehaviour
{
    private UnitBase fromOwner;
    private SkillBase fromSkill;
    private Vector3 startPosition;
    private float damage;
    private float damageInterval = 1f;
    private readonly Dictionary<GameObject, float> lastDamageTimes = new Dictionary<GameObject, float>();
    public void Initialize(UnitBase fromOwner, SkillBase fromSkill, Vector3 startPosition, float damage, float duration)
    {
        this.fromOwner = fromOwner;
        this.fromSkill = fromSkill;
        this.startPosition = startPosition.normalized;
        this.damage = damage;
        Invoke("Deactivate", duration);
    }
    void Deactivate()
    {
        CancelInvoke();
        lastDamageTimes.Clear();
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(fromOwner.tag))
        {
            return;
        }
        if (other.CompareTag("CanTakeDmg"))
        {
            //Health health = other.GetComponent<Health>();
            //if (health != null)
            //{
            //    health.OnTakeDmg(damage);
            //    Debug.Log(other.name + " took " + damage + " damage from the staycollideskill. Current HP: " + health.health);
            //}

            float currentTime = Time.time;

            if (!lastDamageTimes.ContainsKey(other.gameObject))
            {
                lastDamageTimes[other.gameObject] = 0f;
            }

            if (currentTime - lastDamageTimes[other.gameObject] >= damageInterval)
            {
                Health health = other.GetComponent<Health>();
                if (health != null)
                {
                    health.OnTakeDmg(damage);
                    Debug.Log($"{other.name} took {damage} damage from the staycollideskill. Current HP: {health.health}");
                    lastDamageTimes[other.gameObject] = currentTime; 
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fromOwner.tag))
        {
            return;
        }
        if (other.CompareTag("CanTakeDmg"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                fromOwner.GetComponent<EffectManager>().ActiveEffect(other.GetComponent<UnitBase>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CanTakeDmg"))
        {
            lastDamageTimes.Remove(other.gameObject); 
        }
    }
}
