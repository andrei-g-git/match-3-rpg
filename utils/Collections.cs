using Godot;
using Godot.Collections;
using System;

public partial class Collections 
{
public static Array<Array<int>> Change2dStringArrayToInt(Array<Array<string>> stringArray){
	int width = stringArray.Count;
	int height = stringArray[0].Count;
	var intArray = new Array<Array<int>>();
	intArray.Resize(width);
	for(int x = 0; x < width; x++){
		intArray[x].Resize(height);
		for(int y = 0; y < height; y++){
			intArray[x][y] = stringArray[x][y].ToInt();
		}		
	}
	return intArray;

	// int_array.resize(width)
	// for x in width:
	// 	int_array[x].resize(height)
	// 	for y in height:
	// 		int_array[x][y] = string_array[x][y].to_int()
	// return int_array	
}

}
