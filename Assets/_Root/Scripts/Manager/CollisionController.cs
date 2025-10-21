using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public float StrengThreshold = 20f;
    public Animator playerAnim;



    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.relativeVelocity.magnitude > StrengThreshold)
        //{
        //    if (playerAnim == null) { 
        //        playerAnim = GetComponentInParent<AutoGetIk>().IkContainer.GetComponent<Animator>();
        //    }

        //    if(GameManager.Instance.currentGameState != EGameState.playing) return;
        //    playerAnim.enabled = false;
        //    EventManager.Event_OnPlayerDie?.Invoke();
        //}
    }
}
