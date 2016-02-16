﻿using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    public Object TankPrefap;
    public Sprite[] BombSprites;
    public Object[] BombExplosions;
    public Object RocketEffect;
    public Object BombCollisionEffect;
    public Object AirstrikePrefap;
    public ArrayList CustomPositionX = new ArrayList();

    public void Spawn(int Count ,string playername, int playerrand, int playerscore)
    {
        float eachX = (CameraWidth / Count );
        float lastX = (-CameraWidth * .5f) - (eachX - 1f);

        for (int i = 0; i < Count; i++)
        {
            float x = (float)CustomPositionX.Count == 0f ? lastX += eachX + 1 : (float)CustomPositionX[i];
            float y = CalculateY(x);


            GameObject obj = Instantiate(TankPrefap, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
            SetPropertiesToTank(obj, i, playername, playerrand, playerscore);
        }
    }

    private void SetPropertiesToTank(GameObject obj, int counter, string playername, int playerrand, int playerscore)
    {
        Color color = ColorRandom.GetRandomColors(counter);
        obj.tag = "Player";
        Tank tData = obj.GetComponent<Tank>();
        tData.Color = tData.OrginalColor = color;
        tData.CanDisabled = true;
        tData.Health = 100;
        tData.Strength = 100;
        tData.Oil = 500;
        tData.PlayerName = "Mohammed";
        tData.PlayerRank = playerrand;
        tData.PlayerScore = playerscore;
        obj.GetComponent<Rigidbody>().centerOfMass = new Vector3(0f, -0.5f, 0);
    }

    private float CalculateY(float x)
    {
        RaycastHit info;
        Ray ray = new Ray(new Vector3(x, 15, 0), Vector3.down);
        bool hit = Physics.Raycast(ray, out info);

        if (hit)
            return info.point.y + 0.5f;
        else
            return 6;
    }


    public static float CameraWidth
    {
        get
        {
            Camera cam = Camera.main;
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            return width;
        }
    }


}
