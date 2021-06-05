using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
	public GameObject Coin;
	
	// Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("SpawnItem", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void SpawnItem()
	{
		int randomX = Random.Range(0, 14);
		int randomZ = Random.Range(0, 14);

		while((5<=randomX && randomX<=8)&&(5<=randomZ && randomZ <=8))
		{
			randomX = Random.Range(0, 14);
			randomZ = Random.Range(0, 14);
		}

		GameObject coin = Instantiate(Coin, new Vector3(((float)randomX * 5.65f + 5.65f / 2f), 0.2f, ((float)randomZ * 5.65f + 5.65f / 2f)), Quaternion.identity);
	}
}
