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
    public int originalObjectivesAmount;
    
    [SerializeField] private TextMeshProUGUI[] objectiveText = new TextMeshProUGUI[3];
    [SerializeField] private GameObject objListGO;
    [SerializeField] private float objListGOOpenX;
    [SerializeField] private float objListGOClosedX;

    private float objListGOPosX;
    private bool objListIsMoving;
    [SerializeField]  private float moveRate;

    /* public enum objectivesEnum { ChristamsTree, Cat, Vases, Couch, Carpets, Presents, Milk, Fridge, Toilet, Glogg};
     objectivesEnum objEnum;*/

    void Start()
    {
        vaseCount = 0;
        carpetCount = 0;
        presentCount = 0;
        //moveRate = 150f;
        // if you change the string later on, rememebr to change it in all other scripts bcoz its string dependent
        objectivesList.Add(new obj { name = "Set Christmas Tree on fire", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal a cat", conditionMet = false });
        objectivesList.Add(new obj { name = "Destroy 3 vases", conditionMet = false });
        objectivesList.Add(new obj { name = "Rip the couch", conditionMet = false });
        objectivesList.Add(new obj { name = "Stain 2 carpets", conditionMet = false });
        objectivesList.Add(new obj { name = "Steal 4 presents", conditionMet = false });
        objectivesList.Add(new obj { name = "Spill milk and eat the cookies", conditionMet = false });
        objectivesList.Add(new obj { name = "Unplug the fridge", conditionMet = false });
        objectivesList.Add(new obj { name = "Clog the toilet", conditionMet = false });
        objectivesList.Add(new obj { name = "Chug all the Glögg", conditionMet = false });

        originalObjectivesAmount = objectivesList.Count;

        // choose 3 random objectives, add them to the gameObjectives list and remove from objectivesList
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && objListIsMoving == false)
        {
            ShowHideObjectiveList();
            objListIsMoving = true;
        }

        if (objListIsMoving == true)
        {
            Vector3 tmp = objListGO.transform.localPosition;

            if (objListGOPosX <= objListGO.transform.localPosition.x)
            {
                // goes to the left
                tmp.x -= moveRate * Time.deltaTime;

            } else
            {
                tmp.x += moveRate * Time.deltaTime;
            }

            tmp.x = Mathf.Clamp(tmp.x, objListGOClosedX, objListGOOpenX);

            objListGO.transform.localPosition = tmp;

            if (tmp.x <= objListGOClosedX || tmp.x >= objListGOOpenX) {
                objListIsMoving = false;
            }
        }


    }

    private void ShowHideObjectiveList()
    {
        //Debug.Log(objListGO.transform.localPosition + " " + objListGO.transform.position);
        if (objListGO.transform.localPosition.x >= objListGOOpenX)
        {
            objListGOPosX = objListGOClosedX;
        }
        if (objListGO.transform.localPosition.x <= objListGOClosedX)
        {
            objListGOPosX = objListGOOpenX;
        }
    }

    // destroy a vase, check if all vases have been destroyed & change conditionMet
    public void DestroyVases()
    {
        vaseCount++;

        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].name == "Destroy 3 vases")
            {
                objectiveText[i].text = "Destroy 3 vases (" + vaseCount.ToString() + "/3)";
                if (vaseCount == 3)
                {
                    gameObjectives[i].conditionMet = true;
                    objectiveText[i].color = Color.green;

                    if (CheckIfAllConditionsAreMet()) { GameManager.instance.GameWon(); }
                    return;
                }
            }
        }        
    }

    // stain a carpet, check if all carpets have been stained & change conditionMet
    public void StainCarpets()
    {
        carpetCount++;

        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].name == "Stain 2 carpets")
            {
                objectiveText[i].text = "Stain 2 carpets (" + carpetCount.ToString() + "/2)";
                if (carpetCount == 2)
                {
                    gameObjectives[i].conditionMet = true;
                    objectiveText[i].color = Color.green;

                    if (CheckIfAllConditionsAreMet()) { GameManager.instance.GameWon(); }
                    return;
                }
            }
        }
    }

    // steal a present, check if all presents have been stolen & change conditionMet
    public void StealPresents()
    {
        presentCount++;

        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].name == "Steal 4 presents")
            {
                objectiveText[i].text = "Steal 4 presents (" + presentCount.ToString() + "/4)"; 
                if (presentCount == 4)
                {
                    gameObjectives[i].conditionMet = true;

                    objectiveText[i].color = Color.green;

                    if (CheckIfAllConditionsAreMet()) { GameManager.instance.GameWon(); }
                    return;
                }
            }
        }
    }

    // change conditionMet based on objective name
    public void OtherConditionMet(string objectiveName)
    {
       /* Debug.Log(gameObjectives[0].name + " " + gameObjectives[1].name + " " + gameObjectives[2].name);
        Debug.Log("compared to " + objectiveName);*/

        for (int i = 0; i < gameObjectives.Count; i++)
        {
            if (gameObjectives[i].name == objectiveName)
            {
                gameObjectives[i].conditionMet = true;
                objectiveText[i].color = Color.green;
                
                if (CheckIfAllConditionsAreMet()) { GameManager.instance.GameWon(); }
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
    
}
