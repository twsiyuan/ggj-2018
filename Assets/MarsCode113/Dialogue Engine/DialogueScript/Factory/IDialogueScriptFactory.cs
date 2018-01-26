namespace MarsCode113.DialogueFramework
{
    public interface IDialogueScriptFactory
    {

        DialogueScript BuildScript(object data);

    }
}