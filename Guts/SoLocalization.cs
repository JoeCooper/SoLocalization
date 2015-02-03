﻿using UnityEngine;
using System.Collections.Generic;

public static class SoLocalization {

	private const string PreferredLanguageKey = "SoLocalizationPreferredLanguage";
	
	private static string _PreferredLanguage = null;
	public static string PreferredLanguage { get {
			if(string.IsNullOrEmpty(EditorLanguage) == false) {
				return EditorLanguage;
			}
			var languages = SoLocalizationText.AllLanguages;
			if(string.IsNullOrEmpty(_PreferredLanguage) || System.Array.IndexOf(languages, _PreferredLanguage) == -1)
			{
				PlayerPrefs.GetString(PreferredLanguageKey, null);
			}
			if(string.IsNullOrEmpty(_PreferredLanguage) || System.Array.IndexOf(languages, _PreferredLanguage) == -1)
			{
				IsoCodeBySystemLanguage.TryGetValue(Application.systemLanguage, out _PreferredLanguage);
			}
			if(string.IsNullOrEmpty(_PreferredLanguage) || System.Array.IndexOf(languages, _PreferredLanguage) == -1)
			{
				if(languages.Length > 0) _PreferredLanguage = languages[0];
			}
			return _PreferredLanguage;
		}
		set {
			_PreferredLanguage = value;
			PlayerPrefs.SetString(PreferredLanguageKey, _PreferredLanguage);
		}
	}

	public static string EditorLanguage { get; set; }
	
	public static string GetLanguageDisplayName(string isoCode)
	{
		string result = "Unknown Language";
		if(isoCode != null) LanguageNameByIsoCode.TryGetValue(isoCode, out result);
		return result;
	}
	
	public static string[] AllLanguageCodes {
		get {
			string[] allCodes = new string[LanguageNameByIsoCode.Count];
			LanguageNameByIsoCode.Keys.CopyTo(allCodes, 0);
			return allCodes;
		}
	}
	
	private readonly static Dictionary<string,string> LanguageNameByIsoCode = new Dictionary<string, string>() {
		{"aa", "Afar"},
		{"ab", "Abkhazian"},
		{"ae", "Avestan"},
		{"af", "Afrikaans"},
		{"ak", "Akan"},
		{"am", "Amharic"},
		{"an", "Aragonese"},
		{"ar", "Arabic"},
		{"as", "Assamese"},
		{"av", "Avaric"},
		{"ay", "Aymara"},
		{"az", "Azerbaijani"},
		{"ba", "Bashkir"},
		{"be", "Belarusian"},
		{"bg", "Bulgarian"},
		{"bh", "Bihari"},
		{"bi", "Bislama"},
		{"bm", "Bambara"},
		{"bn", "Bengali"},
		{"bo", "Tibetan"},
		{"br", "Breton"},
		{"bs", "Bosnian"},
		{"ca", "Catalan"},
		{"ce", "Chechen"},
		{"ch", "Chamorro"},
		{"co", "Corsican"},
		{"cr", "Cree"},
		{"cs", "Czech"},
		{"cu", "Church Slavic"},
		{"cv", "Chuvash"},
		{"cy", "Welsh"},
		{"da", "Danish"},
		{"de", "German"},
		{"dv", "Divehi"},
		{"dz", "Dzongkha"},
		{"ee", "Ewe"},
		{"el", "Modern Greek"},
		{"en", "English"},
		{"eo", "Esperanto"},
		{"es", "Spanish"},
		{"et", "Estonian"},
		{"eu", "Basque"},
		{"fa", "Persian"},
		{"ff", "Fulah"},
		{"fi", "Finnish"},
		{"fj", "Fijian"},
		{"fo", "Faroese"},
		{"fr", "French"},
		{"fy", "Western Frisian"},
		{"ga", "Irish"},
		{"gd", "Gaelic"},
		{"gl", "Galician"},
		{"gn", "Guaraní"},
		{"gu", "Gujarati"},
		{"gv", "Manx"},
		{"ha", "Hausa"},
		{"he", "Modern Hebrew"},
		{"hi", "Hindi"},
		{"ho", "Hiri Motu"},
		{"hr", "Croatian"},
		{"ht", "Haitian"},
		{"hu", "Hungarian"},
		{"hy", "Armenian"},
		{"hz", "Herero"},
		{"ia", "Interlingua"},
		{"id", "Indonesian"},
		{"ie", "Interlingue"},
		{"ig", "Igbo"},
		{"ii", "Sichuan Yi"},
		{"ik", "Inupiaq"},
		{"io", "Ido"},
		{"is", "Icelandic"},
		{"it", "Italian"},
		{"iu", "Inuktitut"},
		{"ja", "Japanese"},
		{"jv", "Javanese"},
		{"ka", "Georgian"},
		{"kg", "Kongo"},
		{"ki", "Kikuyu"},
		{"kj", "Kwanyama"},
		{"kk", "Kazakh"},
		{"kl", "Kalaallisut"},
		{"km", "Central Khmer"},
		{"kn", "Kannada"},
		{"ko", "Korean"},
		{"kr", "Kanuri"},
		{"ks", "Kashmiri"},
		{"ku", "Kurdish"},
		{"kv", "Komi"},
		{"kw", "Cornish"},
		{"ky", "Kirghiz"},
		{"la", "Latin"},
		{"lb", "Luxembourgish"},
		{"lg", "Ganda"},
		{"li", "Limburgish"},
		{"ln", "Lingala"},
		{"lo", "Lao"},
		{"lt", "Lithuanian"},
		{"lu", "Luba-Katanga"},
		{"lv", "Latvian"},
		{"mg", "Malagasy"},
		{"mh", "Marshallese"},
		{"mi", "Māori"},
		{"mk", "Macedonian"},
		{"ml", "Malayalam"},
		{"mn", "Mongolian"},
		{"mr", "Marathi"},
		{"ms",	"Malay"},
		{"mt", "Maltese"},
		{"my", "Burmese"},
		{"na", "Nauru"},
		{"nb", "Norwegian Bokmål"},
		{"nd", "North Ndebele"},
		{"ne", "Nepali"},
		{"ng", "Ndonga"},
		{"nl", "Dutch"},
		{"nn", "Norwegian Nynorsk"},
		{"no", "Norwegian"},
		{"nr", "South Ndebele"},
		{"nv", "Navajo"},
		{"ny", "Chichewa"},
		{"oc", "Occitan (after 1500)"},
		{"oj", "Ojibwa"},
		{"om", "Oromo"},
		{"or", "Oriya"},
		{"os", "Ossetian"},
		{"pa", "Panjabi"},
		{"pi", "Pāli"},
		{"pl", "Polish"},
		{"ps", "Pashto"},
		{"pt", "Portuguese"},
		{"qu", "Quechua"},
		{"rm", "Romansh"},
		{"rn", "Rundi"},
		{"ro", "Romanian"},
		{"ru", "Russian"},
		{"rw", "Kinyarwanda"},
		{"sa", "Sanskrit"},
		{"sc", "Sardinian"},
		{"sd", "Sindhi"},
		{"se", "Northern Sami"},
		{"sg", "Sango"},
		{"si", "Sinhala"},
		{"sk", "Slovak"},
		{"sl", "Slovene"},
		{"sm", "Samoan"},
		{"sn", "Shona"},
		{"so", "Somali"},
		{"sq", "Albanian"},
		{"sr", "Serbian"},
		{"ss", "Swati"},
		{"st", "Southern Sotho"},
		{"su", "Sundanese"},
		{"sv", "Swedish"},
		{"sw", "Swahili"},
		{"ta", "Tamil"},
		{"te", "Telugu"},
		{"tg", "Tajik"},
		{"th", "Thai"},
		{"ti", "Tigrinya"},
		{"tk", "Turkmen"},
		{"tl", "Tagalog"},
		{"tn", "Tswana"},
		{"to", "Tonga (Tonga Islands)"},
		{"tr", "Turkish"},
		{"ts", "Tsonga"},
		{"tt", "Tatar"},
		{"tw", "Twi"},
		{"ty", "Tahitian"},
		{"ug", "Uighur"},
		{"uk", "Ukrainian"},
		{"ur", "Urdu"},
		{"uz", "Uzbek"},
		{"ve", "Venda"},
		{"vi", "Vietnamese"},
		{"vo", "Volapük"},
		{"wa", "Walloon"},
		{"wo", "Wolof"},
		{"xh", "Xhosa"},
		{"yi", "Yiddish"},
		{"yo", "Yoruba"},
		{"za", "Zhuang"},
		{"zh", "Chinese"},
		{"zu", "Zulu"}
	};
	
	private static readonly Dictionary<SystemLanguage,string> IsoCodeBySystemLanguage = new Dictionary<SystemLanguage, string> {
		{ SystemLanguage.Afrikaans, "af" },
		{ SystemLanguage.Arabic, "ar" },
		{ SystemLanguage.Basque, "eu" },
		{ SystemLanguage.Belarusian, "be" },
		{ SystemLanguage.Bulgarian, "bg" },
		{ SystemLanguage.Catalan, "ca" },
		{ SystemLanguage.Chinese, "yp" },
		{ SystemLanguage.Czech, "cs" },
		{ SystemLanguage.Danish, "da" },
		{ SystemLanguage.Dutch, "ng" },
		{ SystemLanguage.English, "en" },
		{ SystemLanguage.Estonian, "et" },
		{ SystemLanguage.Faroese, "fo" },
		{ SystemLanguage.Finnish, "fi" },
		{ SystemLanguage.French, "fr" },
		{ SystemLanguage.German, "de" },
		{ SystemLanguage.Greek, "el" },
		{ SystemLanguage.Hebrew, "he" },
		{ SystemLanguage.Hungarian, "hu" },
		{ SystemLanguage.Icelandic, "is" },
		{ SystemLanguage.Indonesian, "id" },
		{ SystemLanguage.Italian, "it" },
		{ SystemLanguage.Japanese, "ja" },
		{ SystemLanguage.Korean, "ko" },
		{ SystemLanguage.Latvian, "lv" },
		{ SystemLanguage.Lithuanian, "lt" },
		{ SystemLanguage.Norwegian, "no" },
		{ SystemLanguage.Polish, "pl" },
		{ SystemLanguage.Portuguese, "pt" },
		{ SystemLanguage.Romanian, "ro" },
		{ SystemLanguage.Russian, "ru" },
		{ SystemLanguage.SerboCroatian, "hr" }, //Uncertain
		{ SystemLanguage.Slovak, "sk" },
		{ SystemLanguage.Spanish, "es" },
		{ SystemLanguage.Swedish, "sv" },
		{ SystemLanguage.Thai, "th" },
		{ SystemLanguage.Turkish, "tr" },
		{ SystemLanguage.Ukrainian, "uk" },
		{ SystemLanguage.Vietnamese, "vi" }
	};
}
