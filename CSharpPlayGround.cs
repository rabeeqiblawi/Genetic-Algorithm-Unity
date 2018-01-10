using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharpPlayGround : MonoBehaviour {

    List<char> things;
    List<char> other_things;
    private void Start()
    {

        things = new List<char>();
        other_things = new List<char>();
        populateList();
        foreach (var item in things)
        {
            other_things.Add(item);
        }
        things.Clear();
        print(other_things[0]);

    }



    void populateList() {
        things.Add('a');
        things.Add('b');
        things.Add('c');
    }
}
