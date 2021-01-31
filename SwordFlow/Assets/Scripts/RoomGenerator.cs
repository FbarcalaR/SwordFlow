using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int numberOfCellsWide;
    public int numberOfCellsTall;
    public GameObject[] roomsWithRightDownExits;
    public GameObject[] roomsWithLeftRightDownExits;
    public GameObject[] roomsWithLeftDownExits;

    public GameObject[] roomsWithUpRightDownExits;
    public GameObject[] roomsWithLeftUpRightDownExits;
    public GameObject[] roomsWithLeftUpDownExits;

    public GameObject[] roomsWithUpRightExits;
    public GameObject[] roomsWithLeftUpRightExits;
    public GameObject[] roomsWithLeftUpExits;

    public GameObject[] roomsWithLeftExits;
    public GameObject[] roomsWithUpExits;
    public GameObject[] roomsWithRightExits;
    public GameObject[] roomsWithDownExits;

    public GameObject[] roomsWithLeftRightExits;
    public GameObject[] roomsWithUpDownExits;

    public GameObject[] roomsWithoutExits;


    public int maxRooms;                    //the max number of rooms to instantiate
    public int minRooms;                    //the min room to instantiate

    private float roomXLenght;              //the x size of the room
    private float roomYLenght;              //the y size of the room
    private bool[,] positions;              //the matrix with the positions where the rooms have to be instantiated
    private List<int[]> instantiatedRoomsPositions;  //List with the matrix positions where the rooms had been instantiated
    private List<GameObject> instantiatedRooms;
    private int roomsNumber;                //the final number of rooms

    void Start()
    {
        //get sprite x size and y size from the room prefab
        GameObject centralRoomPrefab = GetRandomRoomFromList(roomsWithLeftUpRightDownExits);
        roomXLenght = centralRoomPrefab.GetComponentInChildren<Grid>().cellSize.x * numberOfCellsWide;
        roomYLenght = centralRoomPrefab.GetComponentInChildren<Grid>().cellSize.y * numberOfCellsTall;

        //set the number of rooms randomly between bounds
        roomsNumber = Random.Range(minRooms, maxRooms);
        //instantiate List and matrix
        instantiatedRoomsPositions = new List<int[]>();
        positions = new bool[roomsNumber * 2 + 1, roomsNumber * 2 + 1];

        //Initialize matrix of positions
        for (int i = 0; i < positions.GetLength(0); i++)
            for (int j = 0; j < positions.GetLength(1); j++)
                positions[i, j] = false;

        //set the central room in the matrix and the List and instantiate it in game
        positions[roomsNumber, roomsNumber] = true;
        int[] centralRoom = { roomsNumber, roomsNumber };
        instantiatedRoomsPositions.Add(centralRoom);

        setPositionsMatrix();
        instantiatedRooms = new List<GameObject>();
        InstantiateRooms();
        SpawnLevelEnd(instantiatedRooms);
    }

    //Once the central room is set in Start, this function will instantiate the rest of the rooms.
    //the function selects randomly a room from the instantiated rooms list and, randomly, selects
    //an adjacent position to it. If the selected position fits boundaries, the the matrix and 
    //List will be updated with the new room and the room will be instantiated in the game.
    private void setPositionsMatrix()
    {
        while (instantiatedRoomsPositions.Count < roomsNumber)
        {
            int[] position = getPositionNoNeighbour();

            positions[position[0], position[1]] = true;
            instantiatedRoomsPositions.Add(position);
        }
    }

    private GameObject GetRandomRoomFromList(GameObject[] roomList)
    {
        int roomIndex = Random.Range(0, roomList.Length);
        var roomPrefab = roomList[roomIndex];
        return roomPrefab;
    }

    private void InstantiateRooms()
    {
        for (int i = 0; i < positions.GetLength(0); i++)
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                int[] thisRoom = { i, j };
                if (positions[i, j])
                {
                    GameObject roomToInstatiatePrefab;

                    switch (GetRoomType(thisRoom))
                    {
                        case RoomType.roomsWithRightDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithRightDownExits);
                            break;
                        case RoomType.roomsWithLeftRightDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftRightDownExits);
                            break;
                        case RoomType.roomsWithLeftDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftDownExits);
                            break;

                        case RoomType.roomsWithUpRightDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithUpRightDownExits);
                            break;
                        case RoomType.roomsWithLeftUpRightDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftUpRightDownExits);
                            break;
                        case RoomType.roomsWithLeftUpDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftUpDownExits);
                            break;

                        case RoomType.roomsWithUpRightExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithUpRightExits);
                            break;
                        case RoomType.roomsWithLeftUpRightExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftUpRightExits);
                            break;
                        case RoomType.roomsWithLeftUpExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftUpExits);
                            break;

                        case RoomType.roomsWithLeftRightExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftRightExits);
                            break;
                        case RoomType.roomsWithUpDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithUpDownExits);
                            break;

                        case RoomType.roomsWithLeftExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftExits);
                            break;
                        case RoomType.roomsWithUpExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithUpExits);
                            break;
                        case RoomType.roomsWithRightExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithRightExits);
                            break;
                        case RoomType.roomsWithDownExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithDownExits);
                            break;

                        case RoomType.roomsWithoutExits:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithoutExits);
                            break;

                        default:
                            roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithLeftUpRightDownExits);
                            break;
                    }

                    Vector3 instantiatePosition = new Vector3((thisRoom[0] - roomsNumber) * roomXLenght, -(thisRoom[1] - roomsNumber) * roomYLenght, 0);
                    instantiatedRooms.Add(Instantiate(roomToInstatiatePrefab, instantiatePosition, Quaternion.identity));
                }
                else if (!positions[i, j] && numberOfNeighbours(thisRoom) > 0)
                {
                    var roomToInstatiatePrefab = GetRandomRoomFromList(roomsWithoutExits);
                    Vector3 instantiatePosition = new Vector3((thisRoom[0] - roomsNumber) * roomXLenght, -(thisRoom[1] - roomsNumber) * roomYLenght, 0);
                    instantiatedRooms.Add(Instantiate(roomToInstatiatePrefab, instantiatePosition, Quaternion.identity));
                }
            }
    }

    private void SpawnLevelEnd(List<GameObject> instantiatedRooms)
    {
        var roomsWithEndLevelPoint = instantiatedRooms.Where(iR => iR.GetComponentInChildren<EndSpawnPoint>()).ToArray();
        int index = Random.Range(0, roomsWithEndLevelPoint.Count());

        var selectedRoom = roomsWithEndLevelPoint[index].GetComponentInChildren<EndSpawnPoint>();
        selectedRoom.SpawnLevelEnd();
    }

    //this function returns a valid position that only has one neighbour and has not been instantiated
    private int[] getPositionNoNeighbour()
    {
        List<int[]> posiblePositions = new List<int[]>();

        //all the instantiated rooms are checked
        foreach (int[] roomPosition in instantiatedRoomsPositions)
        {
            //for every instantiated room,
            //the surrounding positions (no corners) are checked.
            //if one sourronding position is not instantiated and has only one neighbour (which will be the already instantiated position)
            //then this sourronding position is added to the list of posible positions
            int x = roomPosition[0];
            int y = roomPosition[1];
            if (positionNotInstantiatedAndHasOneNeighbour(x, y - 1))
                posiblePositions.Add(new int[] { x, y - 1 });
            if (positionNotInstantiatedAndHasOneNeighbour(x - 1, y))
                posiblePositions.Add(new int[] { x - 1, y });
            if (positionNotInstantiatedAndHasOneNeighbour(x + 1, y))
                posiblePositions.Add(new int[] { x + 1, y });
            if (positionNotInstantiatedAndHasOneNeighbour(x, y + 1))
                posiblePositions.Add(new int[] { x, y + 1 });
        }

        //once all instantiated positions have been checked, then a random room from the list is returned
        return posiblePositions[Random.Range(0, posiblePositions.Count)];
    }

    //checks if a given room has a top neighbour
    private bool hasUpNeighbour(int[] room)
    {
        if (room[1] - 1 < 0) return false;
        return positions[room[0], room[1] - 1];
    }

    //checks if a given room has a left neighbour
    private bool hasLeftNeighbour(int[] room)
    {
        if (room[0] - 1 < 0) return false;
        return positions[room[0] - 1, room[1]];
    }

    //checks if a given room has a right neighbour
    private bool hasRightNeighbour(int[] room)
    {
        if (room[0] + 1 >= positions.GetLength(0)) return false;
        return positions[room[0] + 1, room[1]];
    }

    //checks if a given room has a bot neighbour
    private bool hasDownNeighbour(int[] room)
    {
        if (room[1] + 1 >= positions.GetLength(1)) return false;
        return positions[room[0], room[1] + 1];
    }

    //returns the number of instantiated neighbours a room has
    private int numberOfNeighbours(int[] room)
    {
        int numberOfNeighbours = 0;
        if (hasUpNeighbour(room))
            numberOfNeighbours++;
        if (hasDownNeighbour(room))
            numberOfNeighbours++;
        if (hasLeftNeighbour(room))
            numberOfNeighbours++;
        if (hasRightNeighbour(room))
            numberOfNeighbours++;
        return numberOfNeighbours;
    }

    //checks if a position has been instantiated and if has just one neighbour
    private bool positionNotInstantiatedAndHasOneNeighbour(int x, int y)
    {
        int[] room = { x, y };
        return !positions[room[0], room[1]] && numberOfNeighbours(room) == 1;
    }

    private RoomType GetRoomType(int[] room)
    {
        if (hasLeftNeighbour(room) && hasUpNeighbour(room) && hasRightNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithLeftUpRightDownExits;
        }

        if (hasUpNeighbour(room) && hasRightNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithUpRightDownExits;
        }
        if (hasLeftNeighbour(room) && hasRightNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithLeftRightDownExits;
        }
        if (hasLeftNeighbour(room) && hasUpNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithLeftUpDownExits;
        }
        if (hasLeftNeighbour(room) && hasUpNeighbour(room) && hasRightNeighbour(room))
        {
            return RoomType.roomsWithLeftUpRightExits;
        }

        if (hasRightNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithRightDownExits;
        }
        if (hasLeftNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithLeftDownExits;
        }
        if (hasUpNeighbour(room) && hasRightNeighbour(room))
        {
            return RoomType.roomsWithUpRightExits;
        }
        if (hasLeftNeighbour(room) && hasUpNeighbour(room))
        {
            return RoomType.roomsWithLeftUpExits;
        }
        if (hasLeftNeighbour(room) && hasRightNeighbour(room))
        {
            return RoomType.roomsWithLeftRightExits;
        }
        if (hasUpNeighbour(room) && hasDownNeighbour(room))
        {
            return RoomType.roomsWithUpDownExits;
        }

        if (hasLeftNeighbour(room))
        {
            return RoomType.roomsWithLeftExits;
        }
        if (hasUpNeighbour(room))
        {
            return RoomType.roomsWithUpExits;
        }
        if (hasRightNeighbour(room))
        {
            return RoomType.roomsWithRightExits;
        }
        if (hasDownNeighbour(room))
        {
            return RoomType.roomsWithDownExits;
        }


        return RoomType.roomsWithoutExits;
    }
    enum RoomType
    {
        roomsWithRightDownExits,
        roomsWithLeftRightDownExits,
        roomsWithLeftDownExits,

        roomsWithUpRightDownExits,
        roomsWithLeftUpRightDownExits,
        roomsWithLeftUpDownExits,

        roomsWithUpRightExits,
        roomsWithLeftUpRightExits,
        roomsWithLeftUpExits,

        roomsWithLeftExits,
        roomsWithUpExits,
        roomsWithRightExits,
        roomsWithDownExits,

        roomsWithLeftRightExits,
        roomsWithUpDownExits,

        roomsWithoutExits
    }

}
