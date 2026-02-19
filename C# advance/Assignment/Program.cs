namespace Assignment
{
    internal class Program
    {
        public delegate void TickMessage();
        static void Main(string[] args)
        {
            //Create a PhoneBook class with an indexer that takes a person's name and returns their phone number.
            PhoneBook phoneBook = new PhoneBook();
            Console.WriteLine(phoneBook["Alice"] = "123-456-7890");
            Console.WriteLine(phoneBook["Bob"]);
            //Build a WeeklySchedule class where you can access daily schedules using day names: schedule["Monday"].
            WeeklySchedule schedule = new WeeklySchedule();
            schedule["Monday"] = "Meeting at 10 AM";
            schedule["Tuesday"] = "Project deadline";
            Console.WriteLine(schedule["Monday"]);
            schedule[1] = "Gym at 6 PM";
            Console.WriteLine(schedule[1]);
            // Implement a Matrix class with a two-dimensional indexer matrix[row, column] for mathematical operations.
            Matrix matrix = new Matrix();
            matrix[0, 0] = 1;
            matrix[0, 1] = 2;
            matrix[1, 0] = 3;
            matrix[1, 1] = 4;
            Console.WriteLine(matrix[0, 0] + matrix[1, 1]);
            //Create a generic Stack<T> class with Push, Pop, and Peek methods.
            Stack<string> stack = new Stack<string>();
            stack.Push("First");
            stack.Push("Second");
            Console.WriteLine(stack.Peek());
            stack.Pop();
            Console.WriteLine(stack.Peek());
            //Build a generic Pair<T, U> class that holds two values of potentially different types.
            Pair<string, int> pair = new Pair<string, int>("Age", 30);
            Console.WriteLine($"{pair.First}: {pair.Second}");
            //Implement a generic Cache<TKey, TValue> class with expiration functionality using constraints.
            Cashe<string, string> cache = new Cashe<string, string>();
            cache.Add("key1", "value1");
            cache.Add("key2", "value2");
            Console.WriteLine(cache.Get("key1"));
            Console.WriteLine(cache.Get("key3"));
            //Design a generic Repository<T> with CRUD operations and constraints ensuring T implements IEntity interface.
            Repository<MyEntity> repository = new Repository<MyEntity>();
            MyEntity entity1 = new MyEntity { ID = 1, Name = "Entity1" };
            MyEntity entity2 = new MyEntity { ID = 2, Name = "Entity2" };
            repository.Add(entity1);
            repository.Add(entity2);
            Console.WriteLine(repository.Get(1)?.Name);
            Console.WriteLine(repository.Get(3)?.Name);
            Console.WriteLine(repository.Update(1, new MyEntity { ID = 1, Name = "Updated Entity1" })?.Name);
            repository.Remove(entity2);
            Console.WriteLine(repository.GetAll().Count());
            //Create a simple timer class that raises events for tick and completion.
            Timer timer = new Timer(5);
            timer.Tick += timer.TickHandler;
            timer.Completed += () => Console.WriteLine("Timer completed!");
            timer.Start();
            //Create a method that calculates the average of nullable integers, handling null values appropriately.
            double average = CalculateAverage(10, 20, null, 30);
            Console.WriteLine($"Average: {average}");
            //Create a contact manager using Dictionary<string, string> to store name-phone pairs with add, remove, and search functionality.
            ContactManager contactManager = new ContactManager();
            contactManager["Alice"] = "123-456-7890";
            contactManager["Bob"] = "987-654-3210";
            Console.WriteLine(contactManager["Alice"]);
            Console.WriteLine(contactManager["Charlie"]);
                contactManager.RemoveContact("Bob");
            Console.WriteLine(contactManager["Bob"]);
            //Create a generic method ConvertList<TSource, TTarget> that converts one list type to another using a converter function.
         //   CovertList<string, int> converter = new CovertList<string, int>();
            List<string> stringList = new List<string> { "1", "2", "3" };
            List<int> intList = CovertList<string,int>.ConvertList(stringList, s => int.Parse(s));
            Console.WriteLine(string.Join(", ", intList));
        }
        class PhoneBook
        {
            private Dictionary<string, string> phoneNumbers = new Dictionary<string, string>();
            public string this[string name]
            {
                get
                {
                    if (phoneNumbers.TryGetValue(name, out string number))
                    {
                        return number;
                    }
                    return "Number not found";
                }
                set
                {
                    phoneNumbers[name] = value;
                }
            }
        }
        class WeeklySchedule
        {
            public Dictionary<string, string> schedules = new Dictionary<string, string>();
            public string[] days = new string[7];
            public string this[string day]
            {
                get
                {
                    if (schedules.TryGetValue(day, out string schedule))
                    {
                        return schedule;
                    }
                    return "Schedule not found";
                }
                set
                {
                    schedules[day] = value;
                }
            }
            public string this[int index]
            {
                get
                {
                    if (index >= 0 && index < days.Length)
                    {
                        return days[index];
                    }
                    return "Invalid day index";
                }
                set
                {
                    if (index >= 0 && index < days.Length)
                    {
                        days[index] = value;
                    }
                }
            }
        }
        class Matrix
        {
            public int[,] matrix = new int[9, 8];
            public int this[int row, int col]
            {
                get
                {
                    if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
                    {
                        return matrix[row, col];
                    }
                    throw new IndexOutOfRangeException("Invalid matrix index");
                }
                set
                {
                    if (row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1))
                    {
                        matrix[row, col] = value;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("Invalid matrix index");
                    }
                }
            }
        }
        class Stack<T> where T : class
        {
            private List<T> stack = new List<T>();
            public void Push(T item)
            {
                stack.Add(item);
            }
            public T Pop()
            {
                if (stack.Count == 0)
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                T item = stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
                return item;
            }
            public T Peek()
            {
                if (stack.Count == 0)
                {
                    throw new InvalidOperationException("Stack is empty");
                }
                return stack[stack.Count - 1];
            }
        }
        class Pair<T, U> where T : class where U : struct
        {
            public T First { get; set; }
            public U Second { get; set; }
            public Pair(T first, U second)
            {
                First = first;
                Second = second;
            }
        }
        class Cashe<TKey, TValue> where TKey : class where TValue : class
        {
            private Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>();
            public void Add(TKey key, TValue value)
            {
                cache[key] = value;
            }
            public TValue Get(TKey key)
            {
                if (cache.TryGetValue(key, out TValue value))
                {
                    return value;
                }
                return null;
            }
        }
        public interface IEntity
        {
            int ID { get; set; }
        }
        class Repository<T> : IRepository<T> where T : class, IEntity
        {
            private List<T> items = new List<T>();
            public void Add(T item)
            {
                items.Add(item);
            }
            public void Remove(T item)
            {
                items.Remove(item);
            }
            public T Get(int id)
            {
                return items.FirstOrDefault(i => i.ID == id);
            }
            public IEnumerable<T> GetAll()
            {
                return items;
            }
            public T Update(int id, T item)
            {
                var existingItem = Get(id);
                if (existingItem != null)
                {
                    int index = items.IndexOf(existingItem);
                    items[index] = item;
                    return item;
                }
                return null;
            }
        }
        class MyEntity : IEntity
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }
        class Timer
        {
            public event TickMessage Tick;
            public event Action Completed;
            private int duration;
            public Timer(int duration)
            {
                this.duration = duration;
            }
         public void TickHandler()
            {
                Console.WriteLine("Tick");
            }
            public void Start()
            {
                for (int i = 0; i < duration; i++)
                {
                    Tick?.Invoke();
                    System.Threading.Thread.Sleep(1000); // Simulate time passing
                }
                Completed?.Invoke();
            }
        }
        public static double CalculateAverage(params int?[] numbers)
        {
            int sum = 0;
            int count = 0;
            foreach (var number in numbers)
            {
                if (number.HasValue)
                {
                    sum += number.Value;
                    count++;
                }
            }
            return count > 0 ? (double)sum / count : 0;
        }
        class ContactManager
        {
            private Dictionary<string, string> contacts = new Dictionary<string, string>();
            public string this[string name]
            {
                get
                {
                    if (contacts.TryGetValue(name, out string phone))
                    {
                        return phone;
                    }
                    return $"{name} is Contact not found";
                }
                set
                {
                    contacts[name] = value;
                }
            }
            public void RemoveContact(string name)
            {
                contacts.Remove(name);
            }
           /* public void AddContact(string name, string phone)
            {
                contacts[name] = phone;
            }
            public string SearchContact(string name)
            {
                if (contacts.TryGetValue(name, out string phone))
                {
                    return phone;
                }
                return "Contact not found";
            }*/
        }
        class CovertList<TSource,TTarget> where TSource : class where TTarget : struct
        {
            public static List<TTarget> ConvertList(List<TSource> sourceList, Func<TSource, TTarget> converter)
            {
                List<TTarget> targetList = new List<TTarget>();
                foreach (var item in sourceList)
                {
                    targetList.Add(converter(item));
                }
                return targetList;
            }
        }
    }
}
