using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class OnEnterCollideSkill : MonoBehaviour
{
    private UnitBase fromOwner;
    private SkillBase fromSkill;
    private Vector3 direction;
    private float damage;
    private float speed; 
    public void Initialize(UnitBase fromOwner, SkillBase fromSkill, Vector3 direction, Transform startPosition, float damage, float duration, float speed)
    {
        this.fromOwner = fromOwner;
        this.fromSkill = fromSkill;
        this.direction = direction.normalized;
        transform.position = startPosition.position;
        transform.rotation = Quaternion.LookRotation(direction);
        this.damage = damage;
        this.speed = speed;
        Invoke("Deactivate", duration);
    }
    void Deactivate()
    {
        CancelInvoke();
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fromOwner.tag))
        {
            return;
        }
        if (other.CompareTag("CanTakeDmg") || other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.OnTakeDmg(damage);
                Debug.Log(other.name + " took " + damage + " damage from the entercollideskill. Current HP: " + health.health);
                if (other.CompareTag("Player"))
                {
                    UIManager.Instance.HPSlider.value = other.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].value;
                }
                fromSkill.ApplyEffects();
                fromOwner.GetComponent<EffectManager>().ActiveEffect(fromSkill.skillConfig.codeName, other.GetComponent<UnitBase>());
            }
        }        
    }
}
