using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPickup : MonoBehaviour
{

    public Transform prompt;
    public Letter letter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (prompt.gameObject.active && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddToInventory(letter);
            Destroy(gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.name.Equals("Player"))
        {
            prompt.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.gameObject.name.Equals("Player"))
        {
            prompt.gameObject.SetActive(false);
        }
    }
}
