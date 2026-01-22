using UnityEngine;

public class MuteBtn : MonoBehaviour
{

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); SoundManager.Instance.MuteBGM(SoundManager.Instance.isMute);
        SetMute(SoundManager.Instance.isMute);
    }

    public void OnMuteBtn()
    {
        ChangeMute(SoundManager.Instance.isMute);

        SetMute(SoundManager.Instance.isMute);
    }

    private void ChangeMute(bool isMute)
    {
        if (isMute)
        {
            SoundManager.Instance.isMute = false;
        }
        else
        {
            SoundManager.Instance.isMute = true;
        }
    }


    private void SetMute(bool isMute)
    {
        animator.SetBool("isMute", isMute);
        SoundManager.Instance.MuteBGM(isMute);
        SoundManager.Instance.MuteSE(isMute);


    }

}
