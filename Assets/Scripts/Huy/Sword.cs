using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 10f;
    //private bool hasHit = false;
    private const string playerTag = "Player";
    public float lifeAfterHit = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Swords hit object: {collision.gameObject.name} with tag: {collision.gameObject.tag}");
        //if (hasHit) return;
        if (collision.collider.CompareTag(playerTag))
        {
            collision.collider.GetComponent<Health>().OnTakeDmg(damage);
        }

        //hasHit = true;
        StartCoroutine(DisableAfterDelay(lifeAfterHit));
    }

    private IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);              
        gameObject.SetActive(false);   
    }
}
