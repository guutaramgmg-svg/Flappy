using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioClip[] BGMClips;

    [SerializeField] AudioSource audioSourceSE;
    [SerializeField] AudioClip[] SEClips;

    public bool isMute;

    public static SoundManager Instance { get; private set; }

    public enum BGM { BGM }
    public enum SE { 
        DAMAGE_SE,
        POINTUP_SE,
        ONARA_01_SE,
        ONARA_02_SE,
        ONARA_03_SE,
        ONARA_04_SE,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        PlayBGM(BGM.BGM);
    }

    public void PlayBGM(BGM bgm)
    {
        if (audioSourceBGM == null || BGMClips.Length <= (int)bgm) return;
        audioSourceBGM.clip = BGMClips[(int)bgm];
        audioSourceBGM.Play();
    }

    public void PlaySE(SE se)
    {
        if (audioSourceSE == null || SEClips.Length <= (int)se) return;
        audioSourceSE.PlayOneShot(SEClips[(int)se]);
    }


    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public void MuteBGM(bool isMute)
    {
        if (audioSourceBGM == null) return;
        audioSourceBGM.mute = isMute;
    }

    public void MuteSE(bool isMute)
    {
        if (audioSourceSE == null) return;
        audioSourceSE.mute = isMute;
    }

}
