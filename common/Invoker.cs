using Abstractions;
using System.Collections.Generic;

public partial class Invoker: Invokable{
	private List<Commandable> commandQueue = [];
    public List<Commandable> CommandQueue{get => commandQueue;}

	public Invoker(){

	}

    public void AddCommand(Commandable command){
        commandQueue.Add(command);
    }

    public void ExecuteAll(){
        foreach(Commandable command in commandQueue){
			command.Execute();
		}
    }
}
