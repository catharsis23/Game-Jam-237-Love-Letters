using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePromptManager : MonoBehaviour
{

    public Transform prompt;
    public DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (prompt.gameObject.active && Input.GetKey(KeyCode.E))
        {
            dialogueTrigger.TriggerDialogue();
            GameObject.Find("GameManager").GetComponent<GameManager>().isInDialogue = true;

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
