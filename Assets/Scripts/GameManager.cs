using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isInDialogue;
    public bool isInInventory;
    public int lettersDelivered;
    public int lettersNeededDeliveredToWin = 5;

    public List<Letter> inventory;
    public int inventorySize;

    public GameObject inventoryItem; 

    // Start is called before the first frame update
    void Start()
    {
        inventory = new List<Letter>();
        lettersDelivered = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(Letter letter)
    {
        inventory.Add(letter);
        GameObject inventoryScreen = GameObject.Find("ItemsParent");

        var newItem = Instantiate(inventoryItem, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        newItem.name = "Letter for " + letter.owner;
        newItem.transform.parent = inventoryScreen.transform;

        newItem.GetComponent<LetterTrigger>().letter = letter;
    }

    public void RemoveFromInventory(Letter letter)
    {
        inventory.Remove(letter);
        GameObject letterToDestroy = GameObject.Find("Letter for " + letter.owner);
        Destroy(letterToDestroy);
    }
}
