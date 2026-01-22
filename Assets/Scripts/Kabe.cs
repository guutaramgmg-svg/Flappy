using UnityEngine;

public class Kabe : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    float leftLimit = -12f;
    KabePool pool;

    public void Init(KabePool pool)
    {
        this.pool = pool;
    }

    void Update()
    {
        if (pool == null) return;

        transform.position -= Vector3.right * speed * Time.deltaTime;

        if (transform.position.x < leftLimit)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
