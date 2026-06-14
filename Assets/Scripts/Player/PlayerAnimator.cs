using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator am;
    MoveScript ms;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        ms = GetComponent<MoveScript>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (ms.moveDirection.x != 0 || ms.moveDirection.y != 0)
        {
            am.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move", false);
        }
    } 

    void SpriteDirectionChecker()
    {
        if (ms.lastHorizontalvector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
