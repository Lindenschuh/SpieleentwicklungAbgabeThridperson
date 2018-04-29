using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float lifeTime;
    public float explosionForce;
    public float explosionRadius;
    public float upModifier;
    public LayerMask GravityMask;
    public LayerMask BuildMask;
    public GameObject explosionEffect;
    public int BombDMG;

    // Use this for initialization
    private void Start()
    {
        Invoke("explode", lifeTime);
    }

    // Update is called once per frame
    private void explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hit in hitColliders)
        {
            if (((1 << hit.gameObject.layer) & GravityMask) != 0)
            {
                Rigidbody r = hit.GetComponent<Rigidbody>();
                if (r != null)
                {
                    r.AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier);
                }
            }
            else if (((1 << hit.gameObject.layer) & BuildMask) != 0)
            {
                var dmgAbleObj = hit.GetComponent<Destroyable>();
                if (dmgAbleObj != null)
                {
                    dmgAbleObj.GetDMG(BombDMG);
                }
            }
        }
        var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
        DestroyObject(effect, 2f);
    }
}