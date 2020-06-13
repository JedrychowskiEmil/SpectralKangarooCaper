using UnityEngine;

public class DestroyController : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "BoxDown" || collision.gameObject.tag == "Gem" || collision.gameObject.tag == "Spike")
        {
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            Destroy(parent);
        }
    }
}
