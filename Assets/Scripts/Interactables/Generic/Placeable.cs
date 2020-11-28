using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    #region globals
    private GameObject copy;
    private Quaternion originalRot;
    #endregion

    public IEnumerator OnGrab()
    {
        copy = Instantiate(this.gameObject);

        Component[] copyComponents = copy.GetComponents(typeof(Component));
        foreach (Component comp in copyComponents)
        {
            if (comp.GetType() != typeof(Transform) &&
                comp.GetType() != typeof(MeshRenderer) &&
                comp.GetType() != typeof(MeshFilter)) Destroy(comp);
        }

        copy.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Material[] copyMats = copy.GetComponent<Renderer>().materials;
        foreach (Material mat in copyMats)
        {
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            Color oldColor = mat.color;
            Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, .3f);
            mat.SetColor("_Color", newColor);
        }

        originalRot = this.transform.rotation;

        while (Hand.Instance.gameObject != null)
        {
            RaycastHit hit;
            Vector3 placePos = Vector3.up * 10000;

            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                placePos = new Vector3(hit.point.x, hit.point.y + .1f, hit.point.z);
            }

            copy.transform.position = placePos;

            copy.transform.rotation = Quaternion.Euler(new Vector3(originalRot.eulerAngles.x, this.transform.rotation.eulerAngles.y, originalRot.eulerAngles.z));
            yield return null;
        }
    }

    public void OnDrop()
    {
        Destroy(copy);
    }
}
