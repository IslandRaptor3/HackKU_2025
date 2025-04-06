using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float lifetime = 8f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }


}
