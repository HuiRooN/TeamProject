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
	[SerializeField] Transform cameraTransform;

	// Start is called before the first frame update
	void Awake()
    {
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
	{
		//GetInput();
		Move();
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

        //bool isMove = moveInput.magnitude != 0;
        //anim.SetBool("isRun", isMove);

        //if (isMove)
        //{
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
		//}
	}
}
