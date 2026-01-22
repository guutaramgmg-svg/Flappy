using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField] Transform[] backgrounds; // 3枚の背景
    [SerializeField] float speed = 2f;        // スクロール速度
    [SerializeField] float width = 20f;       // 背景1枚の横幅

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // 左にスクロール
            backgrounds[i].position += Vector3.left * speed * Time.deltaTime;

            // 左端を超えたら一番右に移動
            if (backgrounds[i].position.x <= -width)
            {
                Transform rightMost = GetRightMostBackground();
                backgrounds[i].position = new Vector3(rightMost.position.x + width, backgrounds[i].position.y, backgrounds[i].position.z);
            }
        }
    }

    // 一番右にある背景を返す
    Transform GetRightMostBackground()
    {
        Transform rightMost = backgrounds[0];
        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].position.x > rightMost.position.x)
                rightMost = backgrounds[i];
        }
        return rightMost;
    }
}
