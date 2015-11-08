using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {
	public int NumberofCircles = 1;
	public float innerRadius = 2f;
	public float sideResolution = 1f;
	public float steping = 1f;


	// Use this for initialization
	void Start () {
		//BuildCirclebySides(26,2f);
		BuildConcentricCircles(NumberofCircles,innerRadius,sideResolution,steping);

	}
	


	void BuildCirclebySides(int _nSides, float _sideSize){

		//YES I KNOW, nSides sounds weird for a function that are buidling a circle. It is actually a Polygon, just deal with that.
		int nSides = _nSides;
		float sideSize = _sideSize;

		GameObject circle  = new GameObject("Circle"); //The parent GO which will hold the sides of our circle.
		GameObject pivot = new GameObject("Pivot"); // To avoid overcomplicated quaternion operations and trigonometrics, to rotate
		// the sides of the "circle" we are simply parenting it to this pivot GameObject and than rotating it.

		for (int i = 0; i < nSides; i++){ 
			
			float angle = 360f / nSides; // get the rotation angle by the number of sides
			
			float centerDistance = (sideSize / 2) / (Mathf.Tan(angle / 2 * Mathf.Deg2Rad)); /*distance necessary to the number of
			pieces ofthe given size touch each other forming the "cirlce"*/
			
			GameObject current = GameObject.CreatePrimitive(PrimitiveType.Cube); /* We are using a Primitive Cube here in this
			prototype but nothing is holding us to use any model here in the future */

			current.transform.localScale = new Vector3(sideSize,1f,0.2f); //TODO create a polymorphic method which takes all dimensions.
			current.transform.Translate (0f,0f,centerDistance); //M
			current.transform.SetParent(pivot.transform); 
			pivot.transform.Rotate(Vector3.up,angle * i);
			current.transform.parent = circle.transform; //Parent the side to the object we are building
			pivot.transform.rotation = Quaternion.identity; //Reset pivot rotation(because we used Rotate method)
		}

		GameObject.Destroy(pivot);

	}

	void BuildCircleByRadiusAndSideSize(float radius, float _sideSize){

		float angle = Mathf.Atan((_sideSize ) / radius) * Mathf.Rad2Deg;

		print ("angle: " + angle);
		int nSides = Mathf.RoundToInt(360f / angle);
		print (nSides);

		BuildCirclebySides(nSides,_sideSize);

	}

	void BuildConcentricCircles(int numberOfCircles, float _innerRadius, float sideSize, float spacing){

		for (int i = 0; i < numberOfCircles; i++ )
			BuildCircleByRadiusAndSideSize(_innerRadius + (i * spacing), sideSize);

	}
}
