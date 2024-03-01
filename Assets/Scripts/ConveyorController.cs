using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{
    [SerializeField] private int direction = 1;
    [SerializeField] private float length = 50;
    [SerializeField] private float speed = 1;

	public Material red;
	public Material yellow;

	public GameObject pItem;

    private ArrayList itemObjects;

    // Start is called before the first frame update
    void Start()
    {
        itemObjects = new ArrayList();

        //change conveyor length
        this.gameObject.transform.localScale = new Vector3(length, 0.5f, 5f);
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "item")
		{
            collision.gameObject.GetComponent<ItemController>().ChangeState(Constants.STATE_IN_CONVEYOR);
            itemObjects.Add(collision.gameObject);
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "item")
		{
            collision.gameObject.GetComponent<ItemController>().ChangeState(Constants.STATE_OUT_CONVEYOR);
            itemObjects.Remove(collision.gameObject);
        }
	}

	private void Update()
	{
		for (int i = 0; i < itemObjects.Count; i++)
		{
			Vector3 cPos = (itemObjects[i] as GameObject).transform.position;
			cPos += new Vector3(speed * direction, 0f, 0f) * Time.deltaTime;
			(itemObjects[i] as GameObject).transform.position = cPos;
		}

		if (direction > 0)
		{
			this.GetComponent<MeshRenderer>().material = red;
		}
		else
		{
			this.GetComponent<MeshRenderer>().material = yellow;
		}
	}

	public void AddNewObject()
	{
		GameObject newItem = Instantiate(pItem);
		newItem.transform.position = new Vector3(0f, 8f, 0f);
	}

	public void Reverse()
	{
		direction = -1 * direction;
	}

	public void Stop()
	{
		if (direction != 0)
		{
			direction = 0;
		}
		else
		{
			direction = 1;
		}
	}

	public void KickItem()
	{
		int random = Random.Range(0, itemObjects.Count);
		if (random >= itemObjects.Count) random = 0;
		GameObject item = (itemObjects[random] as GameObject);

		item.GetComponent<ItemController>().ChangeState(Constants.STATE_OUT_CONVEYOR);
		itemObjects.Remove(item);

		item.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 10f, 500f));
	}

}
