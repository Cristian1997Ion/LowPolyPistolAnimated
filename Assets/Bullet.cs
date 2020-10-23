using UnityEngine;
using System.Collections;

// ----- Low Poly FPS Pack Free Version -----
public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How long does it take for the bullet objet to be destroyed")]
    private float lifespan = 5;
    [SerializeField]
    private bool destroyOnHit = true;
    [SerializeField]
    private float bulletForce = 0.1f;


    private Rigidbody rb;


    private void Start()
    {
        StartCoroutine(DestroyAfter());
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletForce;

    }



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("HIT:");
        Debug.Log(collision.collider.gameObject.name);
        if (destroyOnHit)
        {
            Destroy(gameObject);
            return;
        }

        rb.useGravity = true;

    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
    }
}