using System.Diagnostics.CodeAnalysis;
using UnityEngine;


// We can use this Helper Class to define any class we want to use which might have a singleton.
// Awesome for easy static instance referencing like managers
public class SingletonBuilder<T> : MonoBehaviour where T : Component
{
	private static T _instance;

	[SuppressMessage("ReSharper", "InvertIf")]
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				// Go for all instances of this class on the scene and see what to do about it
				var instances = FindObjectsOfType<T>();
				// There's just one instance of this class on the scene. GOOD
				if (instances.Length == 1)
					_instance = instances[0];
				// There's many objects of this type on the scene. BAD
				else if (instances.Length > 1)
					Debug.LogError(typeof(T) + ": There is more than 1 instance in the scene.");
				else
					// There's no objects of this type on the scene. BAD
					Debug.LogError(typeof(T) + ": Instance doesn't exist in the scene.");
			}
			// Return what we found (if anything)
			return _instance;
		}
	}
}