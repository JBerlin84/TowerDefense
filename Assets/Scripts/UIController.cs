using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	Tower chosenTower = null;
	Tower previewTower;

	[Header("Towers")]
	public Tower t1; // this is just for the header "bug"?
	public Tower t2, t3, t4, t5, t6, t7, t8, t9;

    [Header("Preview")]
    [Range(0,100)]
    public int rotationSpeed;

	[Header("Related objects")]
	public GameObject popupPanel;		// popup window with data for tower and projectile.
	public GameObject previewWindow;	// displays what is selected

	void Start() {
		popupPanel.SetActive (false);
	}

	void Update() {
        // Rotates the preview tower if there is one.
        GameObject preview = GameObject.FindGameObjectWithTag("Preview");
        if (preview) {
            preview.transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
	}

	public void Tower1Button() {
		selectTower (t1);
	}

	public void Tower2Button() {
		selectTower (t2);
	}

	public void Tower3Button() {
		selectTower (t3);
	}

	public void Tower4Button() {
		selectTower (t4);
	}

	public void Tower5Button() {
		selectTower (t5);
	}

	public void Tower6Button() {
		selectTower (t6);
	}

	public void Tower7Button() {
		selectTower (t7);
	}

	public void Tower8Button() {
		selectTower (t8);
	}

	public void Tower9Button() {
		selectTower (t9);
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

	/// <summary>
	/// Gets the selected tower to build.
	/// </summary>
	/// <returns>The selected tower.</returns>
	public Tower GetSelectedTower() {
		return chosenTower;
	}

	/// <summary>
	/// Check wether a tower has been selected for building.
	/// </summary>
	/// <returns><c>true</c> if a tower is chosen; otherwise, <c>false</c>.</returns>
	public bool IsATowerChosen() {
		return chosenTower != null;
	}

	/// <summary>
	/// Clears the selected tower to be build so we no longer have a selected tower.
	/// </summary>
	public void ClearSelectedTower() {
		chosenTower = null;
		GameObject.Destroy (previewTower);
        GameObject g = GameObject.FindGameObjectWithTag("Preview");
        GameObject.Destroy(g);        
	}

	/// <summary>
	/// Selects given tower as the current tower to be built.
	/// </summary>
	/// <param name="tower">Tower to build.</param>
	void selectTower(Tower tower) {
		ClearSelectedTower ();

		chosenTower = tower;

        // preview tower.
		previewTower = Instantiate (tower) as Tower;
		Transform ttransform = previewTower.transform;
		Destroy(ttransform.GetComponent<Tower>());
        Destroy(ttransform.GetComponent<NavMeshObstacle>());
		Destroy(ttransform.GetComponent<AudioSource>());
		ttransform.parent = previewWindow.transform;
		ttransform.gameObject.layer = LayerMask.NameToLayer("UI");
		ttransform.localScale = new Vector3 (45, 45, 45);
        ttransform.localPosition = Vector3.zero;
        ttransform.localPosition = new Vector3(ttransform.localPosition.x, ttransform.localPosition.y - ttransform.localScale.y, ttransform.localPosition.z);
        ttransform.localRotation = Quaternion.Euler(Vector3.zero);
        ttransform.tag = "Preview";
	}

	/// <summary>
	/// Displaies the stats for the selected tower.
	/// </summary>
	/// <param name="tower">Tower.</param>
	void displayStats(Tower tower) {
		popupPanel.SetActive (true);

		Text text = popupPanel.GetComponentInChildren<Text> ();
		text.text = tower.ToString ();
	}

	/// <summary>
	/// Hides the stats for the tower.
	/// </summary>
	void hideStats() {
		popupPanel.SetActive (false);
	}
}
