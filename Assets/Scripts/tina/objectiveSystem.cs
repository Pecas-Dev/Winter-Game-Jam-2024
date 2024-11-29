using System.Collections.Generic;
using NUnit.Framework;
using Unity.Multiplayer.Center.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class objectiveSystem : MonoBehaviour
{
    public class obj
    {
        public string name;
        public bool conditionMet;
    }

    public List<obj> objectivesList = new List<obj>();
    public List<obj> gameObjectives = new List<obj>();

    // which objective, is it fulfilled
    //public Dictionary<int, bool> objectives = new Dictionary<int, bool>();

    // 1 light the tree on fire
    // 2 steal a cat
    // 3 destroy 3 vases
    // 4 rip the couch
    // 5 stain 2 carpets
    // 6 steal 4 presents
    // 7 spill the milk and crush the cookies
    // 8 unplug the fridge
    // 9 clog the toilet (toilet paper disappears)
    // 10 chug their glogg

    public int vaseCount;
    public int carpetCount;
    public int presentCount;

    void Start()
    {
        vaseCount = 0;
        carpetCount = 0;
        presentCount = 0;

        objectivesList.Add(new obj { name = "Set Christmas Tree on fire", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal a cat", conditionMet = false });
        objectivesList.Add(new obj { name = "Destroy 3 vases (" + vaseCount.ToString() + "/3)", conditionMet = false });
        objectivesList.Add(new obj { name = "Rip the couch", conditionMet = false });
        objectivesList.Add(new obj { name = "Stain 2 carpets (" + carpetCount.ToString() + "/2)", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal 4 presents (" + presentCount.ToString() + "/4)", conditionMet = false });
        objectivesList.Add(new obj { name = "Spill milk and eat the cookies", conditionMet = false });
        objectivesList.Add(new obj { name = "Unplug the fridge", conditionMet = false });
        objectivesList.Add(new obj { name = "Clog the toilet", conditionMet = false });
        objectivesList.Add(new obj { name = "Drink all the Glögg", conditionMet = false });

        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, objectivesList.Count-1);
            gameObjectives.Add(objectivesList[random]);
            objectivesList.Remove(objectivesList[random]);
        }

        for (int i = 0; i < 3; i++)
        {
            Debug.Log(gameObjectives[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
