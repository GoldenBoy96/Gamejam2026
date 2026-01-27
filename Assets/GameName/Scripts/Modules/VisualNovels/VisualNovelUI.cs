using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class VisualNovelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dialogueText;

    private List<string> _dialogueLines = new List<string>();
    private bool _isSkipping = false;

    async void Start()
    {
        SetupDialogCollection(new List<string>
        {
            "Hello, welcome to our visual novel!",
            "This is the second line of dialogue.",
            "And this is the final line. Enjoy!"
        });
        PlayAllDialogues();
        // await PlayDialogueSequence("This is a sample dialogue line being displayed with a typing effect.");
    }

    public void SetupDialogCollection(List<string> dialogues)
    {
        _dialogueLines = dialogues;
    }

    public void ToggleUIVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }   
    public async void PlayAllDialogues()
    {
        foreach (var line in _dialogueLines)
        {
            await PlayDialogueSequence(line);
        }
    }

    public async Task PlayDialogueSequence(string dialogueLine)
    {
        string currentText = "";
        SetDialogueText(currentText);
        foreach (char letter in dialogueLine.ToCharArray())
        {
            if (_isSkipping)
            {
                currentText = dialogueLine;
                SetDialogueText(currentText);
                _isSkipping = false;
                return;
            }
            currentText += letter;
            SetDialogueText(currentText);
            await Task.Delay(30); // Adjust delay for typing speed
        }
        await Task.Delay(1000); // Adjust delay before next dialogue
    }

    public void SkipCurrentDialogue()
    {
        _isSkipping = true;
        // TO DO: Jump to next dialogue line
    }
    public void SetDialogueText(string dialogue)
    {
        _dialogueText.text = dialogue;
    }
}
