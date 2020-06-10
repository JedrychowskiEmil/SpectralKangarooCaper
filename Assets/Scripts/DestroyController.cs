using UnityEngine;

public class DestroyController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "BoxDown" || collision.gameObject.tag == "BoxTop")
        {
            GameObject parent = collision.gameObject.transform.parent.gameObject;
            Destroy(parent);
        }
    }
}
