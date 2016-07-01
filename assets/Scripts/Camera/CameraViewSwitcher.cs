using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class CameraViewSwitcher : MonoBehaviour {

    public enum FirstOrThird { First, Third };
    private FirstOrThird currentCameraState;
    public FirstOrThird getCurrentCameraState() { return currentCameraState; }

    private Dictionary<GameObject, FirstOrThird> ListOfTransformCamera;
    public GameObject StartCameraFromGameObject;

    // Use this for initialization
    void Start() {
        ListOfTransformCamera = new Dictionary<GameObject, FirstOrThird> ();

        // Set the Start Camera to Enabled and add it to the list
        StartCameraFromGameObject.GetComponentInChildren<Camera> ().enabled = true;
        ListOfTransformCamera.Add (StartCameraFromGameObject, FirstOrThird.Third);
        currentCameraState = FirstOrThird.Third;

        // Set the others to disabled
        foreach (GameObject gameobject in GameObject.FindGameObjectsWithTag ("FirstPerson")) {
            gameobject.GetComponentInChildren<Camera> ().enabled = false;
            gameobject.GetComponentInChildren<HeadBob> ().enabled = false;
            gameobject.GetComponentInChildren<RigidbodyFirstPersonController> ().enabled = false;
            ListOfTransformCamera.Add (gameobject, FirstOrThird.First);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeCameraToGameObject(GameObject toGameObject) {
        if (ListOfTransformCamera.ContainsKey (toGameObject)) {
            FirstOrThird stateOfGameobject;
            if (ListOfTransformCamera.TryGetValue (toGameObject, out stateOfGameobject)) {
                if (currentCameraState == FirstOrThird.First) {
                    toGameObject = StartCameraFromGameObject;
                }
            }

            // Just to be sure set all GameObjects its Camera to disabled
            foreach (GameObject gameobject in ListOfTransformCamera.Keys) {
                foreach (Camera cam in gameobject.GetComponentsInChildren<Camera> ()) {
                    cam.enabled = false;
                }
                foreach (HeadBob headhbob in gameobject.GetComponentsInChildren<HeadBob> ()) {
                    headhbob.enabled = false;
                }
                foreach (RigidbodyFirstPersonController rigibody in gameobject.GetComponentsInChildren<RigidbodyFirstPersonController> ()) {
                    rigibody.enabled = false;
                }
            }

            // Enable the good stuff
            foreach (Camera cam in toGameObject.GetComponentsInChildren<Camera> ()) {
                cam.enabled = true;
            }
            foreach (HeadBob headhbob in toGameObject.GetComponentsInChildren<HeadBob> ()) {
                headhbob.enabled = true;
            }
            foreach (RigidbodyFirstPersonController rigibody in toGameObject.GetComponentsInChildren<RigidbodyFirstPersonController> ()) {
                rigibody.enabled = true;
            }

            GameObject tempHUD = GameObject.FindGameObjectWithTag ("HUD");
            tempHUD.GetComponentInChildren<Canvas> ().worldCamera = toGameObject.GetComponentInChildren<Camera> ();

            if (ListOfTransformCamera.TryGetValue (toGameObject, out stateOfGameobject)) {
                switch (stateOfGameobject) {
                    case FirstOrThird.First:
                    currentCameraState = FirstOrThird.First;
                    tempHUD.GetComponentInChildren<Text> ().text = "Third Person";
                    break;
                    case FirstOrThird.Third:
                    currentCameraState = FirstOrThird.Third;
                    tempHUD.GetComponentInChildren<Text> ().text = "First Person";
                    break;
                }
            }
        }
    }
}
