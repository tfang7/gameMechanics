﻿using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
    public Transform[] sun;
    public float dayCycleInMinutes = 1;

    private const float SECOND = 1;
    private const float MINUTE = 60 * SECOND;
    private const float HOUR = 60 * MINUTE;
    private const float DAY = 24 * HOUR;

    private const float DEGREES_PER_SECOND = 360 / DAY;
    private float _degreeRotation;
    private float _timeOfDay;
    private float _gameTimeLength = 250.0f;
    // Use this for initialization
    void Start () {
        _timeOfDay = 0;
        _degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInMinutes * MINUTE);

	   
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < sun.Length; i++)
        {
            sun[i].Rotate(new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
            
        }
        _timeOfDay += Time.deltaTime;
    }
    public float getTime()
    {
        return _timeOfDay;
    }
    public float getGameLength()
    {
        return _gameTimeLength;
    }
}
