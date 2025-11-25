using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<SkillBase> skills;

    private Animator animator;
    int maxSkillCount = 2;
    bool activeCondition;
    bool isPlayer;

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
        skills.Add(new ThunderStorm(gameObject.GetComponent<UnitBase>(), new ThunderStormConfig(), new List<EffectBase>()));
        skills.Add(new FireBall(gameObject.GetComponent<UnitBase>(), new FireBallConfig(), new List<EffectBase>()));
        //skills.Add(new Inferno(gameObject.GetComponent<UnitBase>(), new InfernoConfig(), new List<EffectBase>()));
        //skills.Add(new Revive(gameObject.GetComponent<UnitBase>(), new ReviveConfig(), new List<EffectBase>()));
        //skills.Add(new ScorchingAura(gameObject.GetComponent<UnitBase>(), new ScorchingAuraConfig(), new List<EffectBase>()));
        //skills.Add(new GlacialSpike(gameObject.GetComponent<UnitBase>(), new GlacialSpikeConfig(), new List<EffectBase>()));
        //skills.Add(new FrostNova(gameObject.GetComponent<UnitBase>(), new FrostNovaConfig(), new List<EffectBase>()));       
        //skills.Add(new TeslaTrap(gameObject.GetComponent<UnitBase>(), new TeslaTrapConfig(), new List<EffectBase>()));

        UnitBase character = gameObject.GetComponent<UnitBase>();
        if (character != null)
        {
            if (character is Player)
            {
                //skills.Add(new FireBall(gameObject.GetComponent<UnitBase>(), new FireBallConfig(), new List<EffectBase>()));
                isPlayer = true;
            }
            else if (character is Enemy)
            {
                isPlayer = false;

            }
            else if (character is Boss)
            {
                isPlayer = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = skills.Count - 1; i >= 0; i--)
        {
            switch (skills[i].skillConfig.activeCondition)
            {
                case SkillActiveCondition.OnAction:
                    if (isPlayer)
                    {
                        if (i == 1)
                        {
                            activeCondition = Input.GetKeyDown(KeyCode.E);
                        }
                        else if (i == 0)
                        {
                            activeCondition = Input.GetKeyDown(KeyCode.Q);
                        }
                    }
                    skills[i].OnActive(activeCondition);
                    if (activeCondition) UIManager.Instance.OnUsed(i, skills[i].skillConfig.parameters[0]);
                    break;
                case SkillActiveCondition.TargetIsEnemyInRange:

                    break;
                case SkillActiveCondition.TargetIsAllyInRange:

                    break;
                case SkillActiveCondition.TargetIsSelf:

                    break;
                case SkillActiveCondition.OnDead:
                    if (GetComponent<UnitBase>().isDead)
                    {
                        skills[i].OnActive(true);
                    }
                    break;
                case SkillActiveCondition.ASAP:
                    skills[i].OnActive(true);
                    break;
                default:
                    Debug.Log("nothing happens");
                    break;
            }
        }
    }
    //public void AddSkill(SkillBase skillToAdd, SkillBase skillToRemove)
    //{
    //    if (skills.Contains(skillToRemove))
    //    {
    //        skills.Remove(skillToRemove);
    //        skills.Add(skillToAdd);
    //    }
    //}
    public void AddSkill(SkillBase skillToAdd, int skillToRemoveIndex)
    {
        skills[skillToRemoveIndex] = skillToAdd;
    }
}
