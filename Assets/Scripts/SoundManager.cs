using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource musicsource;
	public AudioSource afxsource;

	public void SetMusicVolume(float volume)
	{
		musicsource.volume=volume;
	}

	public void SetAfxVolume(float volume)
	{
		afxsource.volume=volume;
	}

	public void OnAfx()
	{
		afxsource.Play();
	}
}    

