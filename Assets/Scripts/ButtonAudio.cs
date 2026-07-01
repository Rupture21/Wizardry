using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioClip clickSound; 

    public void OnButtonClick()
    {
        if (clickSound != null && Camera.main != null)
        {
            AudioManager.Instance.PlaySound2D(clickSound);
        }
    }
}
