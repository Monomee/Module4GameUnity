using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : UnitBase
{
    private Animator animator;
    [SerializeField] private GameObject ulPre;
    float timer = 0f;
    bool isUltimate = false;
    private void Awake()
    {
        HealthInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        Debug.Log("PlayerStats initialized with HP: " + hp);
        if (roleStat == null)
        {
            roleStat = new RoleStat();
        }
        //roleStat.dictStats = new Dictionary<StatType, StatConfigBase>();
        roleStat.dictStats.Add(StatType.Atk, new Attack(StatType.Atk, 100, 0.5f, 0, 0.2f)); 
        if (skills == null)
        {
            skills = new List<SkillBase>();
        }
        TestUltimateCfg testUltimateCfg = new TestUltimateCfg();
        TestUltimateInfo testUltimateInfo = new TestUltimateInfo();
        TestUltimate ultimate = new TestUltimate(testUltimateInfo, testUltimateCfg, this);
        skills.Add(ultimate);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            if (roleStat == null)
            {
                Debug.Log("no stat");
                return;
            }
            if (roleStat.dictStats.ContainsKey(StatType.Atk))
            {
                ((Attack)roleStat.dictStats[StatType.Atk]).DoAttack(this.transform, TestPool.SharedInstance.GetPooledObject().GetComponent<Projectile>());
            }
            else
            {
                Debug.Log("No atk");
            }
        }
        if (isUltimate)
        {
            timer += Time.deltaTime;
            animator.SetFloat("Duration", timer);
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isUltimate = true;

            skills[0].SetSkillPrefab(ulPre);
            //ultimate.GetSkillPrefab().transform.SetParent(this.transform);

            animator.SetTrigger("Attack");
            animator.SetBool("Ultimate", true);
            ((TestUltimate)skills[0]).Cast();
        }
        if (timer > 5f)
        {
            ((TestUltimate)skills[0]).EndCast();
            isUltimate = false;
            animator.SetBool("Ultimate", false);
            timer = 0f;
            Debug.Log("Ultimate ended");
        }
    }
}
