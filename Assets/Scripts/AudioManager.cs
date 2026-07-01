using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [Tooltip("Attach an AudioSource here for 2D sounds like UI clicks.")]
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    //UI sounds, global alerts, player feedback
    public void PlaySound2D(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    //Explosions, enemy deaths, environmental impacts
    public void PlaySound3D(AudioClip clip, Vector3 position, float volume = 1f)
    {
        if (clip == null) return;

        // Create an invisible, temporary GameObject to hold the sound
        GameObject soundObject = new GameObject("TempAudio_" + clip.name);
        soundObject.transform.position = position;

        // Add an AudioSource component and configure it
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        
        // Setting spatialBlend to 1 makes the sound fully 3D (drops off with distance)
        audioSource.spatialBlend = 1f; 
        
        // Play the sound
        audioSource.Play();

        // Destroy the temporary GameObject once the clip finishes playing
        Destroy(soundObject, clip.length);
    }
}