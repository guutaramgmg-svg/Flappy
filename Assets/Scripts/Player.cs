using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Player : MonoBehaviour
{
    #region キャラクター画像関連
    [SerializeField] Sprite idleSprite;   
    [SerializeField] Sprite flapSprite;   
    SpriteRenderer spriteRenderer;
    #endregion

    #region キャラエフェクト関連
    [SerializeField] ParticleSystem fartEffect;
    [SerializeField] ParticleSystem explosionPrefab;
    #endregion

    [SerializeField] Score score;

    [SerializeField] float upSpeed;
    [SerializeField] float downSpeed;

    Rigidbody2D rb;

    #region プレイヤーの状態関連
    bool isGameStarted = false;
    bool isDead;
    #endregion

    #region 取得クラス
    [SerializeField] GameManager gameManager;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite;
        fartEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void Update()
    {
        if (!isGameStarted)
        {
            return;
        }
        if (isDead)
        {
            return;
        }
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame
            ||
            Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            rb.linearVelocity = new Vector2(0, upSpeed);
            Flap();
            fartEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            fartEffect.Play();
        }

        if (transform.position.y <= -14 || transform.position.y >= 14)
        {
            GameOver();
        }
    }

    void Flap()
    {
        int randomIndex = Random.Range(1, 5);
        SoundManager.SE randomSE = (SoundManager.SE)System.Enum.Parse(
        typeof(SoundManager.SE),
        $"ONARA_0{randomIndex}_SE"
        );

        SoundManager.Instance.PlaySE(randomSE);

        spriteRenderer.sprite = flapSprite;
        Invoke(nameof(BackToIdle), 0.15f); 
    }



    void BackToIdle()
    {
        if (isDead) return;
        spriteRenderer.sprite = idleSprite;
    }


    void GameOver()
    {
        gameManager.ShowGameOver();
        isDead = true;
    }

    void DieEffect()
    {
        transform.DOKill();
        spriteRenderer.DOKill();

        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;

        Vector2 explodeDir = Random.insideUnitCircle.normalized;

        Sequence seq = DOTween.Sequence();

        // 一瞬で膨張
        seq.Append(transform.DOScale(1.6f, 0.05f)
            .SetEase(Ease.OutExpo));

        // 爆散
        seq.Append(transform.DOMove(
            (Vector2)transform.position + explodeDir * 1.2f,
            0.2f).SetEase(Ease.OutCubic));

        seq.Join(transform.DORotate(
            new Vector3(0, 0, Random.Range(-360f, 360f)),
            0.2f,

            RotateMode.FastBeyond360));


        ParticleSystem ps = Instantiate(
        explosionPrefab,
        transform.position,
        Quaternion.identity
        );

        ps.Play();

        Destroy(ps.gameObject, ps.main.duration);
        // 消滅
        seq.Join(spriteRenderer.DOFade(0f, 0.05f));


        seq.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
        {
            return;
        }
        SoundManager.Instance.PlaySE(SoundManager.SE.DAMAGE_SE);
        DieEffect();
        GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }
        SoundManager.Instance.PlaySE(SoundManager.SE.POINTUP_SE);
        score.AddScore(1);
    }

    public void StartGame()
    {
        isGameStarted = true;
        rb.gravityScale = downSpeed;
    }
}
