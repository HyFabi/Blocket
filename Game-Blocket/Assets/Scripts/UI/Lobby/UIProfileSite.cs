using MLAPI;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class UIProfileSite : MonoBehaviour {
	[Header("Static Resources")]
	public Text createInput;
	public GameObject worldSelectionSite, characterSelectionSite, listContentPrefab;
	public Button backBtn, nextBtn, characterSlectBtn, worldSelectBtn, createBtn;
	public ScrollRect playerScrollRect, worldScrollRect;

	private RectTransform _playerContent, _worldContent;

	private bool _characterSelectionOpen = true;
	private bool CharacterSelectionOpen { 
		get => _characterSelectionOpen; 
		set {
			_characterSelectionOpen = value;
			characterSelectionSite.SetActive(value);
			worldSelectionSite.SetActive(!value);
		}
	}

	private List<string> FoundPlayerProfiles { get => _foundCharacterProfiles; set {
			_foundCharacterProfiles = value;
			foreach (string profile in value) {
				ListContentUI uiPSC = Instantiate(listContentPrefab, _playerContent.transform).GetComponent<ListContentUI>();
				int x = profile.LastIndexOf(@"\"), y = profile.LastIndexOf('.');
				uiPSC.contentName.text = profile.Substring(x+1, y-x-1);
				uiPSC.CharacterBtn = true;
			}
		}}
	private List<string> _foundCharacterProfiles = new List<string>();

	private List<string> FoundWorldProfiles { get => _foundWorldProfiles; set { 
			_foundWorldProfiles = value;
			foreach(string profile in value) {
				ListContentUI uiPSC = Instantiate(listContentPrefab, _worldContent.transform).GetComponent<ListContentUI>();
				int x = profile.LastIndexOf(@"\"), y = profile.LastIndexOf('.');
				uiPSC.contentName.text = profile.Substring(x + 1, y - x - 1);
				uiPSC.CharacterBtn = false;
			}
		}} 
	private List<string> _foundWorldProfiles= new List<string>();

	private bool _characterSelectonOpen;
	public bool CharacterSelectonOpen { get { 
			return _characterSelectonOpen; 
		} set { 
			_characterSelectonOpen = value;
			characterSelectionSite.SetActive(value);
			worldSelectionSite.SetActive(!value);
		} 
	}

	public void SelectedItem() {
		//UI
		if (CharacterSelectionOpen && NetworkManager.Singleton.IsClient)
				CharacterSelectionOpen = false;
			else if(((ListContentUI.selectedBtnNameCharacter?.Trim() == string.Empty ) != NetworkManager.Singleton.IsClient) && ((ListContentUI.selectedBtnNameWorld?.Trim() == string.Empty) != NetworkManager.Singleton.IsServer)) {
			GameManager.playerProfileNow = ProfileHandler.ImportProfile(ListContentUI.selectedBtnNameCharacter, true) as PlayerProfile;
			GameManager.worldProfileNow = ProfileHandler.ImportProfile(ListContentUI.selectedBtnNameWorld, false) as WorldProfile;
			GlobalVariables.UILobby.SiteIndexOpen = 1;
			}
					
	}

	private void InitButtons(){
		if(GlobalVariables.muliplayer)
			if(NetworkManager.Singleton.IsClient)
				worldSelectBtn.interactable = false;

		backBtn.onClick.AddListener(() => {
			GlobalVariables.UILobby.SiteIndexOpen = 0;
		});
		
		//nextBtn.onClick.AddListener(SelectedItem);

		characterSlectBtn.onClick.AddListener(() => {
			CharacterSelectionOpen = true;
		});
		worldSelectBtn.onClick.AddListener(() => {
			CharacterSelectionOpen = false;
		});
		createBtn.onClick.AddListener(() => {
			if (!ValidateInput())
				return;
			if (CharacterSelectionOpen) { 
				PlayerProfile p = new PlayerProfile(createInput.text, null);
				ProfileHandler.ExportProfile(p, true);
				GameManager.playerProfileNow = p;
				ListContentUI.selectedBtnNameCharacter = createInput.text;
			} else {
				WorldProfile p = new WorldProfile(createInput.text, null);
				ProfileHandler.ExportProfile(p, false);
				GameManager.worldProfileNow = p;
				ListContentUI.selectedBtnNameWorld = createInput.text;
			}
			FindAllProfiles();
			SelectedItem();
			///TODO: Characterdialoge...

		});
		
	}

	public bool ValidateInput() {
		if(createInput.text == null || createInput.text.Trim() == "")
			return false;
		return true;
	}

	public void Start() {
		FindAllProfiles();
	}

	public void FindAllProfiles() {
		FoundPlayerProfiles = ProfileHandler.FindAllProfiles(true);
		FoundWorldProfiles = ProfileHandler.FindAllProfiles(false);
		if(DebugVariables.checkProfileCount)
			Debug.Log($"PlayerProfiles: {FoundPlayerProfiles.Count}, WorldProfiles: {FoundWorldProfiles.Count}");
	}

	public void Awake()
	{
		GlobalVariables.UIProfileSite = this;
		CharacterSelectionOpen = true;
		InitButtons();
		_playerContent = playerScrollRect.content;
		_worldContent = worldScrollRect.content;
	}
}
