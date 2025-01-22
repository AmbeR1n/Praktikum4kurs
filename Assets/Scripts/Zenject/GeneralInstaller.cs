using UnityEngine;
using Zenject;

public class GeneralInstaller : MonoInstaller
{
    [SerializeField] private DialogueInstaller _dialogueInstaller;

    public override void InstallBindings()
    {
        BindDialogueInstaller();
    }

    private void BindDialogueInstaller()
    {
        Container.Bind<DialogueInstaller>().FromInstance(_dialogueInstaller).AsSingle();
    }
}
