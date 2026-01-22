using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KabePool : MonoBehaviour
{
    [SerializeField] GameObject kabePrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] float spawnInterval = 2f; // 壁の生成間隔

    Queue<GameObject> pool = new Queue<GameObject>();
    Coroutine spawnCoroutine;

    // ゲーム開始時に呼ぶ
    void OnEnable()
    {
        // プール生成（初回のみ）
        if (pool.Count == 0)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(kabePrefab, transform);
                obj.SetActive(false);
                obj.GetComponent<Kabe>().Init(this);
                pool.Enqueue(obj);
            }
        }

        // コルーチンで定期生成開始
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    // コルーチン
    IEnumerator SpawnRoutine()
    {
        // 最初の壁は少し遅らせる
        yield return new WaitForSeconds(1f);

        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void Spawn()
    {
        if (pool.Count == 0) return;

        GameObject obj = pool.Dequeue();
        obj.transform.position = new Vector3(10f, RandomY(), 0);
        obj.SetActive(true);
    }

    // Kabe から呼ばれる
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    float RandomY()
    {
        //        return Random.value < 0.5f ? 6.0f : -6.0f;

        return Random.Range(-5f, 5f);
    }

    // ゲームオーバー時に呼ぶと生成を停止できる
    public void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
