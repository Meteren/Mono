
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BlackBoard blackBoard = new BlackBoard();
    public float roomIndex = 0;

    private void Awake()
    {
        instance = this;
    }
    //change transfrom with room
    public List<Transform> rooms;

    private void Update()
    {
        
    }

    private void ListenRoomIndex()
    {
        foreach(var room in rooms)
        {

        }
    }

}
