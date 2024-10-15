using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 관리에 필요한 네임스페이스 추가

//스타트 화면을 클릭하면 메인페이지로 이동하는 코드입니다.
public class startpage : MonoBehaviour
{
    public AudioSource audioSource; // 오디오 소스 컴포넌트
    public AudioClip introClip; // 짧게 재생할 mp3 파일
    public AudioClip backgroundClip; // 배경 음악 파일

    // void Update()
    // {
    //     // 화면을 클릭하면 메인 페이지로 전환
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         LoadMainScene();
    //     }
    // }
    //
    // private void LoadMainScene()
    // {
    //     // 씬 전환
    //     SceneManager.LoadScene("Main");
    // }

    void Start()
    {
        // AudioSource 컴포넌트가 없으면 추가
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 배경 음악 재생
        PlayBackgroundMusic();
    }

    void Update()
    {
        // 화면을 클릭하면 메인 페이지로 전환
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(PlayAudioAndLoadScene());
        }
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundClip != null)
        {
            audioSource.clip = backgroundClip;
            audioSource.loop = true; // 배경 음악을 반복 재생하도록 설정
            audioSource.Play();
        }
    }

    private IEnumerator PlayAudioAndLoadScene()
    {
        // 현재 재생 중인 배경 음악을 중지
        audioSource.Stop();

        // 짧은 mp3 파일 재생
        if (introClip != null)
        {
            audioSource.clip = introClip;
            audioSource.loop = false; // 반복 없이 한 번만 재생
            audioSource.Play();

            // 짧은 mp3 파일 재생 시간 동안 기다림
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // 씬 전환
        SceneManager.LoadScene("Main");
    }

}

