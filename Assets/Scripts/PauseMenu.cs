using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	public GameObject mainMenuLayout;
	public GameObject graphicsOptionsLayout;
	public GameObject audioOptionsLayout;
	public GameObject quitMenuLayout;

	void OnEnable() {
		mainMenuLayout.SetActive(true);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	void OnDisable() {
		mainMenuLayout.SetActive(false);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	//TODO: How to do this properly?
	public void ContinueGame() {
		Time.timeScale = 1.0f;
		OnDisable ();
	}

	// GRAPHICS
	public void GraphicsOptionsLayout() {
		OnDisable ();
		graphicsOptionsLayout.SetActive (true);
	}

	public void ApplyGraphicsOptions() {

	}

	public void CancelGraphicsOptions() {

	}

	// AUDIO
	public void AudioOptionsLayout() {
		OnDisable ();
		audioOptionsLayout.SetActive (true);
	}

	public void ApplyAudioOptions() {

	}

	public void CancelAudioOptions() {

	}

	// QUIT
	public void QuitLayout() {
		OnDisable ();
		quitMenuLayout.SetActive (true);
	}

	public void ConfirmQuitButton() {
		SceneManager.LoadScene ("Menu");
	}

	public void CancelQuitButton() {
		OnDisable ();
		mainMenuLayout.SetActive (true);
	}
}
