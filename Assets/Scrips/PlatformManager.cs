using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class PlatformManager : MonoSingleton<PlatformManager>
{
    public GameObject PlatformPrefab;
    public int InitPlatformCount = 20;
    public float PlatformSpacingZ = 3f;
    public float PlatformRangeX = 3f;

    private Queue<GameObject> platformPool = new Queue<GameObject>();
    private List<GameObject> activatePlatformList = new List<GameObject>();
    private Vector3 nextPlatformPosition;
    public void Init()
    {
        for (int i = 0; i < InitPlatformCount; i++)
        {
            GameObject platform = GetPlatform();
            platform.transform.position = nextPlatformPosition;
            UpdateNextPos(i > 3); //Won't change pos X for 3 first platforms
            platform.SetActive(true);
        }
    }
    private GameObject GetPlatform()
    {
        GameObject platform;
        if (platformPool.Count > 0)
        {
            platform = platformPool.Dequeue();
        }
        else
        {
            platform = Instantiate(PlatformPrefab);
            platform.SetActive(false);
        }
        activatePlatformList.Add(platform);
        return platform;
    }
    private void UpdateNextPos(bool isRandomX)
    {
        float randomX = isRandomX ? Random.Range(-PlatformRangeX, PlatformRangeX) : 0;
        nextPlatformPosition = new Vector3(randomX, 0, nextPlatformPosition.z + PlatformSpacingZ);
    }

    public void RecyclePlatform(GameObject platform)
    {
        platform.SetActive(false);
        activatePlatformList.Remove(platform);
        platformPool.Enqueue(platform);
        GenerateNextPlatform();
    }
    public void GenerateNextPlatform()
    {
        nextPlatformPosition = activatePlatformList.Last().transform.position;
        UpdateNextPos(true);
        GameObject platform = GetPlatform();
        platform.transform.position = nextPlatformPosition;
        platform.SetActive(true);
    }
}
