using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public Animator animator;
    public Animator optionsAnimator;
    public Animator inventoryAnimator;



    private Queue<string> sentences;
    private GameObject[] deliverButtons;
    private Dialogue currentDialogue;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue with: " + dialogue.name);
        Debug.Log("First Sentence: " + dialogue.sentences[0]);

        nameText.text = dialogue.name;
        sentences.Clear();
        currentDialogue = dialogue;
        animator.SetBool("isOpen", true);
        optionsAnimator.SetBool("isOpen", false);


        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            PresentPlayerOptions();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void StartFailedDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue with: " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();
        currentDialogue = dialogue;
        animator.SetBool("isOpen", true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayFailedNextSentence();

    }

    public void DisplayFailedNextSentence()
    {
        if (sentences.Count == 0)
        {
            CloseAllCanvases();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void PresentPlayerOptions()
    {
        Debug.Log("Presting player with options");
        //display panel with options: deliver letter, leave conversation
        animator.SetBool("isOpen", false);
        optionsAnimator.SetBool("isOpen", true);

        //deliver letter option brings up inventory screen with new deliver button

        //leave conversation performs End Dialogue
    }

    public void ChooseLetterToDeliver()
    {
        Debug.Log("Choosing letter to Deliver");
        optionsAnimator.SetBool("isOpen", false);
        inventoryAnimator.SetBool("isOpen", true);
        GameObject.Find("GameManager").GetComponent<GameManager>().isInInventory = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().isInDialogue = false;

        //make deliver button visible

        GameObject inventoryScreen = GameObject.Find("ItemsParent");

        deliverButtons = GameObject.FindGameObjectsWithTag("DeliverButton");

        foreach (GameObject deliverButton in deliverButtons)
        {
            deliverButton.GetComponent<Image>().enabled = true;
            deliverButton.GetComponent<Button>().enabled = true;
            deliverButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

        }
    }


    public void DeliverLetter(Letter letter)
    {
        GameObject inventoryScreen = GameObject.Find("ItemsParent");

        deliverButtons = GameObject.FindGameObjectsWithTag("DeliverButton");

        foreach (GameObject deliverButton in deliverButtons)
        {
            deliverButton.GetComponent<Image>().enabled = false;
            deliverButton.GetComponent<Button>().enabled = false;
            deliverButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }

        CloseAllCanvases();

        Debug.Log("Delivering letter for " + letter.owner + " to " + currentDialogue.name);
        //successful delivery
        if (letter.owner.Equals(currentDialogue.name))
        {
            Debug.Log("Successful Letter delivery!");
            GameObject.Find("GameManager").GetComponent<GameManager>().lettersDelivered++;
            GameObject.Find("GameManager").GetComponent<GameManager>().RemoveFromInventory(letter);
        } else
        {
            Debug.Log("Failed Letter delivery!");
            Dialogue failedDialogue = new Dialogue();
            failedDialogue.name = currentDialogue.name;
            failedDialogue.sentences = new string[1] { "This is obviously not for me!" };
            StartDialogue(failedDialogue);
        }


        
    }

    public void EndDialogue()
    {

        animator.SetBool("isOpen", false);
        optionsAnimator.SetBool("isOpen", false);

        GameObject.Find("GameManager").GetComponent<GameManager>().isInDialogue = false;

    }

    public void CloseAllCanvases()
    {

        animator.SetBool("isOpen", false);
        optionsAnimator.SetBool("isOpen", false);
        inventoryAnimator.SetBool("isOpen", false);

        GameObject.Find("GameManager").GetComponent<GameManager>().isInDialogue = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().isInInventory = false;

    }
}
