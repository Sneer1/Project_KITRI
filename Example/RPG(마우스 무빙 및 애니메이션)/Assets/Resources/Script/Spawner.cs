using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	static Spawner _instance;
	
	Dictionary<GameObject, ObjectCache> _caches = new Dictionary<GameObject, ObjectCache>();
	
	void Awake()
	{
		if (_instance == null)
			_instance = this;
		else
			Destroy (gameObject);
	}
	
	public static bool Register(GameObject prefab, int cacheSize, ObjectCache.Type type)
	{
		if (_instance == null)
			return false;
		
		if (prefab == null)
			return false;
		
		if (_instance._caches.ContainsKey (prefab) == false) {
			ObjectCache cache = new ObjectCache ();
			cache._prefab = prefab;
			cache._cacheSize = cacheSize;
			cache._type = type;
			
			_instance._caches.Add(prefab, cache);
			
			return true;
		} else {
			return true;
		}
	}
	
	Hashtable	_activateCachedObjects;
	
	public static GameObject Spawn(GameObject prefab)
	{
		if (prefab == null)
			return null;
		
		if (_instance == null)
			return null;
		
		if(_instance._caches.ContainsKey (prefab))
		{
			GameObject obj = _instance._caches[prefab].GetNextObject();
			if( obj == null)
				return null;
			
			obj.SetActive(true);
			_instance._activateCachedObjects[obj] = true;
			
			return obj;
		}
		
		return Instantiate(prefab) as GameObject;
	}

	public static void Disable(GameObject objectToDestroy)
	{
		if (objectToDestroy == null)
			return;
		
		if (_instance == null)
			return;
		
		if (_instance._activateCachedObjects.Contains (objectToDestroy) == true) {
			if ((bool)_instance._activateCachedObjects [objectToDestroy] == true) {
				objectToDestroy.SetActive (false);
				_instance._activateCachedObjects [objectToDestroy] = false;
			}
			return;
		}
		
		GameObject.Destroy (objectToDestroy);
	}

	static bool _isInitialized = false;
	public static void Init()
	{
				if (_instance == null)
						return;

				if (_isInitialized == false) {
						_isInitialized = true;
						_instance.StartCoroutine ("RegistCache");
				}
		}

	IEnumerator RegistCache()
	{
		int amount = 0;
		foreach (ObjectCache cache in _instance._caches.Values)
		{
			yield return new WaitForEndOfFrame();

			cache.Generate ();

			yield return new WaitForFixedUpdate();

			cache.Initialize();

			amount += cache._cacheSize;
		}
		_instance._activateCachedObjects= new Hashtable(amount);
	}
}
