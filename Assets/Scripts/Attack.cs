using System.Collections;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Attack: MonoBehaviour
{
    StatConfigBase dmg;
    [SerializeField] Animator animator;

    float cooldown = 0.5f;
    float timer;
    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        dmg = new StatConfigBase(StatType.HP, 10, 1, 0, 1, 0, 0, 100);
        timer = cooldown;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && Input.GetMouseButtonDown(0))
        {
            //if ( != null)
            //{
                animator.SetTrigger("Attack");

                Vector3 projectileDir = transform.forward;

                if (gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
                    Ray ray = Camera.main.ScreenPointToRay(screenCenter);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100f))
                    {
                        if (Mathf.Abs(Vector3.Dot(ray.direction, transform.forward)) <= 0.9f)
                        {
                            if (hit.collider != null)
                            {
                                projectileDir = (hit.point - transform.position).normalized;
                            }
                            else
                            {
                                projectileDir = ray.direction;
                            }
                        }
                    }
                }

                NormalAttackFireball projectile = TestPool.SharedInstance.GetPooledObject().GetComponent<NormalAttackFireball>();
                if (projectile != null)
                {
                    float damage = dmg.GetValue();
                    float speed = 10f;
                    projectile.Initialize(projectileDir, transform, damage, speed);
                    projectile.gameObject.SetActive(true);
                    timer = cooldown;
                }
            //}
        }
    }
}
