using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCache {
	public enum Type
	{
		Recycle = 0,
		FixedLimit,
	}
	
	public GameObject _prefab = null;
	public int _cacheSize = 10;
	public Type _type = Type.Recycle;
	
	GameObject[] _objects;
	public void Generate()
	{
		_objects = new GameObject[_cacheSize ];
		
		for(int i = 0; i < _cacheSize; ++i)
		{
			_objects[i] = GameObject.Instantiate(_prefab) as GameObject;
			_objects[i].name = _prefab.name + "_"+i;
			//_objects[i].transform
			//////////////////////////////
		}
	}
	
	int _lastCachedIndex = 0;
	public GameObject GetNextObject()
	{
		GameObject obj = null;
		for (int i = 0; i < _cacheSize; ++i)
		{
			obj = _objects[_lastCachedIndex];
			
			++_lastCachedIndex;
			_lastCachedIndex %= _cacheSize;
			
			if(obj.activeSelf == false)
				break;
		}
		if(obj.activeSelf)
		{
			if(_type == Type.Recycle)
			{
				obj = _objects[_lastCachedIndex];
				
				++_lastCachedIndex;
				_lastCachedIndex %= _cacheSize;
				
				Spawner.Disable(obj);
			}
			else if(_type == Type.FixedLimit)
			{
				return null;
			}
		}
		return obj;		
	}
	
	public void Initialize()
	{
		for (int i = 0; i < _cacheSize; ++i) {
			_objects [i].SetActive (false);
		}
	}
}
