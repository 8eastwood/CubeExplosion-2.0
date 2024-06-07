using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _defaultExplosionRadius = 5f;
    [SerializeField] private float _defaultExplosionForce = 150f;
    [SerializeField] private ParticleSystem _effect;

    private int _numberOfSides = 3;

    public void GetExplode(GameObject gameObject)
    {
        Explode(gameObject);
    }

    private void Explode(GameObject gameObject)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(SetExplosionForce(explodableObject), transform.position, SetExplosionRadius(explodableObject));
        }
    }

    private float CalculateAverageCoefficient()
    {
        return (transform.localScale.x + transform.localScale.y + transform.localScale.z) / _numberOfSides;
    }

    private float SetExplosionForce(Rigidbody gameObject)
    {
        return _defaultExplosionForce / CalculateAverageCoefficient();
    }

    private float SetExplosionRadius(Rigidbody gameObject)
    {
        return _defaultExplosionRadius / CalculateAverageCoefficient();
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _defaultExplosionRadius);

        List<Rigidbody> objectsToExplode = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objectsToExplode.Add(hit.attachedRigidbody);
            }
        }

        return objectsToExplode;
    }
}
