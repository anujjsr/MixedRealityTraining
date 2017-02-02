﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerScript : MonoBehaviour {

    AccessPanel aPanel;
    bool correctAnswer=true;
	// Use this for initialization
	void Start () {
        if (AccessPanel.Instance == null) {
            Debug.Log("No AccessPanel Instance");
        }
        else {
            aPanel = AccessPanel.Instance;
        }
    }
	
    public void Answer_Button_Click() {
        aPanel.setCorrectPanelActive(true);
        aPanel.getCorrectPanel().transform.position = aPanel.getAssessmentPanel().transform.position;
        aPanel.setAssessmentActive(false);

        if (correctAnswer) {
            aPanel.setCorrectPanelText("You have answered correct. Good Job!");
            //  Increment precent finished if we are still doing that here.
        }
        else {
            aPanel.setCorrectPanelText("You have answered incorrectly. Try Again?");
        }
    }
}
