using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;


public class myScript : MonoBehaviour {

    public FixedTouchField TouchField;
    public MobileController MoveJoystick;
    public Jump jump;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var fps = GetComponent<RigidbodyFirstPersonController>();

        fps.mouseLook.LookAxis = TouchField.TouchDist;
        fps.runAxis = MoveJoystick.inputvector;
        fps.jumpAxis = jump.Pressed;
    }
}
