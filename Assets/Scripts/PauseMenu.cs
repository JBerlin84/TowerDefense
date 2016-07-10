using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {
	[Header("Layouts")]
	public GameObject menuPanel;
	public GameObject mainMenuLayout;
	public GameObject graphicsOptionsLayout;
	public GameObject audioOptionsLayout;
	public GameObject quitMenuLayout;
	
	[Header("Elements")]
	public Dropdown resolutionDropdown;

	bool isDisplaying;

	public bool IsDisplaying {
		get {
			return isDisplaying;
		}
	}

	void Start() {
		//TODO: Set all the settings for audio and graphics here.
		PopulateResolutionDropdown();
	}

	/// <summary>
	/// Populates the resolution dropdown table.
	/// </summary>
	void PopulateResolutionDropdown() {
		resolutionDropdown.ClearOptions();
		Resolution[] resolutions = Screen.resolutions;
		List<string> sResolutions = new List<string> ();
		foreach (Resolution res in resolutions) {
			sResolutions.Add(res.width + "x" + res.height);
		}
		resolutionDropdown.AddOptions (sResolutions);
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
		//TODO: Revert all the graphics settings here
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
		//TODO: Revert all the audio settings here
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
