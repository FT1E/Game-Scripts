using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class CustomSplineGenerator : EditorWindow
{
    // parameters 
    // spacing between knots
    // TODO - generalize it so user can pick along which plane the spline to be drawn (xy, xz, yz) in the future if needed]
    private float spacingX = 1f; 
    private float spacingZ = 1f; 
    
    // grid size - width, length - along x, z scale in unity
    private int width = 1;
    private int length = 1;

    // height pos (y in unity transform) of knots - every knot has same y value
    private float height = 0f;

    // spline object - to which the spline is drawn
    private SplineContainer spline_container;
    // end parameters     


    private string invalid_response = "";


    [MenuItem("My Tools/Custom Spline Generator")]
    public static void ShowWindow()
    {
        CustomSplineGenerator wnd = GetWindow<CustomSplineGenerator>();
        wnd.titleContent = new GUIContent("Custom Spline Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Random Grid Spline Generator", EditorStyles.boldLabel);

        spline_container = EditorGUILayout.ObjectField("Scene Spline Container", spline_container, typeof(SplineContainer), true) as SplineContainer;

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Spacing", EditorStyles.boldLabel);
        spacingX = EditorGUILayout.FloatField("X", spacingX);
        spacingZ = EditorGUILayout.FloatField("Z", spacingZ);

        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField("Grid size", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField("Width (X)", width);
        length = EditorGUILayout.IntField("Length (Z)", length);

        EditorGUILayout.Space(5);
        height = EditorGUILayout.FloatField("Height (Y) of spline", height);

        EditorGUILayout.Space(5);
        if (GUILayout.Button("Generate Spline")) {
            GenerateGridSpline();
        }

        EditorGUILayout.Space(5);
        if (GUILayout.Button("Clear Splines"))
        {
            for(int i = spline_container.Splines.Count - 1; i >=0; i--) {
                spline_container.RemoveSplineAt(i);
            }
        }


        GUIStyle style = new GUIStyle(EditorStyles.textArea);
        style.wordWrap = true;
        GUI.color = Color.red;
        EditorGUILayout.Space(5);
        EditorGUILayout.LabelField(invalid_response, style);
    }

    private void GenerateGridSpline() {
        
        // parameter validation
        // splineobj - check if null and if it's a scene object
        if (spline_container == null || EditorUtility.IsPersistent(spline_container)) {
            invalid_response = "You need to select a game object with the Spline Container component from the scene";
            return;
        }
        // width, length - positive
        if (width == 0 || length == 0) {
            invalid_response = "Can't have generate a grid with width or length 0";
            return;
        }

        // spacing - different than 0
        if (spacingX == 0 || spacingZ == 0) {
            invalid_response = "Need to set spacing different than 0";
            return;
        }
        // allowing for negatives in order the grid to expand in a different direction

        // height - no restrictions

        invalid_response = "";
        // end parameter validation

        bool[,] right_link = new bool[length + 2, width + 2];
        bool[,] bot_link = new bool[length + 2, width + 2];

        // randomize the grid
        System.Random random = new System.Random();
        double r;
        for (int i = 0; i < length - 1; i++) {
            for (int j = 0; j < width - 1; j++) {
                r = random.NextDouble();
                if (r <= 0.1)
                {
                    continue;   // no r-b links
                }
                else if (r <= 0.7)
                {
                    // both r-b links
                    right_link[i, j] = true;
                    bot_link[i, j] = true;
                }
                else if (r <= 0.85)
                {
                    // only r link
                    right_link[i, j] = true;
                }
                else {
                    // only b link
                    bot_link[i, j] = true;
                }
            }
        }

        
        // create the spline
        /*float3 knot_pos = float3.zero;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length; j++)
            {
                knot_pos.x = i * spacingX;
                knot_pos.z = j * spacingZ;
                grid_spline.Add(knot_pos, TangentMode.Linear);
            }
        }*/
        // instead of above - traverse the matrix through dfs and draw multiple splines for each depth
        Stack<Vector2Int> dfs = new Stack<Vector2Int>();


        for (int i = length - 1; i >= 0; i--) {
            for (int j = width - 1; j >= 0; j--) {
                dfs.Push(new Vector2Int(j, i));
            }
        }

        Spline grid_spline = null;
        Vector2Int current;
        Vector2Int previous = new Vector2Int(-1, -1);
        float3 knot_pos = float3.zero;
        while (dfs.Count > 0) {

            if (dfs.Count > length * width * 5)
            {
                Debug.Log("Likely an infinite loop!");
                break;  // so I don't get stuck in an infinite loop
            }

            if (grid_spline != null && grid_spline.Count > 1)
            {
                spline_container.AddSpline(grid_spline);
            }
            grid_spline = new Spline(width * length, false);


            current = dfs.Pop();

            // if current - previous is at distance 1 in manhattan add a knot, and remove the link
            // else add the spline so far only if it has at least 2 knots, and make a new spline and add a knot


            while (right_link[current.y, current.x] || bot_link[current.y, current.x]) {
                

                knot_pos.z = current.y * spacingZ;
                while (right_link[current.y, current.x])
                {
                    current.x += 1;
                    knot_pos.x = current.x * spacingX;
                    grid_spline.Add(knot_pos, TangentMode.Linear);

                    // set it to false, so no other spline goes along this unit line again
                    right_link[current.y, current.x] = false;
                }

                knot_pos.x = current.x * spacingX;
                while (bot_link[current.y, current.x]) {
                    current.y += 1;
                    knot_pos.z = current.y * spacingZ;
                    grid_spline.Add(knot_pos, TangentMode.Linear);


                    // set it to false, so no other spline goes along this unit line again
                    bot_link[current.y, current.x] = false;
                }
            }

            previous = current;
            
        }
        
        spline_container.AddSpline(grid_spline);
    }


}
