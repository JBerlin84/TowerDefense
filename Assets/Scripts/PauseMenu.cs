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

	/// <summary>
	/// Gets a value indicating whether this menu is displaying.
	/// </summary>
	/// <value><c>true</c> if this instance is displaying; otherwise, <c>false</c>.</value>
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

	/// <summary>
	/// Resets the pause menu to its initial state
	/// </summary>
	public void Display() {
		isDisplaying = true;
		menuPanel.SetActive (true);
		mainMenuLayout.SetActive(true);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	/// <summary>
	/// Close down the menu.
	/// </summary>
	public void Close() {
		isDisplaying = false;
		menuPanel.SetActive (false);
		mainMenuLayout.SetActive(false);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	/// <summary>
	/// Set all submenu to false.
	/// </summary>
	void Back() {
		mainMenuLayout.SetActive(false);
		graphicsOptionsLayout.SetActive(false);
		audioOptionsLayout.SetActive(false);
		quitMenuLayout.SetActive(false);
	}

	/// <summary>
	/// Continues the game.
	/// </summary>
	public void ContinueGame() {
		Time.timeScale = 1f;
		Close ();
	}

	/// <summary>
	/// Display graphics options layout
	/// </summary>
	public void GraphicsOptionsLayout() {
		Back ();
		graphicsOptionsLayout.SetActive (true);
	}

	/// <summary>
	/// Applies the graphics options.
	/// </summary>
	public void ApplyGraphicsOptions() {
		SaveGraphicsSettings ();
		ApplySettings ();
		Back ();
		mainMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Cancels the modified graphics options.
	/// </summary>
	public void CancelGraphicsOptions() {
		LoadSettings();
		Back ();
		mainMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Displays the audio options layout.
	/// </summary>
	public void AudioOptionsLayout() {
		Back ();
		audioOptionsLayout.SetActive (true);
	}

	/// <summary>
	/// Applies the audio options.
	/// </summary>
	public void ApplyAudioOptions() {
		SaveAudioSettings ();
		ApplySettings ();
		Back ();
		mainMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Cancels the modified audio options.
	/// </summary>
	public void CancelAudioOptions() {
		//TODO: Revert all the audio settings here
		Back ();
		mainMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Displays the confirm quit menu.
	/// </summary>
	public void QuitLayout() {
		Back ();
		quitMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Confirms you want to quit.
	/// </summary>
	public void ConfirmQuitButton() {
		SceneManager.LoadScene ("Menu");
	}

	/// <summary>
	/// You want to contiue to play.
	/// </summary>
	public void CancelQuitButton() {
		Back ();
		mainMenuLayout.SetActive (true);
	}

	/// <summary>
	/// Saves the graphics settings.
	/// </summary>
	void SaveGraphicsSettings() {
		// Graphics settings
		PlayerPrefs.SetInt ("resolutionDropdown", resolutionDropdown.value);	// TODO: this needs to take into account the text, not index.
		PlayerPrefs.SetInt ("fullscreenToggle",fullscreenToggle.isOn?1:0);
		PlayerPrefs.SetInt ("textureQualityDropdown", textureQualityDropdown.value);
		PlayerPrefs.SetInt ("AADropdown", AADropdown.value);
		PlayerPrefs.SetInt ("VSyncDropdown", VSyncDropdown.value);

		PlayerPrefs.Save ();
	}

	/// <summary>
	/// Saves the audio settings.
	/// </summary>
	void SaveAudioSettings() {
		// Volume settings
		PlayerPrefs.SetFloat ("masterVolumeSlider", masterVolumeSlider.value);
		PlayerPrefs.SetFloat ("musicVolumeSlider", musicVolumeSlider.value);
		PlayerPrefs.SetFloat ("sfxVolumeSlider", sfxVolumeSlider.value);

		PlayerPrefs.Save ();
	}

	/// <summary>
	/// Loads both the graphics and options settings.
	/// </summary>
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

	/// <summary>
	/// Applies both graphics and audio options.
	/// </summary>
	void ApplySettings() {
		// Set screen resolution
		Text text = resolutionDropdown.GetComponentInChildren<Text> ();
		string[] res = text.text.Split('x');
		int width = Int32.Parse (res [0]);	// pars width
		int height = Int32.Parse (res [1]);	// parse height
		Screen.SetResolution (width, height, fullscreenToggle.isOn);

		QualitySettings.masterTextureLimit = textureQualityDropdown.value; // 0 max, 1 mid, 2 low
		QualitySettings.antiAliasing = (AADropdown.value < 3) ? AADropdown.value * 2 : 8;	// 0, 2, 4 or 8 multisamples per pixels.
		QualitySettings.vSyncCount = VSyncDropdown.value;

	}
}
