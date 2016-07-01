using UnityEngine;
using System.Collections;

public class ThirdPersonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag ("HUD").GetComponent<CameraViewSwitcher> ().getCurrentCameraState () == CameraViewSwitcher.FirstOrThird.Third) {
            if (Input.GetKey (KeyCode.W)) {
                this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + 0.1f);
            }
            if (Input.GetKey (KeyCode.A)) {
                this.transform.position = new Vector3 (this.transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
            }
            if (Input.GetKey (KeyCode.S)) {
                this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - 0.1f);
            }
            if (Input.GetKey (KeyCode.D)) {
                this.transform.position = new Vector3 (this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
            }
        } else if (GameObject.FindGameObjectWithTag ("HUD").GetComponent<CameraViewSwitcher> ().getCurrentCameraState () == CameraViewSwitcher.FirstOrThird.First) {
            // Else be add the position of the selected hero.
            // So when switching back at the third person Camera, the position will be always alright!
        }
    }
}
