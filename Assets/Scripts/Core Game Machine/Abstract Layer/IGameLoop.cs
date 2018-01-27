using System.Collections;

public interface IGameLoop
{
    void Initial(ISensorListener sensor, IAnimateManager animate);

    IEnumerator MainLoop();
}