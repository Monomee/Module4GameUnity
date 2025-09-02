using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] public float arrowDamage = 5.0f;
    [SerializeField] float flightLifetime = 8f;   // tự hủy nếu không trúng
    [SerializeField] float stickLifetime = 12f;  // tồn tại sau khi dính
    [SerializeField] string stickOnlyOnTag = "Player"; // chỉ dính mục tiêu có tag này (để trống = dính mọi thứ)

    Rigidbody rb;
    Collider col;
    bool hasHit;

    // Thông tin mỗi phát bắn (truyền từ chỗ spawn)
    Transform owner;              // kẻ bắn
    Collider[] ownerCols;         // collider của kẻ bắn (để ignore)
    float damage;

    Coroutine lifeCo;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        hasHit = false;

        // reset vật lý
        if (rb)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        if (col) col.enabled = true;

        // hẹn giờ tự hủy nếu không trúng
        if (lifeCo != null) StopCoroutine(lifeCo);
        lifeCo = StartCoroutine(DespawnAfter(flightLifetime));
    }

    /// <summary>Gọi ngay sau khi SetActive(true) để truyền thông tin phát bắn.</summary>
    public void Init(Transform ownerTf, Collider[] ownerColliders, float dmg)
    {
        owner = ownerTf;
        ownerCols = ownerColliders;
        damage = dmg;

        // Không va chạm với kẻ bắn
        if (col && ownerCols != null)
            for (int i = 0; i < ownerCols.Length; i++)
                if (ownerCols[i]) Physics.IgnoreCollision(col, ownerCols[i], true);
    }

    void FixedUpdate()
    {
        // Cho đẹp: đang bay thì quay theo quỹ đạo
        if (!hasHit && rb && rb.velocity.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnCollisionEnter(Collision c)
    {
        if (hasHit) return;
        if (owner && c.transform.IsChildOf(owner)) return; // không dính chính mình
        var health = c.collider.GetComponentInParent<Health>();
        if (health) health.OnTakeDmg(damage);
        // Chỉ dính vào tag mong muốn (nếu có)
        if (!string.IsNullOrEmpty(stickOnlyOnTag) && !c.collider.CompareTag(stickOnlyOnTag))
        {
            // vẫn có thể gây damage rồi tự hủy nếu muốn:
            
            
            if (c.gameObject.CompareTag("Player")) //to do
            {
                UIManager.Instance.HPSlider.value = c.gameObject.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].value;
            }
            // Không dính → tự hủy sớm:
            StartCoroutine(DespawnAfter(0.05f));
            return;
        }

        hasHit = true;

        // Gây damage 1 lần
        
      
        Debug.Log("hoangpl " + c.collider.gameObject.GetInstanceID() + " my " + GetInstanceID() );
        // Lấy contact chính xác
        var contact = c.GetContact(0);
        StickTo(c.collider.transform, contact.point, contact.normal);
    }

    void StickTo(Transform target, Vector3 point, Vector3 normal)
    {
        // Tắt vật lý/va chạm
        if (rb)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        if (col) col.enabled = false;

        // Đặt đúng điểm chạm & xoay mũi vào mục tiêu (ngược normal)
        transform.position = point;
        transform.rotation = Quaternion.LookRotation(-normal, Vector3.up);

        // Parent vào xương "thân" nếu có, fallback là collider transform
        transform.SetParent(FindBone(target) ?? target, true);

        if (lifeCo != null) StopCoroutine(lifeCo);
        lifeCo = StartCoroutine(DespawnAfter(stickLifetime));
    }

    Transform FindBone(Transform root)
    {
        // Tìm vài xương phổ biến (nhanh, không rác GC)
        string[] names = { "Spine", "Chest", "UpperChest", "Hips", "Torso" };
        var ts = root.GetComponentsInChildren<Transform>();
        for (int i = 0; i < ts.Length; i++)
        {
            var nm = ts[i].name;
            for (int j = 0; j < names.Length; j++)
                if (nm.IndexOf(names[j], System.StringComparison.OrdinalIgnoreCase) >= 0)
                    return ts[i];
        }
        return null;
    }

    IEnumerator DespawnAfter(float t)
    {
        yield return new WaitForSeconds(t);

        // Dọn dẹp trước khi trả về pool
        transform.SetParent(null);
        if (col) //tach ham
        {
            col.enabled = true;
            if (ownerCols != null)
            {
                for (int i = 0; i < ownerCols.Length; i++)
                {
                    if (ownerCols[i] == null)
                    {
                        continue;
                    }
                    Physics.IgnoreCollision(col, ownerCols[i], false);
                }
            }
        }
            
        

        gameObject.SetActive(false);
    }
}


