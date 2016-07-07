/// <summary>
/// Code to handle the menu system
/// </summary>
/// 
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public GameObject mainMenuObject;
	public GameObject optionsMenuObject;
	public GameObject confirmQuitObject;

	/// <summary>
	/// only main menu object should be active in the beginning.
	/// </summary>
	void Start() {
		mainMenuObject.SetActive (true);
	}

	/// <summary>
	/// Play button code.
	/// </summary>
	public void Play() {
		SceneManager.LoadScene ("Game");
	}

	/// <summary>
	/// Quit button.
	/// </summary>
	public void Quit() {
		mainMenuObject.SetActive (false);
		confirmQuitObject.SetActive (true);
	}

	/// <summary>
	/// When you confirms that you want to quit.
	/// </summary>
	public void ConfirmQuit() {
		Application.Quit ();
	}

	/// <summary>
	/// When saying no to quit.
	/// </summary>
	public void CancelQuit() {
		confirmQuitObject.SetActive (false);
		mainMenuObject.SetActive (true);
	}

	/// <summary>
	/// When pressing options menu button.
	/// </summary>
	public void OptionsMenu() {
		mainMenuObject.SetActive (false);
		optionsMenuObject.SetActive (true);
	}

	/// <summary>
	/// When going back to the main menu.
	/// </summary>
	public void MainMenu() {
		optionsMenuObject.SetActive (false);
		mainMenuObject.SetActive (true);
	}
}
