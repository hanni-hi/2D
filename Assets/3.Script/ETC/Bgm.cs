using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Bgm : MonoBehaviour
{
    //bgm�� ����ؼ� ����ǵ��� �ϴ� �ڵ��Դϴ�.

    public AudioSource audiosource;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        if (audiosource != null)
        {
            audiosource.Play();
        }
    }

    public void PlayBGM()
    {
        if (audiosource != null && !audiosource.isPlaying)
        {
            audiosource.Play();
        }
    }

    public void StopBGM()
    {
        if (audiosource != null && audiosource.isPlaying)
        {
            audiosource.Stop();
        }
    }

    public void SetVolume(float volume)
    {
        if (audiosource != null)
        {
            audiosource.volume = volume;
        }
    }
}
