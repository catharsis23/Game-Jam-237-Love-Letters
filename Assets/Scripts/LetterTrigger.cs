using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTrigger : MonoBehaviour
{
    public Letter letter;

    public void TriggerLetter()
    {
        FindObjectOfType<LetterManager>().ShowLetter(letter);
    }

    public void DeliverLetter()
    {
        FindObjectOfType<DialogueManager>().DeliverLetter(letter);
    }
}
