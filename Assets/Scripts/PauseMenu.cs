using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class PauseMenu : MonoBehaviour {
	[Header("Layouts")]
	public GameObject menuPanel;
	public GameObject mainMenuLayout;
	public GameObject graphicsOptionsLayout;
	public GameObject audioOptionsLayout;
	public GameObject quitMenuLayout;
	
	[Header("Elements")]
	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle;
	public Dropdown textureQualityDropdown;
	public Dropdown AADropdown;
	public Dropdown VSyncDropdown;
	[Space(10)]
	public Slider masterVolumeSlider;
	public Slider musicVolumeSlider;
	public Slider sfxVolumeSlider;

	bool isDisplaying;

	public bool IsDisplaying {
		get {
			return isDisplaying;
		}
	}

	void Start() {
		//TODO: Set all the settings for audio and graphics here.
		PopulateResolutionDropdown();
		LoadSettings ();
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
		SaveGraphicsSettings ();
		ApplySettings ();
		Back ();
		mainMenuLayout.SetActive (true);
	}

	public void CancelGraphicsOptions() {
		LoadSettings();
		Back ();
		mainMenuLayout.SetActive (true);
	}

	// AUDIO
	public void AudioOptionsLayout() {
		Back ();
		audioOptionsLayout.SetActive (true);
	}

	public void ApplyAudioOptions() {
		SaveAudioSettings ();
		ApplySettings ();
		Back ();
		mainMenuLayout.SetActive (true);
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

	void SaveGraphicsSettings() {
		// Graphics settings
		PlayerPrefs.SetInt ("resolutionDropdown", resolutionDropdown.value);	// TODO: this needs to take into account the text, not index.
		PlayerPrefs.SetInt ("fullscreenToggle",fullscreenToggle.isOn?1:0);
		PlayerPrefs.SetInt ("textureQualityDropdown", textureQualityDropdown.value);
		PlayerPrefs.SetInt ("AADropdown", AADropdown.value);
		PlayerPrefs.SetInt ("VSyncDropdown", VSyncDropdown.value);

		PlayerPrefs.Save ();
	}

	void SaveAudioSettings() {
		// Volume settings
		PlayerPrefs.SetFloat ("masterVolumeSlider", masterVolumeSlider.value);
		PlayerPrefs.SetFloat ("musicVolumeSlider", musicVolumeSlider.value);
		PlayerPrefs.SetFloat ("sfxVolumeSlider", sfxVolumeSlider.value);

		PlayerPrefs.Save ();
	}

	void LoadSettings() {
		// Graphics settings
		resolutionDropdown.value = PlayerPrefs.GetInt ("resolutionDropdown");	// TODO: this needs to take into account the text, not index.
		fullscreenToggle.isOn = (PlayerPrefs.GetInt ("fullscreenToggle") == 1) ? true:false;
		textureQualityDropdown.value = PlayerPrefs.GetInt ("textureQualityDropdown");
		AADropdown.value = PlayerPrefs.GetInt ("AADropdown");
		VSyncDropdown.value = PlayerPrefs.GetInt ("VSyncDropdown");

		// Volume settings
		masterVolumeSlider.value = PlayerPrefs.GetFloat ("masterVolumeSlider");
		musicVolumeSlider.value = PlayerPrefs.GetFloat ("musicVolumeSlider");
		sfxVolumeSlider.value = PlayerPrefs.GetFloat ("sfxVolumeSlider");
	}

	void ApplySettings() {
		// Set screen resolution
		Text text = resolutionDropdown.GetComponentInChildren<Text> ();
		string[] res = text.text.Split('x');
		int width = Int32.Parse (res [0]);	// pars width
		int height = Int32.Parse (res [1]);	// parse height
		Screen.SetResolution (width, height, fullscreenToggle.isOn);

		//Screen.SetResolution(
		QualitySettings.masterTextureLimit = textureQualityDropdown.value; // 0 max, 1 mid, 2 low
		QualitySettings.antiAliasing = (AADropdown.value < 3) ? AADropdown.value * 2 : 8;	// 0, 2, 4 or 8 multisamples per pixels.
		QualitySettings.vSyncCount = VSyncDropdown.value;

	}
}
