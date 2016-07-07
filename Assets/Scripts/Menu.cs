using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject mainMenuObject;
	public GameObject optionsMenuObject;
	public GameObject confirmQuitObject;

	void Start() {
		mainMenuObject.SetActive (true);
	}

	public void Play() {
		SceneManager.LoadScene ("Game");
	}

	public void Quit() {
		mainMenuObject.SetActive (false);
		confirmQuitObject.SetActive (true);
	}

	public void ConfirmQuit() {
		Application.Quit ();
	}

	public void CancelQuit() {
		confirmQuitObject.SetActive (false);
		mainMenuObject.SetActive (true);
	}

	public void OptionsMenu() {
		mainMenuObject.SetActive (false);
		optionsMenuObject.SetActive (true);
	}

	public void MainMenu() {
		optionsMenuObject.SetActive (false);
		mainMenuObject.SetActive (true);
	}
}
