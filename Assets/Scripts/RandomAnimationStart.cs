using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimationStart : MonoBehaviour
{
    void Start()
    {
        // 1. Grab the Animator component attached to this prefab
        Animator anim = GetComponent<Animator>();

        // 2. Get information about the animation state currently playing on Layer 0
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        // 3. Pick a random starting point between 0.0 (start) and 1.0 (end)
        float randomStartTime = Random.Range(0f, 1f);

        // 4. Play the current animation at that random starting point
        anim.Play(state.fullPathHash, -1, randomStartTime);
    }
}