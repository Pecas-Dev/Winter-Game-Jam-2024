using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class roomManager : MonoBehaviour
{ 
    public List<Collider2D> colList = new List<Collider2D>();

    [SerializeField] private Collider2D startingRoomCol;

    private void Awake()
    {
        colList.Insert(0, startingRoomCol);
        for (int i = 1; i < colList.Count; i++) 
        {
            colList[i].gameObject.GetComponent<roomCollision>().OnRoomExit(); 
        }
        
    }

    

    
}
