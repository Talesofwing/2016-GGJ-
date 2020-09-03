using UnityEngine;
using System.Collections;

public enum MUSIC_TYPE
{
	acient,
	brother,
	cured,
	diary,
	father,
	hometown,
	hospital,
	humman,
	lab,
	monster,
	mountain,
	pole,
	sex_change,
	snow_pet,
	start,
	title,
	village,
	zombie
}
public class AudioManager : MonoBehaviour {
	public AudioSource m_audio_source;
	public static readonly string MUSIC_PATH = "music/";
	public void Init()
	{
		m_audio_source = transform.gameObject.AddComponent<AudioSource>();
		m_audio_source.loop = true;
	}
	public void PlayMusic(MUSIC_TYPE music)
	{
		if (m_audio_source.isPlaying)
		{
			m_audio_source.Stop();
		}
		string path = MUSIC_PATH + music.ToString();
		AudioClip clip = (AudioClip)Resources.Load(path, typeof(AudioClip));
		m_audio_source.clip = clip;
		m_audio_source.Play();
	}

}
