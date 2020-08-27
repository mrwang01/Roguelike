using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{

    public enum Direction { up, down, left, right };
    public Direction direction;




    [Header("房間數量")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor, endColor;

    [Header("座標位置")]
    public Transform generatorPoint;
    public float xoffset;
    public float yoffset;
    public List<GameObject> rooms = new List<GameObject>();

    
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
        rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));
        //改變 point 位置

        ChangePointPos();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePointPos()
    {
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

    }

}
