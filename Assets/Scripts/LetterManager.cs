using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterManager : MonoBehaviour
{
    public TMP_Text letterText;

    public Animator animator;

    private string letterContents;
    public Animator inventoryAnimator;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void ShowLetter(Letter letter)
    {
        Debug.Log("Displaying letter for " + letter.owner);
        animator.SetBool("isOpen", true);
        letterContents = letter.letter;
        inventoryAnimator.SetBool("isOpen", false);

        DisplayLetter();

    }

    public void DisplayLetter()
    {


        letterText.text = letterContents;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(letterContents));
    }

    IEnumerator TypeSentence(string sentence)
    {
        letterText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            letterText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        Debug.Log("Closing Letter");
        animator.SetBool("isOpen", false);
        inventoryAnimator.SetBool("isOpen", true);

        GameObject.Find("GameManager").GetComponent<GameManager>().isInDialogue = false;

    }
}
