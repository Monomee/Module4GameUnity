using UnityEngine;

public class Attack : StatConfigBase
{
    public Attack(StatType statType, float baseValue, float basePercentValue, float otherValue, float allPercentValue) : base(statType, baseValue, basePercentValue, otherValue, allPercentValue)
    {
        this.statType = StatType.Atk;
        this.baseValue = baseValue;
        this.basePercentValue = basePercentValue;
        this.otherValue = otherValue;
        this.allPercentValue = allPercentValue;
    }

    public void DoAttack(Transform owner, Projectile projectile = null)
    {
        if (owner == null) return;
        
        Vector3 projectileDir = owner.forward;
        //float offset = 1.5f;
        if (owner.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (Mathf.Abs(Vector3.Dot(ray.direction, owner.forward)) <= 0.9f)
                {
                    if (hit.collider != null)
                    {
                        projectileDir = (hit.point - owner.position).normalized;
                    }
                    else
                    {
                        projectileDir = ray.direction;
                    }
                }                
            }
        }

        if (projectile != null)
        {
            projectile.Initialize(projectileDir, owner);
            projectile.gameObject.SetActive(true);
        }
        
    }
    //public bool CanAttack(float range, float coolDownTimer)
    //{
    //    return range <= attackRange && coolDownTimer >= attackCooldown;
    //}
}
