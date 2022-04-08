using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CoolToggle : MonoBehaviour
{
	public enum ToggleType
	{
		Select,
		Hide
	}

#pragma warning disable 0649

	[field: SerializeField] public ToggleType Type { get; private set; }

#pragma warning restore 0649

}