namespace C__advance
{
    delegate void Notify(string message);
    delegate decimal DiscountDelegate(decimal salary);
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Student student = new Student();
            Console.WriteLine(student.getstudent(0)); // Output: John
            student.setstudent(0, "Michael");
            Console.WriteLine(student.getstudent(0)); // Output: Michael
            Console.WriteLine(student[1]); // Output: Jane
            student.name = "56";
            student[1] = "Emily";
            Console.WriteLine(student[1]); // Output: Emily
            student["Emily"] = "A+";
            Console.WriteLine(student[1]);

            //student["moa", "S+"] = "c+";
            //Console.WriteLine(student["moa"]); // Output: c+     //use public string this[string key] to search in dictuinary not in array
            */
            /*
            var email = "sas@das.dsa";
            Console.WriteLine(email.IsValidEmail());// we prefer use extension method rather than static method because it is more readable and we can use it like a normal method without need to create an instance of the class
            int? number = null;

            if (number.HasValue)
            {
                Console.WriteLine(number.Value);
            }
            int result = number ?? 0;
            Console.WriteLine($"result:{result}");
            string name = null;
            int? length = name?.Length;// null conditional operator (return null if name is null instead of throwing an exception)
            Console.WriteLine($"length:{length}");
            */
            /*
            GenericClass<string> genericClass = new GenericClass<string>();
            genericClass.Data = "Hello, World!";
            GenericClass<int[]> genericClass2 = new GenericClass<int[]>();// can't use GenericClass<int> because int is a value type and we have constraint where T : class
            genericClass2.Data = new int[] { 1, 2, 3, 4, 5 };
            foreach (var item in genericClass2.Data)
            {
                Console.WriteLine(item);
            }
            */
           /* Notify notify = SendSms;
            notify += SendEmail;
            order("Laptop", 999.99m,notify);
            notify -= SendEmail;
            order("Phone", 499.99m, notify);
            
             // delgate is method pointer with specific signature (return type and parameters) and it can point to any method with the same signature
             
                DiscountDelegate discountDelegate = EmpDiscount;
            invoice("John Doe", 1000m, discountDelegate);//delegate can't use 3 function at the same time because it has return type 
            invoice("John Doe", 1000m, vipDiscount);// we can use the method name directly because it matches the signature of the delegate
            invoice("John Doe", 1000m, Discount);*/
           /*
            Action<string>Notifacion=(msg) => Console.WriteLine($"Notification: {msg}");// Action is a built-in delegate that can point to any method with void return type and one or more parameters
            Notifacion("This is a notification message.");
            List<string> list = new List<string>() {"hassan","mahmoud","atia"};
            list.ForEach(name => Console.WriteLine(name)); // ForEach is a method that takes an Action delegate as a parameter and executes it for each element in the list
            Func<int,float> func= (i) => i/5f;// Func is a built-in delegate that can point to any method with a return type and zero or more parameters
            Console.WriteLine(func(12));
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select(n => n * n).ToList(); // Select is a LINQ method that takes a Func delegate as a parameter and projects each element of a sequence into a new form
            squaredNumbers.ForEach(n => Console.WriteLine(n));
         //   numbers.Where(n => n % 2 == 0).ToList().ForEach(n => Console.WriteLine($"Even number: {n}")); // Where is a LINQ method that takes a Func delegate as a parameter and filters a sequence of values based on a predicate
         Predicate<int> isEven = n => n % 2 == 0; // Predicate is a built-in delegate that can point to any method with a return type of bool and one parameter
            numbers.Where(n => isEven(n)).ToList().ForEach(n => Console.WriteLine($"Even number: {n}"));
           */
        }
        /*
         * instead of create many class ro method use one with generic type <T> (reusable)
         * ✔️ 2) Performance
         *أفضل من استخدام object
         *لأن object يحتاج Boxing/Unboxing.
         *| Constraint             | المعنى                     |
| ---------------------- | -------------------------- |
| `where T : class`      | لازم يكون Reference Type   |
| `where T : struct`     | لازم يكون Value Type       |
| `where T : new()`      | لازم عنده Constructor فاضي |
| `where T : BaseEntity` | لازم يرث من BaseEntity     |
         */
        /*
         * nullable types
         * | النوع          | مثال                  | يقبل null؟ |
| -------------- | --------------------- | ---------- |
| Value Type     | int, double, DateTime | ❌ لا       |
| Reference Type | string, class         | ✔️ نعم     |
int x = null;  // ❌ Error
string name = null; // ✔️ عادي
int? age = null;<========> Nullable<int> age = null; 
DateTime? birthDate = null;
bool? isActive = null;

         */

        /* public static class StringExtensions  // Must be static class
         {
             // The 'this' keyword makes it an extension method
             public static bool IsValidEmail(this string email)
             {
                 if (string.IsNullOrWhiteSpace(email))
                     return false;

                 return true;

             }
         }
         public class GenericClass<T> where T : class
         {
             public T Data { get; set; }
         }*/
        /*
        public static void order(string product, decimal price,Notify del)
        {
            Console.WriteLine($"Order placed for {product} with price {price}");
            del.Invoke($"Order placed for {product} with price {price}");
            //SendEmail($"Order placed for {product} with price {price}");
            //SendSms($"Order placed for {product} with price {price}");
        }
        public static void SendSms(string msg)
        {
            Console.WriteLine($"SMS sent: {msg}");
        }
        public static void SendEmail(string msg)
        {
            Console.WriteLine($"Email sent: {msg}");
        }
        public static void invoice(string custname,decimal amount,DiscountDelegate discountDelegate)
        {
            decimal final= amount - discountDelegate.Invoke(amount);
            decimal saved= amount - final;
            Console.WriteLine($"Invoice for {custname} with amount {amount}");
            Console.WriteLine($"final amount after discount: {final}");
            Console.WriteLine($"saved amount: {saved}");
        }
        public static decimal EmpDiscount(decimal salary)
        {
            return salary * 0.1m;
        }
        public static decimal vipDiscount(decimal salary)
        {
           return salary * 0.3m;
        }
        public static decimal Discount(decimal salary)
        {
           return salary * 0.05m;
        }
        */
    }
}
