using UnityEngine;

public class DestroyExplosion : MonoBehaviour
{
    [SerializeField] private float destroyAfter;
    private void FixedUpdate()
    {
        Destroy(gameObject, destroyAfter);
    }
}
