using System.Collections;
using UnityEngine;
public class Attack: MonoBehaviour
{
    protected StatConfigBase dmg;
    protected Animator animator;

    protected float cooldown = 0.5f;
    protected float timer;
    
    private void Update()
    {
        OnAttack();
    }

    public virtual void OnAttack()
    {
        
    }
}
