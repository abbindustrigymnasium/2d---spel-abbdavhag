using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	int JumpSfxRandomizer;
	float waiter;
	bool haveFallen;
	public AudioMixer audioMixer;
	private AudioSource audioSrc;
	public AudioClip runAudio;
	public AudioClip jumpAudio1;
	public AudioClip jumpAudio2;
	public AudioClip jumpAudio3;
	public AudioClip jumpAudio4;
	public AudioClip dieAudio;
	private Vector3 m_Velocity = Vector3.zero;
	float mixerVolume;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioSrc = GetComponent<AudioSource>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	void Start(){
		waiter = 2f;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
     			OnLandEvent.Invoke();
			}
		}
	}

	void Update(){
		if(transform.position.x > 10.3f){
			SceneManager.LoadScene(2);
		}
		if(haveFallen){
			waiter -= Time.deltaTime;
			if(waiter <= 0){
				SceneManager.LoadScene(3);
			}
		}
		audioMixer.GetFloat("volume", out mixerVolume);
	}


	public void Move(float move, bool crouch, bool jump)
	{

		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 4.3f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
		}
		 
				//only control the player if grounded or airControl is turned on
		if (!m_Grounded && m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 1f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			JumpSfxRandomizer = Random.Range(1,5);

			audioSrc.volume = 0.83f;

			switch (JumpSfxRandomizer)
			{
				case 1:
					audioSrc.clip = jumpAudio1;
					break;
				case 2:
					audioSrc.clip = jumpAudio2;
					break;
				case 3:
					audioSrc.clip = jumpAudio3;
					break;
				case 4:
					audioSrc.clip = jumpAudio4;
					break;
			}
			audioSrc.loop = false;
			if(mixerVolume != -80){
			audioSrc.Play();
			}
		}
			if (move != 0 && m_Grounded){
				audioSrc.volume = 0.7f;
				if(!audioSrc.isPlaying){
					audioSrc.clip = runAudio;
					audioSrc.loop = true;
					if(mixerVolume != -80){
					audioSrc.Play();
					}
				} 
			} else if(audioSrc.clip == runAudio){
				audioSrc.Stop();
			}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Falldetector"){
			haveFallen = true;
			audioSrc.clip = dieAudio;
			audioSrc.volume = 0.5f;
			audioSrc.loop = false;
			if(!audioSrc.isPlaying){
			if(mixerVolume != -80){
			audioSrc.Play();
			}
		}
	}
}
}
