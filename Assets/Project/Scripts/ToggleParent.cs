using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleParent : MonoBehaviour
{
	public enum ToggleGroupState
	{
		Normal,
		Hybrid,
		FullCheck
	}

#pragma warning disable 0649

	[SerializeField] Toggle toggle;
	[SerializeField] CoolToggle.ToggleType childType;
	[SerializeField] Image hybridCheckmark;
	[SerializeField] Image fullCheckCheckmark;

#pragma warning restore 0649

	ToggleGroupState state;
	List<Toggle> childToggles = new List<Toggle>();

	private void UpdateStatus()
	{
		int checkedToggles = CheckedToggles();
		hybridCheckmark.enabled = false;
		fullCheckCheckmark.enabled = false;

		if (checkedToggles == 0)
		{
			state = ToggleGroupState.Normal;
			toggle.SetIsOnWithoutNotify(false);
		}
		else if (checkedToggles < childToggles.Count)
		{
			state = ToggleGroupState.Hybrid;
			toggle.SetIsOnWithoutNotify(true);
			toggle.graphic = hybridCheckmark;
			hybridCheckmark.enabled = true;
		}
		else
		{
			state = ToggleGroupState.FullCheck;
			toggle.SetIsOnWithoutNotify(true);
			toggle.graphic = fullCheckCheckmark;
			fullCheckCheckmark.enabled = true;
		}
	}

	private int CheckedToggles()
	{
		int checkedToggles = 0;
		foreach (Toggle toggle in childToggles)
		{
			if (toggle.isOn) ++checkedToggles;
		}

		return checkedToggles;
	}

	// Events
	private void ToggleValueChanged(bool value)
	{
		switch (state)
		{
			case ToggleGroupState.Normal:
			case ToggleGroupState.Hybrid:
				childToggles.ForEach(x => x.SetIsOnWithoutNotify(true));
				break;

			case ToggleGroupState.FullCheck:
				childToggles.ForEach(x => x.SetIsOnWithoutNotify(false));
				break;
		}

		UpdateStatus();
	}

	private void ChildValueChanged(bool value)
	{
		UpdateStatus();
	}

	// Unity
	private void Start()
	{
		toggle.onValueChanged.AddListener(ToggleValueChanged);

		foreach (CoolToggle coolToggle in FindObjectsOfType<CoolToggle>())
		{
			if (coolToggle.Type != childType) continue;

			Toggle toggle = coolToggle.GetComponent<Toggle>();
			toggle.onValueChanged.AddListener(ChildValueChanged);
			childToggles.Add(toggle);
		}

		UpdateStatus();
	}
}