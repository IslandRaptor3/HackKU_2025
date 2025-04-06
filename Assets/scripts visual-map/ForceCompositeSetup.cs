using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(CompositeCollider2D))]
public class ForceCompositeSetup : MonoBehaviour
{
    void Reset()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

        var poly = GetComponent<PolygonCollider2D>();
        poly.compositeOperation = Collider2D.CompositeOperation.Merge;

        Debug.Log("Composite Collider setup complete.");
    }
}
