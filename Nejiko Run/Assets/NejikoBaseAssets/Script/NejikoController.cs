using UnityEngine;
using System.Collections;

public class NejikoController : MonoBehaviour {

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;

	public float gravity;
	public float speedZ;
	public float speedJump;

	void Start()
	{
		//必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController> ();
		animator = GetComponent<Animator> ();

	}

	void Update()
	{
		//地上にいる場合のみ操作を行う
		if (controller.isGrounded) {
			//Inputを検知して前に進める
			if (Input.GetAxis ("Vertical") > 0.0f) {
				moveDirection.z = Input.GetAxis ("Vertical") * speedZ;
			} else {
				moveDirection.z = 0;
			}

			//方向転換
			transform.Rotate (0, Input.GetAxis ("Horizontal") * 3, 0);

			//ジャンプ
			if (Input.GetButton ("Jump")) {
				moveDirection.y = speedJump;
				animator.SetTrigger ("jump");
			}
		}

		//重力分の力を毎フレーム追加
		moveDirection.y -= gravity * Time.deltaTime;

		//移動実行
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move (globalDirection * Time.deltaTime);

		//移動後設置してたらY方向の速度はリセットする
		if(controller.isGrounded ) moveDirection.y = 0;

		//速度が0以上なら走っているフラグをtrueにする
		animator.SetBool("run", moveDirection.z > 0.0f);


	
	}
}
