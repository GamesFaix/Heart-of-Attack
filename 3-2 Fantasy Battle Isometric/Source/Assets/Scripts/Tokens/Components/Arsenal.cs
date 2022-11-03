using System.Collections.Generic;
using UnityEngine;
using System;

namespace HOA {
	public class Arsenal : Group<Task> {
		public Unit Parent {get; private set;}

		public Arsenal (Unit parent) {
			Parent = parent;
			list = new List<Task>();	
		}

		public void Sort () {list.Sort();}

		public void Reset () {foreach (Task task in list) {task.Reset();} }

		public Task Move {
			get {
				foreach (Task task in list) {
					if (task.Weight == 1) {return task;}
				}
				return default(Task);
			}
		}

		public Task Task (string name) {
			foreach (Task task in list) {
				if (task.Name == name) {return task;}
			}
			return default(Task);
		}

		public bool Replace (Task oldTask, Task newTask) {
			if (oldTask == null || newTask == null) {
				throw new Exception("Arsenal.Replace: Null argument.");
			}
			else if (!list.Contains(oldTask)) {
				throw new Exception ("Arsenal cannot replace action.  Does not contain action '"+oldTask.Name+"'.");
			}
			else {
				list.Remove(oldTask);
				list.Add(newTask);
				Sort();
				return true;
			}
		}

		public bool Replace (string name, Task newTask) {
			Task oldTask = Task(name);
			if (oldTask == null) {throw new Exception("Arsenal.Replace: Does not contain action '"+name+"'.");}
			else {return Replace (oldTask, newTask);}
		}

		public bool Remove (string name) {
			Task task = Task(name);
			if (task == null) {throw new Exception("Arsenal.Remove: Does not contain action '"+name+"'.");}
			else {
				list.Remove(task);
				return true;
			}
		}
	}
}