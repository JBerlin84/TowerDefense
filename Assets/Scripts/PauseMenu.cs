using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameObject menuPanel;
	public GameObject mainMenuLayout;
	public GameObject graphicsOptionsLayout;
	public GameObject audioOptionsLayout;
	public GameObject quitMenuLayout;

	bool isDisplaying;

	public bool IsDisplaying {
		get {
			return isDisplaying;
		}
	}

	public void Display() {
		isDisplaying = true;
		menuPanel.SetActive (true);
		mainMenuLayout.SetActive(true);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	public void Close() {
		isDisplaying = false;
		menuPanel.SetActive (false);
		mainMenuLayout.SetActive(false);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	void Back() {
		mainMenuLayout.SetActive(false);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	//TODO: How to do this properly?
	public void ContinueGame() {
		Time.timeScale = 1f;
		Close ();
	}

	// GRAPHICS
	public void GraphicsOptionsLayout() {
		Back ();
		graphicsOptionsLayout.SetActive (true);
	}

	public void ApplyGraphicsOptions() {

	}

	public void CancelGraphicsOptions() {
		Back ();
		mainMenuLayout.SetActive (true);
	}

	// AUDIO
	public void AudioOptionsLayout() {
		Back ();
		audioOptionsLayout.SetActive (true);
	}

	public void ApplyAudioOptions() {

	}

	public void CancelAudioOptions() {
		Back ();
		mainMenuLayout.SetActive (true);
	}

	// QUIT
	public void QuitLayout() {
		Back ();
		quitMenuLayout.SetActive (true);
	}

	public void ConfirmQuitButton() {
		SceneManager.LoadScene ("Menu");
	}

	public void CancelQuitButton() {
		Back ();
		mainMenuLayout.SetActive (true);
	}
}
