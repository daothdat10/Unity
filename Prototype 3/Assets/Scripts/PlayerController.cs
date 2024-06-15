using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // chinh rig can nag jump
    private Animator playerAnim; // chuyen dong nhan vat
    public float jumpForce = 10; // nhay cao
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    public float gravityModifier; // sua doi trong luc
    public bool isOnGround = true; // kiem tra con tren mat dat khong nue khong keo xuong
    public bool gameOver = false; // kiem tra nhan vat dam vao vat the
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && gameOver != true)
        {

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;

            playerAnim.SetTrigger("Jump_trig");
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,0.1f);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtyParticle.Play();
        }else if (collision.gameObject.CompareTag("Obstade"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            explosionParticle.Play();
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(crashSound,0.1f);
        }
    }
}
