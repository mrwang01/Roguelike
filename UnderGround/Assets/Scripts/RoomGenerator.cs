﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{

    public enum Direction { up, down, left, right };
    public Direction direction;

    [Header("房間資訊")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;
    private GameObject endRoom;

    [Header("座標位置")]
    public Transform generatorPoint;
    public float xoffset;
    public float yoffset;
    public LayerMask roomLayer;

    public int maxStep;

    public List<Room> rooms = new List<Room>();
 
    List<GameObject> farRooms = new List<GameObject>();

    List<GameObject> lessFarRooms = new List<GameObject>(); 

    List<GameObject> oneWayRooms = new List<GameObject>(); 

    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
        rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());
        //改變 point 位置

        ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        
        endRoom = rooms[0].gameObject;
        
        
        //final room

        foreach (var room in rooms)
        {
        //     if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)

        //     {
        //         endRoom = room.gameObject;
        //     }    
        SetupRoom(room , room.transform.position);        
         }
        FindEndRoom();
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }
    }
    public void ChangePointPos()
    {
        do{
        direction = (Direction)Random.Range(0,4);

        switch(direction)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0,yoffset,0);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0,-yoffset,0);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xoffset,0,0);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xoffset,0,0);
                break;
        }
        }while(Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));


    }
    public void SetupRoom(Room newRoom, Vector3 roomPosition )
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0,yoffset,0),0.2f,roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0,-yoffset,0),0.2f,roomLayer);
        newRoom.roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xoffset,0,0),0.2f,roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xoffset,0,0),0.2f,roomLayer);

        newRoom.UpdateRoom();
    }

    public void FindEndRoom()
    {
         //最遠房間數字
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //最遠房間跟第二遠的
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if (room.stepToStart == maxStep - 1)
                lessFarRooms.Add(room.gameObject);
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);//最遠單一出口
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);//第二遠單一出口
        }

        if (oneWayRooms.Count != 0)
        //超過一間單一出口
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }

    }
    

}
