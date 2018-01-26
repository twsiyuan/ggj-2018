using UnityEngine;

public class Engine : MonoBehaviour
{	
    
    private static Engine instance;
    public static Engine Instance{
        get {return instance;}
    }
    

    [SerializeField]
    private GameObject[] preloads;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Instantiate(Resources.Load("[Engine]"));
    }


    void Awake()
    {
        instance = this;
        
        name = "[Engine]";

        DontDestroyOnLoad(gameObject);

        foreach (var obj in preloads)
            Instantiate(obj);
    }
    
}