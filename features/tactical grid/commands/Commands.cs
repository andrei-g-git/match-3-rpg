using Abstractions;
using Godot;
using Godot.Collections;

namespace Grid{
	public partial class Commands{
		public class MarkingForCollapse : Commandable{
			private Array<Vector2I> matches;
			private Array<Vector2I> storedByModel;
			public MarkingForCollapse(Array<Vector2I> matches_, Array<Vector2I> storedByModel_){
				matches = matches_; 
				storedByModel = matches_; //this is really, really stupid and useless but Function currying in c# is even worse...
			}
            public void Execute(){}
        }

		//public class 
	}
}
