using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/int")]
public class IntVariable : ScriptableObject,ISerializationCallbackReceiver
{
	public int InitialValue;

	[NonSerialized] public int Value;

	public void OnAfterDeserialize()
	{
		Value = InitialValue;
	}

	public void OnBeforeSerialize()
	{

	}
}
