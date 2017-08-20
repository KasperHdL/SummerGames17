using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Drop : MonoBehaviour {
    private Image img;
    private float a, size;

	// Use this for initialization
	void Start ()
    {
        img = GetComponent<Image>();

        a = 0f;
        size = 0.7f;

        Color c = img.color;
        c.a = a;
        img.color = c;

        StartCoroutine(Fadein());
        StartCoroutine(Zoomin());
	}

    IEnumerator Zoomin()
    {
        yield return new WaitForSeconds(1.5f);

        while (size < 1f)
        {
            size += Time.deltaTime * 0.05f;

            Vector3 t = transform.localScale;
            t = new Vector3(size, size, size);
            transform.localScale = t;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Fadein()
    {
        yield return new WaitForSeconds(1.5f);

        while (a < 1f)
        {
            a += Time.deltaTime * 0.5f;

            Color c = img.color;
            c.a = a;
            img.color = c;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
