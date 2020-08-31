using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public LayerMask roomLayer;

    public List<GameObject> rooms = new List<GameObject>();

    
    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
        rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));
        //改變 point 位置

        ChangePointPos();
        }
        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        rooms[roomNumber-1].GetComponent<SpriteRenderer>().color = endColor;

        
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

}
