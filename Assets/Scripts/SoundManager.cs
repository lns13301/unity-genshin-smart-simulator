using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private Sound[] sound;
    private Sound[] effectSound;
    public int mapCode;
    public bool isMapChanged;
    public static SoundManager instance;

    public int timer;

    private void Start()
    {
        instance = this;

        sound = new Sound[transform.GetChild(0).childCount];
        effectSound = new Sound[transform.GetChild(1).childCount];

        // 음악 등록
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            sound[i] = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Sound>();
        }

        // 효과음 등록
        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            effectSound[i] = transform.GetChild(1).GetChild(i).gameObject.GetComponent<Sound>();
        }
    }

    public void PlayMusic(int index)
    {
        StopAllSounds();
        sound[index].PlaySound();
    }

    public void PlayEffectSound(int index)
    {
        effectSound[index].PlaySound();
    }

    public void PlayOneShotEffectSound(int index)
    {
        effectSound[index].StopSound();
        effectSound[index].PlaySound();
    }

    public void StopSound(int index)
    {
        sound[index].StopSound();
    }

    public void StopEffectSound(int index)
    {
        effectSound[index].StopSound();
    }

    public void RefreshSounds()
    {
        CancelInvoke();
        StopAllSounds();
        isMapChanged = true;
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            sound[i].StopSound();
        }
    }

    public void PlayButtonEffectSound()
    {
        PlayEffectSound(0);
    }
}
