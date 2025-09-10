using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class ParaEvent <T> : ScriptableObject
{
	private Action<T> _action = delegate { };

	public void Invoke(T pParam)
	{
		_action.Invoke(pParam);
	}

	public void Register(Action<T> pAction)
	{
		_action += pAction;
	}

	public void Unregister(Action<T> pAction)
	{
		_action -= pAction;
	}
}
