using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;

    void Awake()
    {
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Debug.LogWarning("EXTRA" + this + "DELETED");
        //     Destroy(gameObject);
        // }

        if (instance != null && instance != this)
        {
            Debug.Log("EXTRA " + this + " DELETED");
            Destroy(instance.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
            characterData = character;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
