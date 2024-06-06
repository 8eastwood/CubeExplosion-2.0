using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _defaultExplosionRadius = 5f;
    [SerializeField] private float _defaultExplosionForce = 150f;
    [SerializeField] private ParticleSystem _effect;

    private float _averageValue;
    private int _numberOfSides = 3;
    private float _explosionForce;
    private float _explosionRadius;

    private void OnMouseDown()
    {
        Explode();
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(SetExplosionForce(explodableObject), transform.position, SetExplosionRadius(explodableObject));
        }

        Destroy(gameObject);
    }

    private float CalculateAverageCoefficient()
    {
        _averageValue = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / _numberOfSides;

        return _averageValue;
    }

    private float SetExplosionForce(Rigidbody gameObject)
    {
        _explosionForce = _defaultExplosionForce / CalculateAverageCoefficient();

        return _explosionForce;
    }

    private float SetExplosionRadius(Rigidbody gameObject)
    {
        _explosionRadius = _defaultExplosionRadius / CalculateAverageCoefficient();

        return _explosionRadius;
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
