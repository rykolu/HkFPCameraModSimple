using UnityEngine;

namespace HkFPCameraModSimple
{
	public class FPcameraSimple : MonoBehaviour
	{
		private float cameraSensitivity = 75;
		private float climbSpeed = 4;
		private float normalMoveSpeed = 10;

		private float slowMoveFactor;
		private float fastMoveFactor;

		private float rotationX = 0.0f;
		private float rotationY = 0.0f;

		void Update()
		{
			rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
			rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
			rotationY = Mathf.Clamp(rotationY, -90, 90);

			transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
			transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{ fastMoveFactor = 3; }
            else 
				{ fastMoveFactor = 1; }
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
				{ slowMoveFactor = 0.25f; }
			else
				{ slowMoveFactor = 1; }

			transform.position += transform.forward * normalMoveSpeed * slowMoveFactor * fastMoveFactor * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * normalMoveSpeed * slowMoveFactor * fastMoveFactor * Input.GetAxis("Horizontal") * Time.deltaTime;


			if (Input.GetKey(KeyCode.Q)) { transform.position += transform.up * climbSpeed * slowMoveFactor * fastMoveFactor * Time.deltaTime; }
			if (Input.GetKey(KeyCode.E)) { transform.position -= transform.up * climbSpeed * slowMoveFactor * fastMoveFactor * Time.deltaTime; }
		}
	}
}