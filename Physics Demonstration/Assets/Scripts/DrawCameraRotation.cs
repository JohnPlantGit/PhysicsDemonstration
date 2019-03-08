using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DrawCameraRotation : MonoBehaviour
{
    public CameraController m_cameraController;

    private Text m_text;
	// Use this for initialization
	void Start ()
    {
        m_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_text.text = "Pitch: " + m_cameraController.Pitch.ToString("0.##") + ", Yaw: " + m_cameraController.Yaw.ToString("0.##");
	}
}
