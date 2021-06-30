using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // 0 - LR; 1 - LRB; 2 - LRT; 3 - LRTB
    public float moveAmount;
    public float startTimeBtwRoom = 0.25f;
    public float minX;
    public float maxX;
    public float minY;
    public LayerMask room;
    [FormerlySerializedAs("StopGen")] public bool stopGen;
    
    private float TimeBtwRoom;
    private int Direction;
    private int DownCounter;

    private void Start()
    {
        int randStartPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartPos].position;

        int randRoom = Random.Range(0, rooms.Length);
        Instantiate(rooms[randRoom], transform.position, Quaternion.identity);

        Direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (TimeBtwRoom <= 0 && !stopGen)
        {
            Move();
            TimeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            TimeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        if (Direction == 1 || Direction == 2) //Right
        {
            DownCounter = 0;
            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;
                
                int randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, quaternion.identity);
                
                Direction = Random.Range(1, 6);
                if (Direction == 3)
                {
                    Direction = 2;
                }else if (Direction == 4)
                {
                    Direction = 5;
                }
            }
            else
            {
                Direction = 5;
            }

        } else if (Direction == 3 || Direction == 4) // Left
        {
            DownCounter = 0;
            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;
                
                int randRoom = Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], transform.position, quaternion.identity);
                
                Direction = Random.Range(3, 6);
            }
            else
            {
                Direction = 5;
            }
            
        } else if (Direction == 5)// Down
        {
            DownCounter++;
            
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
            if (roomDetection.GetComponent<RoomType>().roomType != 1 && roomDetection.GetComponent<RoomType>().roomType != 3)
            {
                if (DownCounter >= 2)
                {
                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                    Instantiate(rooms[3], transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.Log(DownCounter);
                    roomDetection.GetComponent<RoomType>().RoomDestruction();

                    int randBottomRoom = Random.Range(1, 4);
                    if (randBottomRoom == 2)
                    {
                        randBottomRoom = 1;
                    }

                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                }
            }
            
            if (transform.position.y > minY)
            {
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;
                
                int randRoom = Random.Range(2, 4);
                Instantiate(rooms[randRoom], transform.position, quaternion.identity);
                
                Direction = Random.Range(1, 6);
            }
            else
            {
                stopGen = true;
            }
        }
    }
}
