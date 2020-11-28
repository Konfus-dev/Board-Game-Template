
using UnityEngine;

// Very simple script to allow mouse clicks to turn pages

public class MegaBookMouseControl : MonoBehaviour
{
	public MegaBookBuilder book;
	public Collider			prevcollider;
	public Collider			nextcollider;

	void Update()
	{
		if (book.page == -1) prevcollider.tag = "Untagged";
		else prevcollider.tag = "Clickable";

		if (book.page == book.pages.Count + 1) nextcollider.tag = "Untagged";
		else nextcollider.tag = "Clickable";

		if (book && prevcollider && nextcollider && Input.GetMouseButtonDown(0))
		{
			RaycastHit	hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if (hit.collider == prevcollider && prevcollider.CompareTag("Clickable"))
					book.PrevPage();

				if (hit.collider == nextcollider && nextcollider.CompareTag("Clickable"))
					book.NextPage();
			}
		}
	}
}