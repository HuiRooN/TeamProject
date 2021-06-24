using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	Rigidbody rigidbody;

	public GameObject SuccessPanel;
	public GameObject FailPanel;

	public float speed;

	float hAxis;
	float vAxis;
	bool rDown;

	public int health = 0;
	public int maxHealth = 3;

	public int coin;
	public int maxCoin = 10;

	Vector3 moveVec;

	Animator anim;
	[SerializeField] Transform cameraTransform;
	[SerializeField] BGM bgm;

	[SerializeField] AudioClip[] audioClips;
	AudioSource audioSource;

	bool end;

	// Start is called before the first frame update
	void Awake()
    {
		Time.timeScale = 1;
		rigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		health = maxHealth;
		coin = 0;
		SuccessPanel.SetActive(false);
		FailPanel.SetActive(false);

		end = false;

		audioSource = GetComponent<AudioSource>();

	}

    // Update is called once per frame
    void Update()
	{
		//GetInput();
		if (!end)
		{
			Move();
			GameEnd();
			Cursor.visible = false;
		}
		else
			Cursor.visible = true;

		if(Input.GetKeyDown(KeyCode.Q))
        {
			coin = 10;
        }
	}

	void Move()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		rDown = Input.GetButton("Run");

		Vector3 moveInput = new Vector3(hAxis, 0f, vAxis);

		bool isMove = moveInput.magnitude != 0;
		anim.SetBool("isRun", isMove);

		if (isMove)
		{
			Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
			Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
			Vector3 moveDir = lookForward * moveInput.z + lookRight * moveInput.x;
			moveDir.Normalize();

			transform.forward = moveDir;
			if (rDown)
			{
				transform.position += moveDir * speed * 1.5f * Time.deltaTime;
			}
			else
			{
				transform.position += moveDir * speed * Time.deltaTime;
			}


			anim.SetBool("isWalk", moveDir != Vector3.zero);
			anim.SetBool("isRun", rDown);
		}
		else
		{
			anim.SetBool("isWalk", false);
			anim.SetBool("isRun", false);
		}
			//}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Coin")
		{
			Destroy(collision.gameObject);
			if (coin == maxCoin)
				return;
			else
			{
				coin += 1;

				audioSource.clip = audioClips[1];
				audioSource.Play();
				audioSource.loop = false;
			}
		}

		if(collision.gameObject.tag == "Door")
        {
			if(coin == maxCoin)
            {
				Time.timeScale = 0;
				SuccessPanel.SetActive(true);
				bgm.Victory();
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Axe"))
		{
			if (health == 0)
				return;

			audioSource.clip = audioClips[0];
			audioSource.Play();
			audioSource.loop = false;

			health -= 1;
			Vector3 reverseVec = -transform.position;
			StartCoroutine(OnDamage(reverseVec));
		}
		else
			anim.SetBool("isDamage", false);
    }

    IEnumerator OnDamage(Vector3 reverseVec)
    {
		yield return new WaitForSeconds(0.2f);

		reverseVec = reverseVec.normalized;
		reverseVec += Vector3.up;

		rigidbody.AddForce(reverseVec * 2.5f, ForceMode.Impulse);
    }

	public void GameEnd()
    {
		if(health == 0)
        {
			Time.timeScale = 0;
			FailPanel.SetActive(true);
			end = true;
			bgm.Defeat();
		}
    }

	public bool GetEnd()
    {
		return end;
    }
}
