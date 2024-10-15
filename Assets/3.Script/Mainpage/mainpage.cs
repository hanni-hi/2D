using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainpage : MonoBehaviour
{
    [SerializeField] private Sprite[] wallpapers;
    [SerializeField] private GameObject wallpaperPrefab;

    // Start is called before the first frame update
    //�������� ���� �������� �����ϰ� �ֽ��ϴ�.
    void Start()
    {
        generateRandomWallpaper();
    }

    private void generateRandomWallpaper()
    {
        if(wallpapers.Length>0)
        {
            int randomIndex = Random.Range(0, wallpapers.Length);
            Sprite selectedWallpaper = wallpapers[randomIndex];

            GameObject wallpaperObject = Instantiate(wallpaperPrefab, Vector3.zero, Quaternion.identity);
            wallpaperObject.GetComponent<SpriteRenderer>().sprite = selectedWallpaper;
        }
    }
}
