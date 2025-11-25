using UnityEngine;

public class MageFireball : MonoBehaviour
{
    public float lifeTime = 3f;
    public float damage = 15f;

    private const string playerTag = "Player";


    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Fireball hit object: {other.name} with tag: {other.tag}");
        if (other.CompareTag(playerTag))
        {
            other.GetComponent<Health>()?.OnTakeDmg(damage);
            Deactivate();
            UIManager.Instance.HPSlider.value = other.GetComponent<UnitBase>().roleStat.dictStats[StatType.HP].value;
            Debug.Log($"Damage applied to {other.name}: {damage}");
        }

        // If you want the fire ball to disappear when it touched walls or ground 
        // if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) Deactivate();
    }

    private void Deactivate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}
