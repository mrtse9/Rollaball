using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public AudioClip winSound;
	public AudioClip pickupSound;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	private AudioSource soundPlayer;

	
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		count = 0;

		SetCountText();

		winTextObject.SetActive(false);

		soundPlayer = GetComponent<AudioSource>();
	}

	void FixedUpdate()
	{
		
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);
			soundPlayer.PlayOneShot(pickupSound, 1.0f);
			count++;
			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Points: " + count.ToString();

		if (count >= 14)
		{
			winTextObject.SetActive(true);
			soundPlayer.PlayOneShot(winSound, 1.0f);
		}
	}
}