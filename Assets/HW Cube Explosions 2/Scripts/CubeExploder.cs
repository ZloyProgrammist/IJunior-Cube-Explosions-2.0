using System.Collections.Generic;
using UnityEngine;

public class CubeExploder : MonoBehaviour
{
    [SerializeField] private float _referenceSize = 1;
    [SerializeField] private float _minSize = 0.05f;

    public void Explode(SplitableCube cube, float force, float radius)
    {
        float size = Mathf.Max(cube.Size, _minSize);
        float scale = _referenceSize / size;
        float explosionForce = force * scale;
        float explosionRadius = radius * scale;
        Vector3 explosionPosition = cube.transform.position;

        foreach (Rigidbody explodableObject in GetExplodableObjects(explosionPosition, explosionRadius))
        {
            explodableObject.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }

    public void ApplySplitForce(SplitableCube cube, Vector3 direction, float force)
    {
        if (cube.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position, float radius)
    {
        Collider[] hits = Physics.OverlapSphere(position, radius);

        List<Rigidbody> objects = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                objects.Add(hit.attachedRigidbody);

        return objects;
    }
}
