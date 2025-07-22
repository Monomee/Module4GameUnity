using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : MonoBehaviour
{
    public int teamID;
    protected Health healthComponent;
    public float hp;
    protected void HealthInit ()
    {
        if (healthComponent == null)
        {
            healthComponent = new Health(hp);
        }
    }
    public virtual void OnTakeDmg(float damage)
    {
        healthComponent?.OnTakeDmg(damage);     
    }

    public bool IsDead()
    {
        return healthComponent?.hp <= 0;
    }
}
