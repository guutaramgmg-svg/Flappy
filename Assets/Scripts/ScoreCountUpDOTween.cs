using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreCountUpDOTween : MonoBehaviour
{
    TextMeshProUGUI text;
    int currentScore;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int newScore)
    {
        DOTween.To(
            () => currentScore,
            x =>
            {
                currentScore = x;
                text.text = currentScore.ToString();
            },
            newScore,
            0.3f
        ).SetEase(Ease.OutQuad);
    }
}
