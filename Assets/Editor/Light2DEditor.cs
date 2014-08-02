using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Light2D)), CanEditMultipleObjects]
public class Light2DEditor : Editor
{
	/*
	public LightType2D lightType;
	public float range;
	public float intensity;
	public Color colour;
	public float angle;
	*/

	public SerializedProperty
		p_lightType,
		p_range,
		p_intensity,
		p_colour,
		p_angle,
		p_isStatic,
		p_aMax,
		p_aMin,
		p_a;

	void OnEnable ()
	{
		p_lightType = serializedObject.FindProperty("lightType");
		p_range = serializedObject.FindProperty("range");
		p_intensity = serializedObject.FindProperty("intensity");
		p_colour = serializedObject.FindProperty("colour");
		p_angle = serializedObject.FindProperty("angle");
		p_isStatic = serializedObject.FindProperty("isStatic");
		p_aMax = serializedObject.FindProperty("aMax");
		p_aMin = serializedObject.FindProperty("aMin");
		p_a = serializedObject.FindProperty("a");
	}
	
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		EditorGUILayout.PropertyField(p_lightType);
		Light2D.LightType2D lt = (Light2D.LightType2D)p_lightType.enumValueIndex;
		
		switch(lt)
		{
		case Light2D.LightType2D.Point:
			EditorGUILayout.PropertyField(p_range, new GUIContent("Range"));
			EditorGUILayout.PropertyField(p_intensity, new GUIContent("Intensity"));
			EditorGUILayout.PropertyField(p_colour, new GUIContent("Colour"));
			EditorGUILayout.PropertyField(p_isStatic, new GUIContent("Is Static"));
			break;
			
		case Light2D.LightType2D.Spot:
			EditorGUILayout.PropertyField(p_range, new GUIContent("Range"));
			EditorGUILayout.PropertyField(p_intensity, new GUIContent("Intensity"));
			EditorGUILayout.PropertyField(p_colour, new GUIContent("Colour"));
			EditorGUILayout.PropertyField(p_angle, new GUIContent("Angle"));
			EditorGUILayout.PropertyField(p_isStatic, new GUIContent("Is Static"));
			EditorGUILayout.PropertyField(p_aMax, new GUIContent("A Max"));
			EditorGUILayout.PropertyField(p_aMin, new GUIContent("A Min"));
			EditorGUILayout.PropertyField(p_a, new GUIContent("A"));
			break;
			
		case Light2D.LightType2D.UNIMPLEMENTED:
			break;
		}

		serializedObject.ApplyModifiedProperties();
	}
}