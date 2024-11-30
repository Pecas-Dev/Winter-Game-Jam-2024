using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class objectiveSystem : MonoBehaviour
{
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

    public class obj
    {
        public string name;
        public bool conditionMet;
    }

    public List<obj> objectivesList = new List<obj>(); // in total
    public List<obj> gameObjectives = new List<obj>(); // for this game

    public int vaseCount;
    public int carpetCount;
    public int presentCount;

    [SerializeField] private TextMeshProUGUI[] objectiveText = new TextMeshProUGUI[3];

    void Start()
    {
        vaseCount = 0;
        carpetCount = 0;
        presentCount = 0;

        objectivesList.Add(new obj { name = "Set Christmas Tree on fire", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal a cat", conditionMet = false });
        objectivesList.Add(new obj { name = "Destroy 3 vases", conditionMet = false });
        objectivesList.Add(new obj { name = "Rip the couch", conditionMet = false });
        objectivesList.Add(new obj { name = "Stain 2 carpets", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal 4 presents", conditionMet = false });
        objectivesList.Add(new obj { name = "Spill milk and eat the cookies", conditionMet = false });
        objectivesList.Add(new obj { name = "Unplug the fridge", conditionMet = false });
        objectivesList.Add(new obj { name = "Clog the toilet", conditionMet = false });
        objectivesList.Add(new obj { name = "Drink all the Glögg", conditionMet = false });

        for (int i = 0; i < 3; i++)
        {
            int random = Random.Range(0, objectivesList.Count-1);

            gameObjectives.Add(objectivesList[random]);
            string text = objectivesList[random].name;
            
            // expand text if necessary
            if (text == "Destroy 3 vases")
            {
                text += " (" + vaseCount.ToString() + "/3)";
            } else if (text == "Stain 2 carpets")
            {
                text += " (" + carpetCount.ToString() + "/2)";
            } else if (text == "Steal 4 presents")
            {
                text += " (" + presentCount.ToString() + "/4)";
            }

            objectiveText[i].text = text;
            objectivesList.Remove(objectivesList[random]);
        }


        for (int i = 0; i < 3; i++)
        {
            Debug.Log(gameObjectives[i].name);
        }
    }

    // destroy a vase, check if all vases have been destroyed & change conditionMet
    public void DestroyVases()
    {
        vaseCount++;

        if (vaseCount == 3) 
        {
            for (int i = 0; i < gameObjectives.Count; i++) 
            { 
                if (gameObjectives[i].name == "Destroy 3 vases")
                {
                    gameObjectives[i].conditionMet = true;
                    return;
                }
            }
        }
    }

    // stain a carpet, check if all carpets have been stained & change conditionMet
    public void StainCarpets()
    {
        carpetCount++;

        if (carpetCount == 2)
        {
            for (int i = 0; i < gameObjectives.Count; i++)
            {
                if (gameObjectives[i].name == "Stain 2 carpets")
                {
                    gameObjectives[i].conditionMet = true;
                    return;
                }
            }
        }
    }

    // steal a present, check if all presents have been stolen & change conditionMet
    public void StealPresents()
    {
        presentCount++;

        if (presentCount == 4)
        {
            for (int i = 0; i < gameObjectives.Count; i++)
            {
                if (gameObjectives[i].name == "Steal 4 presents")
                {
                    gameObjectives[i].conditionMet = true;
                    return;
                }
            }
        }
    }

    // change conditionMet based on objective name
    public void OtherConditionMet(string objectiveName)
    {
        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].name == objectiveName)
            {
                gameObjectives[i].conditionMet = true;
                return;
            }
        }
    }

    // check if all 3 game objectives have their conditions met
    private bool CheckIfAllConditionsAreMet()
    {
        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].conditionMet == false)
            {
                return false;
            }
        }
        return true;
    }
    
    void Update()
    {
        
    }
}
