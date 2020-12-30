﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
	public bool showController = false;
	public InputDeviceCharacteristics controllerCharacteristics;
	public List<GameObject> controllerPrefabs;
	public GameObject handModelPrefab;

	private InputDevice targetDevice;
	private GameObject spawnedController;
	private GameObject spawnedHandModel;
	private Animator handAnimator;

	// Start is called before the first frame update
	void Start()
	{
		TryInitialize();
	}

	void TryInitialize()
    {
		List<InputDevice> devices = new List<InputDevice>();
		InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

		foreach (var item in devices)
		{

		}

		if (devices.Count > 0)
		{
			targetDevice = devices[0];
			GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
			if (prefab)
			{
				spawnedController = Instantiate(prefab, transform);
			}
			else
			{
				Debug.Log("Did not find corresponding controller model");
				spawnedController = Instantiate(controllerPrefabs[0], transform);
			}

			spawnedHandModel = Instantiate(handModelPrefab, transform);
			handAnimator = spawnedHandModel.GetComponent<Animator>();
		}

	}

	void UpdateHandAnimation()
    {
		if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
			handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
			handAnimator.SetFloat("Trigger", 0.0f);
		}

		if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
		{
			
			handAnimator.SetFloat("Grip", gripValue);
		}
		else
		{
			handAnimator.SetFloat("Grip", 0.0f);
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (!targetDevice.isValid)
		{
			TryInitialize();
		}
		else
		{
			if (showController)
			{
				spawnedHandModel.SetActive(false);
				spawnedController.SetActive(true);
			}
			else
			{
				spawnedHandModel.SetActive(true);
				spawnedController.SetActive(false);
				UpdateHandAnimation();
			}
		}
	}
}