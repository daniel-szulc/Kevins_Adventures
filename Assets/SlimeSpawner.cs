using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject SlimePrefab;
    void Start()
    {
        StartCoroutine(NewSlime());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator NewSlime()
    {
        while (true)
        {
            if (!GetComponentInChildren<SlimeCollect>() && !GetComponentInChildren<Coin>() )
            {
                GameObject _slime = (GameObject) Instantiate(SlimePrefab, transform.position, transform.rotation);
                StartCoroutine(SlimeGrowing(_slime));
            }
            yield return new WaitForSeconds(18);
        }
    }

    IEnumerator SlimeGrowing(GameObject slime)
    {
        Vector2 scale = slime.transform.localScale;
        slime.transform.localScale=new Vector3(0, 0);
        while (slime.transform.localScale.x<scale.x)
        {
            slime.transform.localScale += new Vector3(scale.x / 100, scale.y / 100, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        slime.transform.localScale = scale;
    }
}
