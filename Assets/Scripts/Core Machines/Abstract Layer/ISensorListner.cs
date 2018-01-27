using System.Collections;
using System.Collections.Generic;

public interface ISensorListener
{
    List<int> Result { get; }

    IEnumerator ListenNextBusPath();
}