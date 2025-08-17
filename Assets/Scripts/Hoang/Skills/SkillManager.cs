using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    protected List<SkillBase> skills;

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
        //skills.Add(new FireBall(gameObject.GetComponent<UnitBase>(), new FireBallConfig(), new List<EffectBase>()));
        //skills.Add(new Inferno(gameObject.GetComponent<UnitBase>(), new InfernoConfig(), new List<EffectBase>()));
        skills.Add(new Revive(gameObject.GetComponent<UnitBase>(), new ReviveConfig(), new List<EffectBase>()));
        //skills.Add(new ScorchingAura(gameObject.GetComponent<UnitBase>(), new ScorchingAuraConfig(), new List<EffectBase>()));
        //skills.Add(new GlacialSpike(gameObject.GetComponent<UnitBase>(), new GlacialSpikeConfig(), new List<EffectBase>()));
        //skills.Add(new FrostNova(gameObject.GetComponent<UnitBase>(), new FrostNovaConfig(), new List<EffectBase>()));
        //skills.Add(new ThunderStorm(gameObject.GetComponent<UnitBase>(), new ThunderStormConfig(), new List<EffectBase>()));
        //skills.Add(new TeslaTrap(gameObject.GetComponent<UnitBase>(), new TeslaTrapConfig(), new List<EffectBase>()));

        UnitBase character = gameObject.GetComponent<UnitBase>();
        if (character != null)
        {
            if (character is Player)
            {
                skills.Add(new FireBall(gameObject.GetComponent<UnitBase>(), new FireBallConfig(), new List<EffectBase>()));
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
        foreach (SkillBase skill in skills)
        {
            switch (skill.skillConfig.activeCondition)
            {
                case SkillActiveCondition.OnAction:
                    if (isPlayer)
                    {
                        if (skills.Count != 1)
                        {
                            activeCondition = Input.GetKeyDown(KeyCode.E);
                        }
                        else if (skills.Count == 1)
                        {
                            activeCondition = Input.GetKeyDown(KeyCode.Q);
                        }
                    }

                    skill.OnActive();
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
                        skill.OnActive();
                    }
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
