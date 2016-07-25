using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	Tower chosenTower = null;

	[Header("Towers")]
	public Tower t1; // this is just for the header "bug"?
	public Tower t2, t3, t4, t5, t6, t7, t8, t9;

	[Header("Related objects")]
	public GameObject popupPanel;

	void Start() {
		popupPanel.SetActive (false);
	}

	public void Tower1Button() {
		//chosenTower = (TowerEnum)1;
		chosenTower = t1;
	}

	public void Tower2Button() {
		//chosenTower = (TowerEnum)2;
		chosenTower = t2;
	}

	public void Tower3Button() {
		//chosenTower = (TowerEnum)3;
		chosenTower = t3;
	}

	public void Tower4Button() {
		//chosenTower = (TowerEnum)4;
		chosenTower = t4;
	}

	public void Tower5Button() {
		//chosenTower = (TowerEnum)5;
		chosenTower = t5;
	}

	public void Tower6Button() {
		//chosenTower = (TowerEnum)6;
		chosenTower = t6;
	}

	public void Tower7Button() {
		//chosenTower = (TowerEnum)7;
		chosenTower = t7;
	}

	public void Tower8Button() {
		//chosenTower = (TowerEnum)8;
		chosenTower = t8;
	}

	public void Tower9Button() {
		//chosenTower = (TowerEnum)9;
		chosenTower = t9;
	}

	public void Tower1OnTriggerEnter() {
		displayStats (t1);
	}

	public void Tower2OnTriggerEnter() {
		displayStats (t2);
	}

	public void Tower3OnTriggerEnter() {
		displayStats (t3);
	}

	public void Tower4OnTriggerEnter() {
		displayStats (t4);
	}

	public void Tower5OnTriggerEnter() {
		displayStats (t5);
	}

	public void Tower6OnTriggerEnter() {
		displayStats (t6);
	}

	public void Tower7OnTriggerEnter() {
		displayStats (t7);
	}

	public void Tower8OnTriggerEnter() {
		displayStats (t8);
	}

	public void Tower9OnTriggerEnter() {
		displayStats (t9);
	}

	public void TowerOnTriggerExit() {
		hideStats ();
	}
	/*
	public void Tower2OnTriggerExit() {

	}

	public void Tower3OnTriggerExit() {

	}

	public void Tower4OnTriggerExit() {

	}

	public void Tower5OnTriggerExit() {

	}

	public void Tower6OnTriggerExit() {

	}

	public void Tower7OnTriggerExit() {

	}

	public void Tower8OnTriggerExit() {

	}

	public void Tower9OnTriggerExit() {
		
	}*/

	public Tower GetChosenTower() {
		return chosenTower;
	}

	public bool IsATowerChosen() {
		return chosenTower != null;
	}

	public void ClearChosenTower() {
		chosenTower = null;
	}

	void displayStats(Tower tower) {
		popupPanel.SetActive (true);

		Text text = popupPanel.GetComponentInChildren<Text> ();
		text.text = tower.ToString ();
	}

	void hideStats() {
		popupPanel.SetActive (false);
	}
}
