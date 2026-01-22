using UnityEngine;
using TMPro;
using DG.Tweening;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    public int score;
    Tween countTween;

    void Start()
    {
        scoreText.text = "0";
    }

    public void AddScore(int point)
    {
        int prevScore = score;
        score += point;

        // Šù‘¶Tween‚ðŽ~‚ß‚éi˜A‘±‰ÁŽZ‘Îôj
        countTween?.Kill();

        // ”Žš‚ªƒkƒ‹ƒb‚Æ‘‚¦‚é
        countTween = DOTween.To(
            () => prevScore,
            x => scoreText.text = x.ToString(),
            score,
            0.25f
        ).SetEase(Ease.OutCubic);

        // ƒ|ƒ“ƒb‚Æ’e‚Þ
        scoreText.transform.DOKill();
        scoreText.transform
            .DOPunchScale(Vector3.one * 0.25f, 0.25f, 1, 0.5f);
    }
}
