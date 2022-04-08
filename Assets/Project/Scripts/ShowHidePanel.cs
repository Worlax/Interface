using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ShowHidePanel : MonoBehaviour
{
#pragma warning disable 0649

	[SerializeField] RectTransform panel;

#pragma warning restore 0649

	Toggle toggle;

	// Events
	private void ValueChanged(bool value)
	{
		panel.gameObject.SetActive(value);
	}

	// Unity
	private void Start()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(ValueChanged);
	}
}