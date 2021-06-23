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
	}

    // Update is called once per frame
    void Update()
	{
		//GetInput();
		Move();
		GameEnd();
		//moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		//if(rDown)
  //      {
		//	transform.position += moveVec * speed * 1.5f * Time.deltaTime;
		//}
  //      else
  //      {
		//	transform.position += moveVec * speed * Time.deltaTime;
		//}

		//anim.SetBool("isWalk", moveVec != Vector3.zero);
		//anim.SetBool("isRun", rDown);

		//transform.LookAt(transform.position + moveVec);
	}
	void GetInput()
	{
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
				coin += 1;
		}

		if (collision.gameObject.tag == "Enemy")
		{
			anim.SetBool("isDamage", true);
			if (health == 0)
				return;
			else
				health -= 1;

			Vector3 reverseVec = -transform.position;
			StartCoroutine(OnDamage(reverseVec));
		}
		else
			anim.SetBool("isDamage", false);

		if(collision.gameObject.tag == "Door")
        {
			if(coin == maxCoin)
            {
				Time.timeScale = 0;
				SuccessPanel.SetActive(true);
			}
		}
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Axe"))
		{
			if (health == 0)
				return;
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
		}
    }
}
