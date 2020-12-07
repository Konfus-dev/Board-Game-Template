
using UnityEngine;

// Very simple script to allow mouse clicks to turn pages

public class BookInteraction : MonoBehaviour
{
	public MegaBookBuilder book;

	public AudioSource bookAudioEmitter;
	public AudioClip[] bookPageTurnSounds;

	public Collider			prevcollider;
	public Collider			nextcollider;

    private void Update()
	{
		if (book.page == -1) prevcollider.tag = "Untagged";
		else prevcollider.tag = "Clickable";

		if (book.page == book.pages.Count + 1) nextcollider.tag = "Untagged";
		else nextcollider.tag = "Clickable";

		if (book && prevcollider && nextcollider && Input.GetButtonDown("Left Interact"))
		{
			RaycastHit	hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if (hit.collider == prevcollider && prevcollider.CompareTag("Clickable"))
				{
					
					PlaySound();
					book.PrevPage();
				}

				if (hit.collider == nextcollider && nextcollider.CompareTag("Clickable"))
				{
					PlaySound();
					book.NextPage();
				}
			}
		}
	}

	private void PlaySound()
	{
		bookAudioEmitter.Stop();

		int randomSound = Random.Range(0, bookPageTurnSounds.Length);
		AudioClip clip = bookPageTurnSounds[randomSound];
		bookAudioEmitter.pitch = Random.Range(1f, 1.2f);
		bookAudioEmitter.volume = Random.Range(0.4f, 0.5f);
		bookAudioEmitter.PlayOneShot(clip);
	}
}