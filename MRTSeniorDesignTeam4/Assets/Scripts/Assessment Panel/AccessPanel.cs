﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AccessPanel : HoloToolkit.Unity.Singleton<AccessPanel> {

    //
    //  All basic panels for the menu system
    //
    GameObject infoPanel;
    GameObject assessmentPanel;
    GameObject correctPanel;

    //
    //  Answer/assessment panel
    //
    GameObject answerPanel;
    BoxCollider assessmentCollider;

    //
    //  Variables used for the answer buttsons A-F
    //
    GameObject ansA;
    GameObject ansB;
    GameObject ansC;
    GameObject ansD;
    GameObject ansE;
    GameObject ansF;

    Text currentScore;
    int maxScore;
    int score = 0;

    ReadText txtToSpeach;
    Image img;
    Camera cam;
    XmlParser xml = new XmlParser();

    List<string> corretlyAnswered = new List<string>();

    /// <summary>
    ///     This is run when the application starts.
    ///         This method is used to initalize all of its required variables once.
    ///         The reason for this is because it uses the singleton design pattern as to not
    ///         have to create many instances of this object.
    /// </summary>
    void Start() {
        assessmentPanel = GameObject.Find("AssessmentPanel");
        infoPanel = GameObject.Find("InfoPanel");
        correctPanel = GameObject.Find("CorrectPanel");
        assessmentCollider = assessmentPanel.transform.GetComponent<BoxCollider>();
        answerPanel = assessmentPanel.transform.FindChild("AnswerPanel").gameObject;
        ansA = answerPanel.transform.FindChild("AnswerA").gameObject;
        ansB = answerPanel.transform.FindChild("AnswerB").gameObject;
        ansC = answerPanel.transform.FindChild("AnswerC").gameObject;
        ansD = answerPanel.transform.FindChild("AnswerD").gameObject;
        ansE = answerPanel.transform.FindChild("AnswerE").gameObject;
        ansF = answerPanel.transform.FindChild("AnswerF").gameObject;
        txtToSpeach = GameObject.Find("TextToSpeech").GetComponent<ReadText>();
        img = infoPanel.transform.FindChild("InfoContainer").FindChild("ObjectImage").GetComponent<Image>();
        ansC.SetActive(false);
        ansD.SetActive(false);
        ansF.SetActive(false);
        ansE.SetActive(false);
        infoPanel.SetActive(false);
        assessmentPanel.SetActive(false);
        correctPanel.SetActive(false);
        xml.setup();
        cam = GameObject.Find("HoloLensCamera").GetComponent<Camera>();
    }

    #region Info Panel Helper methods

    /// <summary>
    ///     
    ///     This section is where most of the utility methods that interact with the
    ///     Info panel go. This allows for multiple classes to interact with the panel
    ///     without having to use the find methods which can be inefficent in unity.
    ///      
    ///     For future development, if any new methods that need to interact with the
    ///     InfoPanel section of the menu system they should be placed here.
    /// 
    /// </summary>

    public Vector3 getCamForward() {
        return cam.transform.forward;
    }

    public XmlParser getXMLParser() {
        return xml;
    }

    public void setTitle(string s) {
        infoPanel.transform.FindChild("InfoContainer").FindChild("ObjectTitle").GetComponent<Text>().text = s;
    }

    public string getTitle() {
        return infoPanel.transform.FindChild("InfoContainer").FindChild("ObjectTitle").GetComponent<Text>().text;
    }

    public void setInfoPanelPosition(float x, float y, float z) {
        infoPanel.transform.position = new Vector3(x, y, z);
    }

    public void setDesc(string s) {
        GameObject g = infoPanel.transform.FindChild("InfoContainer").FindChild("InfoParagraph").gameObject;
        g.GetComponent<Text>().text = s;
    }

    public void setMaterial(string s) {
        GameObject g = infoPanel.transform.FindChild("InfoContainer").FindChild("MaterialInfoParagraph").gameObject;
        g.GetComponent<Text>().text = s;
    }

    public void setImg(string s) {
        //Image img = infoPanel.transform.FindChild("InfoContainer").FindChild("ObjectImage").GetComponent<Image>();
        Sprite st = (Sprite)Resources.Load<Sprite>(s);
        img.sprite = st;
    }

    public string getSpeechText() {
        return infoPanel.transform.FindChild("InfoContainer").FindChild("InfoParagraph").gameObject.GetComponent<Text>().text;
    }

    public string getQuestionSpeech() {
        return GameObject.Find("QuestionPanel").transform.FindChild("QuestionText").GetComponent<Text>().text;
    }

    public GameObject getInfoPanel() {
        return infoPanel;
    }

    public void setInfoPanelVis(bool vis) {
        infoPanel.SetActive(vis);
    }
    #endregion

    #region Correct Panel helper methods

    /// <summary> 
    /// 
    ///     This region is all methods for accessing and changing the CorrectPanel
    ///     
    ///     In future development it is best to add/change any of the methods for this
    ///     panel in this region.
    /// 
    /// </summary>

    public GameObject getCorrectPanel() {
        return correctPanel;
    }

    public void setCorrectPanelVis(bool vis) {
        correctPanel.SetActive(vis);
    }

    public void setCorrectPanelText(string s) {
        Text t = correctPanel.transform.FindChild("CorrectPanelText").FindChild("CorrectText").GetComponent<Text>();
        t.text = s;
    }
    #endregion

    #region Assessment Panel Helpers

    /// <summary>
    /// 
    ///     This region holds all of the core methods for the AssessmentPanel.
    ///     Whether it is setting the score, changing the text of the MainMenu to reflect the score
    ///     and many more functionalities they are all here.
    ///     
    ///     In future development any changes/additions to the functionality of the AssessmentPanel
    ///     should be done here so that they are easily found.
    /// 
    /// </summary>

    public void setCurrentScore() {
        currentScore = GameObject.Find("WelcomeLabel").GetComponent<Text>();
        maxScore = RoomAssetManager.Instance.assetCount;
        currentScore.text = "Module Score: " + score + " of " + maxScore;
    }

    public void setScore(int j, string s) {

        bool answeredYet = true;
        for (int i = 0; i < corretlyAnswered.Count; i++) {
            if(s == corretlyAnswered[i]) {
                answeredYet = false;
                break;
            }
        }
        if (answeredYet) {
            score += j;
            corretlyAnswered.Add(s);

            //
            //  Remove this once max score can be found.
            //
            currentScore.text = "Module Score:" + score + " of " + maxScore;
        }
    }

    public void setMaxScore(int i) {

        maxScore = i;
    }

    public void resetCorrectlyAnswered() {
        corretlyAnswered.Clear();
        score = 0;
    }


    public void setAssessmentPanelVis(bool vis) {
        assessmentPanel.SetActive(vis);
    }

    public GameObject getAssessmentPanel() {
        return assessmentPanel;
    }

    public void setQuestion(string s) {
        assessmentPanel.transform.FindChild("QuestionPanel").FindChild("QuestionText").GetComponent<Text>().text = s;
    }

    #region setting/getting of the multiple choice buttons
    //
    //  Start of A
    //
    public GameObject getAnsA() {
        return ansA;
    }

    public void setAnsAText(string s) {
        ansA.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }

    public void setAnsABool(bool b) {
        ansA.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }

    public void setAnsAVis(bool t) {
        ansA.SetActive(t);
    }


    //
    //  Start of B
    //
    public void setAnsBVis(bool t) {
        ansB.SetActive(t);
    }

    public GameObject getAnsB() {
        return ansB;
    }

    public void setAnsBText(string s) {
        ansB.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }

    public void setAnsBBool(bool b) {
        ansB.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }


    //
    //  Start of C
    //
    public void setAnsCVis(bool t) {
        ansC.SetActive(t);
    }

    public GameObject getAnsC() {
        return ansC;
    }

    public void setAnsCBool(bool b) {
        ansC.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }

    public void setAnsCText(string s) {
        ansC.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }


    //
    //  Start of D
    //
    public void setAnsDVis(bool t) {
        ansD.SetActive(t);
    }

    public GameObject getAnsD() {
        return ansD;
    }

    public void setAnsDBool(bool b) {
        ansD.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }

    public void setAnsDText(string s) {
        ansD.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }


    //
    //  Start of E
    //

    public void setAnsEVis(bool t) {
        ansE.SetActive(t);
    }

    public GameObject getAnsE() {
        return ansE;
    }

    public void setAnsEBool(bool b) {
        ansE.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }

    public void setAnsEText(string s) {
        ansE.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }


    //
    //  Start of F
    //
    public GameObject getAnsF() {
        return ansF;
    }

    public void setAnsFBool(bool b) {
        ansF.transform.FindChild("AnswerButton").GetComponent<AnswerScript>().correctAnswer = b;
    }

    public void setAnsFText(string s) {
        ansF.transform.FindChild("AnswerText").GetComponent<Text>().text = s;
    }

    public void setAnsFVis(bool t) {
        ansF.SetActive(t);
    }

    #endregion

    public void setAssessmentRows(int rows) {

        if (rows == 1) {
            assessmentCollider.size = new Vector3(assessmentCollider.size.x, 315);
            assessmentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 315);
            assessmentPanel.transform.FindChild("QuestionPanel").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 315);
            answerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(570, 100);
        }

        else if (rows == 2) {
            assessmentCollider.size = new Vector3(assessmentCollider.size.x, 395);
            assessmentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 395);
            assessmentPanel.transform.FindChild("QuestionPanel").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 395);
            answerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(570, 180);
        }
        else if(rows == 3) {
            assessmentCollider.size = new Vector3(assessmentCollider.size.x, 475);
            assessmentPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 475);
            assessmentPanel.transform.FindChild("QuestionPanel").gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600, 475);
            answerPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(570, 260);
        }

    }
    #endregion

}
