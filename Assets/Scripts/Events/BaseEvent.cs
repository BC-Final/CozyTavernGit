using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BaseEvent", menuName = "Scriptable Objects/Events/BaseEvent")]
public class BaseEvent : ScriptableObject
{
	private Action _action = delegate { };

	public void Invoke() {
		_action.Invoke();
	}

	public void Register(Action pAction) { 
		_action += pAction;
	}

	public void Unregister(Action pAction) {
		_action -= pAction;
	}
}
