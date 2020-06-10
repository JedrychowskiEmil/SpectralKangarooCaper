using UnityEngine;

public class SoundController : MonoBehaviour
{
    static AudioSource audioSource;
    public static AudioClip jump;
    public static AudioClip score;
    public static AudioClip button;
    public static AudioClip hit;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        jump = Resources.Load<AudioClip>("jump");
        score = Resources.Load<AudioClip>("score");
        button = Resources.Load<AudioClip>("button");
        hit = Resources.Load<AudioClip>("hit");
    }

    public static void playSound(string sound)
    {
        switch (sound)
        {
            case "jump":
                audioSource.PlayOneShot(jump);
                break;
            case "score":
                audioSource.PlayOneShot(score);
                break;
            case "button":
                audioSource.PlayOneShot(button);
                break;
            case "hit":
                audioSource.PlayOneShot(hit);
                break;
        }
    }

}
