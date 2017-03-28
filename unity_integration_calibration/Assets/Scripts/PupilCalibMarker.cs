﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PupilCalibMarker : MonoBehaviour {

	RectTransform _transform;
	Image _image;
	bool _started=false;
	float x,y;

	// Use this for initialization
	void Start () {

		_transform = GetComponent<RectTransform> ();
		_image = GetComponent<Image> ();
		_image.enabled = false;

		AssignDelegates ();

	}

	public void AssignDelegates(){
		PupilGazeTracker.Instance.NullDelegates ();
		PupilGazeTracker.Instance.OnCalibrationStarted += OnCalibrationStarted;
		PupilGazeTracker.Instance.OnCalibrationDone += OnCalibrationDone;
		PupilGazeTracker.Instance.OnCalibData += OnCalibData;
		if (PupilGazeTracker.Instance.m_status == PupilGazeTracker.EStatus.Calibration)
			Debug.LogWarning ("Switching calibration in playmode. Method to restart calibration in another mode is not yet implemented! This might cause issues");
	}

	public void OnCalibrationStarted(PupilGazeTracker m)
	{
		_started = true;
	}

	public void OnCalibrationDone(PupilGazeTracker m)
	{
		_started = false;
	}

	public void OnCalibData(PupilGazeTracker m,object position)
	{
		Vector3 _v3 = (Vector3)position;
		this.x = _v3.x;
		this.y = _v3.y;
	}

	void _SetLocation(float x,float y)
	{
		Canvas c = _transform.GetComponentInParent<Canvas> ();
		if (c == null)
			return;
		Vector3 pos=new Vector3 ((x-0.5f)*c.pixelRect.width,(y-0.5f)*c.pixelRect.height,0);
		_transform.localPosition = pos;
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C))
			PupilGazeTracker.Instance.StartCalibration ();
		if (Input.GetKeyDown (KeyCode.S))
			PupilGazeTracker.Instance.StopCalibration ();/**/
		_image.enabled = _started;
		if(_started)
			_SetLocation (x, y);

//		if (_started)
//			print ("2D calibration method is going on");
	}
}
