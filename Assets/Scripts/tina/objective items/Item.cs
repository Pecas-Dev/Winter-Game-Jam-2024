using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
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

    public enum Types 
    { christmasTree, vase, carpet, present, milkncookies, cat, toilet, couch, fridge, glogg, shoes,
        candles, wallet, chinaSet, lamp, headphones, pillow, jewelry }
    public Types type;
    
    public int pointsWorth;
    //public string type;
    public bool disappear;
    public bool changeState;
    public Sprite changed;
    public bool isObjective;

    private void Start()
    {
        disappear = false;
        changeState = false;
        isObjective = false;
    
        for (int i = 0; i < GameManager.instance.objectiveSyst.originalObjectivesAmount; i++)
        {
            if (type == Types.christmasTree ||
            type == Types.vase ||
            type == Types.carpet ||
            type == Types.present ||
            type == Types.milkncookies ||
            type == Types.cat ||
            type == Types.toilet ||
            type == Types.couch ||
            type == Types.fridge ||
            type == Types.glogg)
            { pointsWorth = 0; }
        }
    }

    public void interaction()
    {
        GameManager.instance.mischiefSyst.addPoints(pointsWorth);

        // normal items
        if (type == Types.candles ||
            type == Types.wallet ||
            type == Types.chinaSet ||
            type == Types.lamp ||
            type == Types.headphones ||
            type == Types.pillow ||
            type == Types.jewelry ||
            type == Types.cat)
        {
            disappear = true;
        }


        // special numerical objectives - vase, carpet, present,

        if (type == Types.vase)
        {
            GameManager.instance.objectiveSyst.DestroyVases();
            changeState = true;
            return;
        }
        if (type == Types.carpet)
        {
            GameManager.instance.objectiveSyst.StainCarpets();
            changeState = true;
            return;
        }
        if (type == Types.present)
        {
            GameManager.instance.objectiveSyst.StealPresents();
            disappear = true;
            return;
        }

        // rest of objectives - christmasTree, milkncookies, cat, toilet, couch, fridge, glogg,

        /*
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
         */

        if (type == Types.christmasTree)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Set Christmas Tree on fire");
            changeState = true;
            return;
        }
        if (type == Types.milkncookies)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Spill milk and eat the cookies");
            changeState = true;
            return;
        }
        if (type == Types.cat)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Steal a cat");
            disappear = true;
            return;
        }
        if (type == Types.toilet)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Clog the toilet");
            changeState = true;
            return;
        }
        if (type == Types.couch)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Rip the couch");
            changeState = true;
            return;
        }
        if (type == Types.fridge)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Unplug the fridge");
            changeState = true;
            return;
        }
        if (type == Types.glogg)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Chug all the Glögg");
            changeState = true;
            return;
        }
        if (type == Types.shoes)
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Steal the shoes");
            changeState = true;
            return;
        }


        /*if (type == "Present")
        {
            GameManager.instance.objectiveSyst.StealPresents(); // steal = disable gameobject
            go.SetActive(false);
            return;
        }
        if (type == "ChristmasTree")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Set Christmas Tree on fire");
            go.GetComponent<SpriteRenderer>().sprite = changed; // into burnt tree
            return;
        }
        if (type == "Cat")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Steal a cat");
            go.SetActive(false); // steal = disable gameobject
            return;
        }
        if (type == "Couch")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Rip the couch");
            go.GetComponent<SpriteRenderer>().sprite = changed; // into a ripped couch
            return;
        }
        if (type == "MilkAndCookies")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Spill milk and eat the cookies");
            go.GetComponent<SpriteRenderer>().sprite = changed; // unto empty milk cup and cookie crumbs
            return;
        }
        if (type == "Fridge")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Unplug the fridge");
            go.GetComponent<SpriteRenderer>().sprite = changed; // into open fridge with spoiled food
            return;
        }
        if (type == "Toilet")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Clog the toilet");
            go.GetComponent<SpriteRenderer>().sprite = changed; // into clogged toilet
            return;
        }
        if (type == "Glogg")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Chug all the Glögg");
            go.GetComponent<SpriteRenderer>().sprite = changed; // into empty bottle
            return;
        }*/
    }
}
