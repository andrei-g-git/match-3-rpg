using System.Linq;
using Godot;
using Godot.Collections;
//using System;

public partial class Collections {
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
	}

	public static Array<T> RemoveDuplicates<[MustBeVariant]T>(Array<T> array_){
		var cSharpArray = array_.Distinct().ToArray();
		return new Array<T>(cSharpArray);
	}

	public static Array<Array<T>> Create2DArray<[MustBeVariant]T>(int width, int height){
		Array<Array<T>> newArray = new Array<Array<T>>();
		newArray.Resize(width);
		for(int x = 0; x < width; x++){
			newArray[x].Resize(height);
		}
		return newArray;
	}

	public class FixedSizeArray<[MustBeVariant]T>{
		private Array _array = [];
		private int maxSize;

		public FixedSizeArray(int maxSize){
			this.maxSize = maxSize;
		}

		public void Add(Variant item){
			if(_array.Count < maxSize){
				_array.Add(item);
			}
		}
	}
}
