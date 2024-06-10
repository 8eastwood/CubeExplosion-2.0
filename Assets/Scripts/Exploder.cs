using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _defaultExplosionRadius = 5f;
    [SerializeField] private float _defaultExplosionForce = 150f;
    [SerializeField] private ParticleSystem _effect;

    private const int _amountOfLocalScaleAxis = 3;

    public void ExplodeObject(GameObject gameObject)
    {
        Explode(gameObject);
    }

    private void Explode(GameObject gameObject)
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(GetExplosionForce(explodableObject), transform.position, GetExplosionRadius(explodableObject));
        }
    }

    private float CalculateAverageCoefficient()
    {
        return (transform.localScale.x + transform.localScale.y + transform.localScale.z) / _amountOfLocalScaleAxis;
    }

    private float GetExplosionForce(Rigidbody gameObject)
    {
        return _defaultExplosionForce / CalculateAverageCoefficient();
    }

    private float GetExplosionRadius(Rigidbody gameObject)
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
