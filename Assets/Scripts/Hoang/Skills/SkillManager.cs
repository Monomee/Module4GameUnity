using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    protected List<SkillBase> skills;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (skills == null)
        {
            skills = new List<SkillBase>();
        }
      //  Activator.CreateInstance(typeof(FireBall), new object[] { });   tu codename skill=> new object skill
        skills.Add(new FireBall(gameObject.GetComponent<UnitBase>(), new FireBallConfig(), new List<EffectBase>()));

        skills.Add(new Revive(gameObject.GetComponent<UnitBase>(), new ReviveConfig(), new List<EffectBase>()));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SkillBase skill in skills)
        {
            switch (skill.skillConfig.activeCondition)
            {
                case SkillActiveCondition.OnAction:
                    skill.OnActive();     
                    break;
                case SkillActiveCondition.TargetIsEnemyInRange:

                    break;
                case SkillActiveCondition.TargetIsAllyInRange:

                    break;
                case SkillActiveCondition.TargetIsSelf:

                    break;
                case SkillActiveCondition.OnDead:
                    //if (gameObject.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].GetValue() <= 0)
                    //{
                    //skill.OnActive();
                    //    skills.Remove(skill);
                    //}
                    break;
                case SkillActiveCondition.ASAP:
                    skill.OnActive();
                    break;
                default:
                    Debug.Log("nothing happens");
                    break;
            }
            
        }              
    }
}
