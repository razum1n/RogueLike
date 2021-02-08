using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor, currentColor;
    public int distanceToEnd;
    public Transform generatorPoint;
    public float xOffset = 18f;
    public float yOffset = 10f;
    public LayerMask whatIsRoom;
    public enum Direction { up, right, down, left };
    public Direction selectedDirection;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();
    private List<GameObject> generatedOutlines = new List<GameObject>();
    private GameObject endRoom;

    public RoomPrefabs rooms;

    public RoomCenter centerStart, centerEnd;
    public RoomCenter[] potentialCenters;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

            layoutRoomObjects.Add(newRoom);

            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }
        CreateRoomOutline(Vector3.zero);
        foreach(GameObject room in layoutRoomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);

        foreach(GameObject outline in generatedOutlines)
        {
            bool generateCenter = true;

            if(outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).room = outline.GetComponent<Room>();
                generateCenter = false;
            }

            if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).room = outline.GetComponent<Room>();
                generateCenter = false;
            }

            if(generateCenter)
            {
                int centerSelected = Random.Range(0, potentialCenters.Length);

                Instantiate(potentialCenters[centerSelected], outline.transform.position, transform.rotation).room = outline.GetComponent<Room>();
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    public void MoveGenerationPoint()
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
        }

    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);

        #region directionCount Generation
        int directionCount =0;
        if (roomAbove)
            directionCount++;
        if (roomBelow)
            directionCount++;
        if (roomRight)
            directionCount++;
        if (roomLeft)
            directionCount++;
        #endregion

        switch (directionCount)
        {
            case 0:
                Debug.LogError("Found no room exits");
                break;
            case 1:
                if (roomAbove)
                    generatedOutlines.Add(Instantiate(rooms.singleUp,roomPosition,transform.rotation));
                if (roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                if (roomRight)
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                if (roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                break;
            case 2:
                if (roomAbove && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                if (roomLeft && roomAbove)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                if (roomLeft && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                if (roomLeft && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftDown, roomPosition, transform.rotation));
                if (roomRight && roomBelow)
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                if (roomAbove && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.doubleRightUp, roomPosition, transform.rotation));
                break;
            case 3:
                if (roomAbove && roomBelow && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftDownUp, roomPosition, transform.rotation));
                if (roomAbove && roomBelow && roomRight)
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownUp, roomPosition, transform.rotation));
                if (roomAbove && roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftRightUp, roomPosition, transform.rotation));
                if (roomBelow && roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                break;
            case 4:
                if(roomAbove && roomBelow && roomRight && roomLeft)
                    generatedOutlines.Add(Instantiate(rooms.fourway, roomPosition, transform.rotation));
                break;
        }


    }
}
[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleLeft, singleRight, singleUp, singleDown,
        doubleLeftUp,doubleLeftRight,doubleLeftDown,doubleRightDown,doubleRightUp,doubleUpDown,
        tripleRightDownUp,tripleRightDownLeft,tripleLeftDownUp,tripleLeftRightUp,
        fourway;
}