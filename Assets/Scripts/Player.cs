using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;

	float hAxis;
	float vAxis;
	bool rDown;

	Vector3 moveVec;

	Animator anim;

	// Start is called before the first frame update
    void Awake()
    {
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		rDown = Input.GetButton("Run");

		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		if(rDown)
        {
			transform.position += moveVec * speed * 1.5f * Time.deltaTime;
		}
        else
        {
			transform.position += moveVec * speed * Time.deltaTime;
		}

		anim.SetBool("isWalk", moveVec != Vector3.zero);
		anim.SetBool("isRun", rDown);

		transform.LookAt(transform.position + moveVec);
	}
}
