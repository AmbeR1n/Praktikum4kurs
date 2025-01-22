using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DialogueManager : MonoBehaviour
{
    public bool DialogPlay { get; private set; }

    private Story _currentStory;
    private TextAsset _incJson;

    private GameObject _dialoguePanel;
    private TextMeshProUGUI _dialogueText;
    private TextMeshProUGUI _nameText;

    [HideInInspector] public GameObject _choiceButtonsPanel; // Переделать
    private GameObject _choiceButton;
    private List<TextMeshProUGUI> _choiceButtonText = new();

    [Inject]
    public void Construct(DialogueInstaller dialogueInstaller)
    {
        _incJson = dialogueInstaller.IncJson;
        _dialoguePanel = dialogueInstaller.DialoguePanel;
        _dialogueText = dialogueInstaller.DialogueText;
        _nameText = dialogueInstaller.NameText;

        _choiceButtonsPanel = dialogueInstaller.ChoiceButtonsPanel;
        _choiceButton = dialogueInstaller.ChoiceButton;
    }

    private void Awake()
    {
        _currentStory = new Story(_incJson.text);
    }

    void Start()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        DialogPlay = true;
        _dialoguePanel.SetActive(true);
        ContinueStory();
    }

    public void ContinueStory(bool choiceBefore = false)
    {
        if (_currentStory.canContinue)
        {
            ShowDialogue();
            ShowChoiceButton();
        }
        else if (!choiceBefore)
            ExitDialogue();
    }

    private void ShowDialogue()
    {
        _dialogueText.text = _currentStory.Continue();
        _nameText.text = (string)_currentStory.variablesState["CharacterName"];
    }

    private void ShowChoiceButton()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        _choiceButtonsPanel.SetActive(currentChoices.Count != 0);

        if (currentChoices.Count <= 0)
            return;

        ClearChoiceButton();

        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choice = Instantiate(_choiceButton);
            choice.GetComponent<ButtonAction>().Index = i;
            choice.transform.SetParent(_choiceButtonsPanel.transform);

            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = currentChoices[i].text;
            _choiceButtonText.Add(choiceText);
        }
    }

    private void ClearChoiceButton()
    {
        foreach (Transform child in _choiceButtonsPanel.transform)
            Destroy(child.gameObject);
        _choiceButtonText.Clear();
    }

    public void ChoiceButtonAction(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory(true);
    }

    private void ExitDialogue()
    {
        DialogPlay = false;
        _dialoguePanel.SetActive(false);

        MovingNextScene();
    }

    private void MovingNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex <= SceneManager.sceneCount)
            SceneManager.LoadScene(nextSceneIndex);
    }
}
