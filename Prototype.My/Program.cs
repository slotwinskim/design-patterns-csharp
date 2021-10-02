using System;

namespace Prototype.My
{
		internal class Program
		{
				static void Main()
				{
						var person = new Person(new CustomId(1), "Firstname1", "LastName1");
						var personShallowCopy = person.ShallowCopy();
						var personDeepCopy = person.DeepCopy();

						Console.WriteLine($"Person: {person}");
						Console.WriteLine($"Person: {personShallowCopy}");
						Console.WriteLine($"Person: {personDeepCopy}");
						Console.WriteLine();

						person.Id.Id = 10;
						person.FirstName = "FirstnameChanged";

						personShallowCopy.Id.Id = 20;
						personShallowCopy.FirstName = "FirstnameShallowCopy";
						
						personDeepCopy.Id.Id = 30;
						personDeepCopy.FirstName = "FirstnameDeepCopy";

						Console.WriteLine($"Person: {person}");
						Console.WriteLine($"Person: {personShallowCopy}");
						Console.WriteLine($"Person: {personDeepCopy}");

						// Person: 20: FirstnameChanged LastName1
						// Person: 20: FirstnameShallowCopy LastName1
						// Person: 30: FirstnameDeepCopy LastName1
				}

				public class CustomId
				{
						public int Id { get; set; }

						public CustomId(int id)
						{
								Id = id;
						}
				}

				public class Person
				{
						public CustomId Id { get; set; }
						public string FirstName { get; set; }
						public string LastName { get; set; }

						public Person(CustomId id, string firstName, string lastName)
						{
								Id = id;
								FirstName = firstName;
								LastName = lastName;
						}

						public Person ShallowCopy()
						{
								var person = (Person)MemberwiseClone();
								return person;
						}

						public Person DeepCopy()
						{
								var copiedId = new CustomId(Id.Id);
								//var person = new Person(copiedId, FirstName, LastName);
								var person = (Person)MemberwiseClone();
								person.Id = copiedId;

								return person;
						}

						public override string ToString()
						{
								return $"{Id.Id}: {FirstName} {LastName}";
						}
				}
		}
}
