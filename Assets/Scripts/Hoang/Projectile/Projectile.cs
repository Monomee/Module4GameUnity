using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float lifetime = 5f;
    private float speed = 10f; // Adjust speed as needed
    public void Initialize(Vector3 direction, Transform startPosition)
    {
        this.direction = direction.normalized;
        transform.position = startPosition.position + Vector3.up;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    void OnEnable()
    {
        Invoke("Deactivate", lifetime);
    }
    void Deactivate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
