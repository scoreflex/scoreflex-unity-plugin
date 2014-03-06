using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DictionaryUtils {

	public static object getValueForKey(IDictionary dictionary, string key, object defaultValue) { 
		if (dictionary.Contains(key)) {

			return dictionary[key];
		}
		return defaultValue;
	}

	public static long getValueForKey(IDictionary dictionary, string key, long defaultValue) { 
		if (dictionary.Contains(key)) {
			
			return (long)dictionary[key];
		}
		return defaultValue;
	}

	public static int getValueForKey(IDictionary dictionary, string key, int defaultValue) { 
		if (dictionary.Contains(key)) {
			
			return (int)dictionary[key];
		}
		return defaultValue;
	}

	public static bool getValueForKey(IDictionary dictionary, string key, bool defaultValue) { 
		if (dictionary.Contains(key)) {
			
			return (bool)dictionary[key];
		}
		return defaultValue;
	}
}
