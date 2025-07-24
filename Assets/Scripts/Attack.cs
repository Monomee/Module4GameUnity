using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage { get; set; }
    public float attackRange { get; set; }
    public float attackSpeed { get; set; }
    public float attackCooldown { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DoAttack(Transform owner, Projectile projectile = null)
    {
        if (owner == null) return;
        Vector3 projectileDir = owner.forward;
        //float offset = 1.5f;
        //if (owner.gameObject.layer == LayerMask.NameToLayer("Player"))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, attackRange))
        //    {
        //        if (hit.collider != null)
        //        {
        //        }
        //        else
        //        {
        //            projectileDir = projectile ? owner.forward : Vector3.zero;
        //        }
        //    }
        //}
        if (projectile != null)
        {
            projectile.Initialize(projectileDir, owner);
            projectile.gameObject.SetActive(true);
        }
        
    }
    public bool CanAttack(float range, float coolDownTimer)
    {
        return range <= attackRange && coolDownTimer >= attackCooldown;
    }
}
