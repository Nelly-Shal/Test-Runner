using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    public GameObject partPref;
    private List<GameObject> parts = new List<GameObject>();
    private float maxSpeed = 15;
    private float speed = 0;
    private int maxPartCount = 5;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }

    void Update()
    {
        if (speed == 0) return;
        foreach (GameObject part in parts)

        {
            part.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }
        if (parts[0].transform.position.z < -50)
        {
            Destroy(parts[0]);
            parts.RemoveAt(0);

            CreateNextPart();
        }

    }

    public void StartLevel()
    {
        speed = maxSpeed;
        //SwipeManager.instance.enabled = true;

    }
    public void ResetLevel()
    {
        speed = 0;
        while (parts.Count>0)
        {
            Destroy(parts[0]);
            parts.RemoveAt(0);
        }
        for (int i=0;i<maxPartCount;i++)
        {
            CreateNextPart();
            //SwipeManager.instance.enabled = false;
        }
    }   
    public void CreateNextPart()
    {
        Vector3 pos = Vector3.zero;
        if (parts.Count > 0) { pos = parts[parts.Count - 1].transform.position + new Vector3(0, 0, 45); }
        GameObject go = Instantiate(partPref, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        parts.Add(go);

    }
}
