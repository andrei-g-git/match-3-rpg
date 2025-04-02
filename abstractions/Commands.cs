
using System;
using System.Collections.Generic;

namespace Abstractions{
	public interface Commandable{
		public void Execute();
	}

	public interface Invokable{
		public List<Commandable> CommandQueue {get;}
		public void AddCommand(Commandable command);
		public void ExecuteAll();
	}

	// public interface Invokable{
	// 	public List<Action> CommandQueue {get;}
	// 	public void AddCommand(Action command);
	// 	public void ExecuteAll();		
	// }
}
