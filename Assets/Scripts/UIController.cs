using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

	TowerEnum chosenTower = TowerEnum.none;

	public void Tower1Button() {
		chosenTower = (TowerEnum)1;
	}

	public void Tower2Button() {
		chosenTower = (TowerEnum)2;
	}

	public void Tower3Button() {
		chosenTower = (TowerEnum)3;
	}

	public void Tower4Button() {
		chosenTower = (TowerEnum)4;
	}

	public void Tower5Button() {
		chosenTower = (TowerEnum)5;
	}

	public void Tower6Button() {
		chosenTower = (TowerEnum)6;
	}

	public void Tower7Button() {
		chosenTower = (TowerEnum)7;
	}

	public void Tower8Button() {
		chosenTower = (TowerEnum)8;
	}

	public void Tower9Button() {
		chosenTower = (TowerEnum)9;
	}

	public void Tower1OnTriggerEnter() {

	}

	public void Tower2OnTriggerEnter() {

	}

	public void Tower3OnTriggerEnter() {

	}

	public void Tower4OnTriggerEnter() {

	}

	public void Tower5OnTriggerEnter() {

	}

	public void Tower6OnTriggerEnter() {

	}

	public void Tower7OnTriggerEnter() {

	}

	public void Tower8OnTriggerEnter() {

	}

	public void Tower9OnTriggerEnter() {

	}

	public void Tower1OnTriggerExit() {
		
	}

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
		
	}

	public TowerEnum GetChosenTower() {
		return chosenTower;
	}

	public bool IsATowerChosen() {
		return chosenTower != TowerEnum.none;
	}

	public void ClearChosenTower() {
		chosenTower = TowerEnum.none;
	}

	void displayStats() {

	}
}
