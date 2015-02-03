using UnityEngine;
using System.Collections.Generic;
using NobleMuffins.Util;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoLocalizationText : ScriptableObject
{
	private static ICollection<SoLocalizationText> allTexts;
	public static ICollection<SoLocalizationText> AllTexts {
		get {
			if(allTexts == null)
			{
				var _allTexts = Resources.LoadAll<SoLocalizationText>(string.Empty);
				allTexts = new HashSet<SoLocalizationText>(_allTexts);
			}
			return allTexts;
		}
	}

	public void Awake()
	{
		if(AllTexts.Contains(this) == false)
		{
			AllTexts.Add(this);
		}

		var othersWithSameKey = from text in AllTexts where text != this && text.Key == Key select text;

		if(othersWithSameKey.Count() > 0) {
			ResetKey();
		}
	}

	public void ResetKey()
	{
		#if UNITY_EDITOR
		key = System.Guid.NewGuid().ToString();
		EditorUtility.SetDirty(this);
		#endif
	}

	public void OnDestroy()
	{
		AllTexts.Remove(this);
	}

	public static string[] AllLanguages {
		get {
			var allLanguages = new HashSet<string>();
			foreach(var text in AllTexts)
			{
				foreach(var language in text.Languages)
				{
					allLanguages.Add(language);
				}
			}
			if(allLanguages.Contains(string.Empty)) allLanguages.Remove(string.Empty);
			var result = new string[allLanguages.Count];
			allLanguages.CopyTo(result);
			return result;
		}
	}

	[System.Serializable] class TextsByLanguage: SerializableDictionary<string,string> { }

	[SerializeField] private TextsByLanguage Content = new TextsByLanguage();

	[SerializeField] private string key;
	public string Key { get {
			#if UNITY_EDITOR
			if(string.IsNullOrEmpty(key))
			{
				ResetKey();
			}
			#endif
			return key;
		} }

	public string Comment;

	public string[] Languages {
		get {
			return Content.keys.ToArray();
		}
	}

	public string Text {
		get {
			var lang = SoLocalization.PreferredLanguage;
			if(Content.ContainsKey(lang)) return Content[lang];
			else return string.Empty;
		}
	}

	public void DropLanguage(string languageCode)
	{
		Content.Remove(languageCode);
	}
	
	public string GetText(string languageCode)
	{
		if(Content.ContainsKey(languageCode)) return Content[languageCode];
		else return string.Empty;
	}

	public void SetText(string languageCode, string content)
	{
		Content[languageCode] = content;
	}
}
