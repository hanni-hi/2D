using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // �� ������ �ʿ��� ���ӽ����̽� �߰�

//��ŸƮ ȭ���� Ŭ���ϸ� ������������ �̵��ϴ� �ڵ��Դϴ�.
public class startpage : MonoBehaviour
{
    public AudioSource audioSource; // ����� �ҽ� ������Ʈ
    public AudioClip introClip; // ª�� ����� mp3 ����
    public AudioClip backgroundClip; // ��� ���� ����

    // void Update()
    // {
    //     // ȭ���� Ŭ���ϸ� ���� �������� ��ȯ
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         LoadMainScene();
    //     }
    // }
    //
    // private void LoadMainScene()
    // {
    //     // �� ��ȯ
    //     SceneManager.LoadScene("Main");
    // }

    void Start()
    {
        // AudioSource ������Ʈ�� ������ �߰�
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // ��� ���� ���
        PlayBackgroundMusic();
    }

    void Update()
    {
        // ȭ���� Ŭ���ϸ� ���� �������� ��ȯ
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
            audioSource.loop = true; // ��� ������ �ݺ� ����ϵ��� ����
            audioSource.Play();
        }
    }

    private IEnumerator PlayAudioAndLoadScene()
    {
        // ���� ��� ���� ��� ������ ����
        audioSource.Stop();

        // ª�� mp3 ���� ���
        if (introClip != null)
        {
            audioSource.clip = introClip;
            audioSource.loop = false; // �ݺ� ���� �� ���� ���
            audioSource.Play();

            // ª�� mp3 ���� ��� �ð� ���� ��ٸ�
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // �� ��ȯ
        SceneManager.LoadScene("Main");
    }

}

