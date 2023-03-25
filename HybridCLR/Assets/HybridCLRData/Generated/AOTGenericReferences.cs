public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ constraint implement type
	// }} 

	// {{ AOT generic type
	//System.Collections.Generic.Dictionary`2<System.Object,System.Object>
	//System.Collections.Generic.Dictionary`2/Enumerator<System.Object,System.Object>
	//System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>
	// }}

	public void RefMethods()
	{
		// System.Object UnityEngine.GameObject::AddComponent<System.Object>()
		// System.Object UnityEngine.GameObject::GetComponent<System.Object>()
		// System.Object UnityEngine.Resources::Load<System.Object>(System.String)
	}
}