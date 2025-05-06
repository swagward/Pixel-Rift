using UnityEngine;

public class Narration : MonoBehaviour
{
    [SerializeField] private AudioSource playerSrc;
    [SerializeField] private AudioClip audioClip;
    
    private void Start()
    {
        playerSrc = GameObject.Find("PlayerRoot").GetComponent<AudioSource>();
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        playerSrc.clip = audioClip;
        playerSrc.Play();

        this.GetComponent<BoxCollider>().enabled = false; //stops player from colliding with trigger again x
    }
}
