using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Security.Claims;
using static CreationalPatterns.Factory_2_IMP;
using System.Runtime.ConstrainedExecution;
using System.Xml.Linq;
using System.Diagnostics.Contracts;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;

namespace CreationalPatterns
{
    public static class Singleton_1_IMP
    {
        /*
        1 Scenario: When you need exactly one instance of a class and global access to that instance.
            Example Use Cases:
            Logging
            Configuration settings
            Thread pools

        
        The Singleton design pattern ensures that a class has only one instance and provides a global point of access to it.
        Here is a complete implementation in C# covering all cases for the Singleton pattern, including thread safety and lazy initialization.
        I'll also include a `Main` method to demonstrate its use and output.
        

        ### Explanation:

        1. ** Private Constructor**: The constructor is private to prevent instantiation from outside the class.
        2. ** Static Instance**: A private static instance of the class is maintained.
        3. ** Thread Safety**: The double-checked locking pattern is used to ensure that the instance is created only once and in a thread-safe manner.
        4. **Public Property**: A public static property `Instance` provides the global access point to the instance.
        5. **Lock Object**: A `padlock` object is used to synchronize access to the instance.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Singleton instance created
        Hello from Singleton
        Hello from Singleton
        Both instances are the same
        

        This demonstrates that only one instance of the Singleton class is created, and the same instance is used throughout the program.

         */

        // Singleton class
        public sealed class Singleton
        {
            // Private static instance of the class
            private static Singleton instance = null;
            // Lock synchronization object
            private static readonly object padlock = new object();

            // Private constructor to prevent instantiation from outside
            private Singleton()
            {
                Console.WriteLine("Singleton instance created");
            }

            // Public static property to get the instance of the class
            public static Singleton Instance
            {
                get
                {
                    if (instance == null)
                    {
                        lock (padlock)
                        {
                            if (instance == null)
                            {
                                instance = new Singleton();
                            }
                        }
                    }
                    return instance;
                }
            }

            // A method to demonstrate functionality
            public void ShowMessage()
            {
                Console.WriteLine("Hello from Singleton");
            }
        }

        public static void Driver()
        {
            // Access the Singleton instance and call a method
            Singleton singleton1 = Singleton.Instance;
            singleton1.ShowMessage();

            // Access the Singleton instance again and call a method
            Singleton singleton2 = Singleton.Instance;
            singleton2.ShowMessage();

            // Check if both instances are the same
            if (singleton1 == singleton2)
            {
                Console.WriteLine("Both instances are the same");
            }
            else
            {
                Console.WriteLine("Instances are different");
            }
        }
    }

    public static class Factory_2_IMP
    {
        /*
         2 Factory ✅
            Scenario: When a class cannot anticipate the class of objects it needs to create or when a class wants its subclasses to specify the objects it creates.
            Example Use Cases:
            Creating objects without specifying the exact class
            When the creation logic needs to be centralized

        The Factory Design Pattern is a creational pattern that provides an interface for creating objects in a superclass
        but allows subclasses to alter the type of objects that will be created.
        Here’s a working example in C# to demonstrate the Factory Design Pattern.

        ### Explanation:

        1. ** Product Interface(`IProduct`)**:
           - Defines the interface for products that the factory will create.
           - Contains a method `DoWork()`.

        2. **Concrete Product Classes (`ConcreteProductA`, `ConcreteProductB`)**:
           - Implement the `IProduct` interface.
           - Provide specific implementations of the `DoWork()` method.

        3. ** Factory Class(`ProductFactory`)**:
           - Abstract class that declares the factory method `CreateProduct()` which returns an `IProduct`.
           - The factory method is abstract, meaning subclasses will provide the actual implementation.

        4. ** Concrete Factory Classes(`ConcreteProductAFactory`, `ConcreteProductBFactory`)**:
           - Inherit from `ProductFactory`.
           - Implement the `CreateProduct()` method to instantiate specific products(`ConcreteProductA` or `ConcreteProductB`).

        5. ** Client Code(`Program` Class)**:
           - Creates instances of `ConcreteProductAFactory` and `ConcreteProductBFactory`.
           - Uses these factories to create product instances(`productA` and `productB`).
           - Calls the `DoWork()` method on these products to demonstrate their functionality.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        ConcreteProductA is doing work.
        ConcreteProductB is doing work.
        

        ### Summary:

        - The Factory Design Pattern helps in creating objects without specifying the exact class of object that will be created.
        - The client code interacts with the factory class to get instances of the product,
        promoting loose coupling and making the system more flexible and extensible.
        */

        // Product interface
        public interface IProduct
        {
            void DoWork();
        }

        // Concrete Product classes
        public class ConcreteProductA : IProduct
        {
            public void DoWork()
            {
                Console.WriteLine("ConcreteProductA is doing work.");
            }
        }

        public class ConcreteProductB : IProduct
        {
            public void DoWork()
            {
                Console.WriteLine("ConcreteProductB is doing work.");
            }
        }

        // Factory class
        public abstract class ProductFactory
        {
            public abstract IProduct CreateProduct();
        }

        // Concrete Factory classes
        public class ConcreteProductAFactory : ProductFactory
        {
            public override IProduct CreateProduct()
            {
                return new ConcreteProductA();
            }
        }

        public class ConcreteProductBFactory : ProductFactory
        {
            public override IProduct CreateProduct()
            {
                return new ConcreteProductB();
            }
        }

        // Client code
        public static void Driver()
        {
            // Create factories
            ProductFactory factoryA = new ConcreteProductAFactory();
            ProductFactory factoryB = new ConcreteProductBFactory();

            // Use factories to create products
            IProduct productA = factoryA.CreateProduct();
            IProduct productB = factoryB.CreateProduct();

            // Use products
            productA.DoWork();
            productB.DoWork();
        }
    }

    public static class AbstractFactory_3_IMP
    {
        /*
         3  Abstract Factory ✅
            Scenario: When you need to provide an interface for creating families of related or dependent objects without specifying their concrete classes.
            Example Use Cases:
            When the system needs to be independent of how its objects are created
            When families of related objects need to be used together
            When you want to provide a library of products without exposing implementation details

        The Abstract Factory Design Pattern provides an interface for creating families of related or dependent objects without specifying their concrete classes.
        This pattern is useful when you need to create multiple families of products or objects that work together.

        ### Implementation:

        Let's create a simple example with two families of products: `Chair` and `Sofa`.
        We'll have two types of these products: `Modern` and `Victorian`.

        

        ### Explanation:

        1. ** Abstract Product Interfaces(`IChair`, `ISofa`)**:
           - Define the interfaces for products that the factory will create(`SitOn` for `IChair` and `LieOn` for `ISofa`).

        2. ** Concrete Product Classes(`ModernChair`, `VictorianChair`, `ModernSofa`, `VictorianSofa`)**:
           - Implement the `IChair` and `ISofa` interfaces.
           - Provide specific implementations for `SitOn` and `LieOn`.

        3. ** Abstract Factory Interface(`IFurnitureFactory`)**:
           - Declares methods for creating abstract product objects(`CreateChair`, `CreateSofa`).

        4. ** Concrete Factory Classes(`ModernFurnitureFactory`, `VictorianFurnitureFactory`)**:
           - Implement the `IFurnitureFactory` interface.
           - Provide specific implementations for creating `Modern` and `Victorian` furniture.

        5. ** Client Code(`Program` Class)**:
           - Creates instances of `ModernFurnitureFactory` and `VictorianFurnitureFactory`.
           - Uses these factories to create product instances(`modernChair`, `modernSofa`, `victorianChair`, `victorianSofa`).
           - Calls the `SitOn` and `LieOn` methods on these products to demonstrate their functionality.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Sitting on a modern chair.
                Lying on a modern sofa.
                Sitting on a Victorian chair.
        Lying on a Victorian sofa.
        

        ### Summary:

        - The Abstract Factory Design Pattern helps in creating families of related objects without specifying their concrete classes.
        - It promotes consistency among products and makes the system flexible to add new families of products without changing the existing code.
        - The client code interacts with the abstract factory and product interfaces, promoting loose coupling and making the system more extensible.
        */

        // Abstract Product Interfaces
        public interface IChair
        {
            void SitOn();
        }

        public interface ISofa
        {
            void LieOn();
        }

        // Concrete Product Classes
        public class ModernChair : IChair
        {
            public void SitOn()
            {
                Console.WriteLine("Sitting on a modern chair.");
            }
        }

        public class VictorianChair : IChair
        {
            public void SitOn()
            {
                Console.WriteLine("Sitting on a Victorian chair.");
            }
        }

        public class ModernSofa : ISofa
        {
            public void LieOn()
            {
                Console.WriteLine("Lying on a modern sofa.");
            }
        }

        public class VictorianSofa : ISofa
        {
            public void LieOn()
            {
                Console.WriteLine("Lying on a Victorian sofa.");
            }
        }

        // Abstract Factory Interface
        public interface IFurnitureFactory
        {
            IChair CreateChair();
            ISofa CreateSofa();
        }

        // Concrete Factory Classes
        public class ModernFurnitureFactory : IFurnitureFactory
        {
            public IChair CreateChair()
            {
                return new ModernChair();
            }

            public ISofa CreateSofa()
            {
                return new ModernSofa();
            }
        }

        public class VictorianFurnitureFactory : IFurnitureFactory
        {
            public IChair CreateChair()
            {
                return new VictorianChair();
            }

            public ISofa CreateSofa()
            {
                return new VictorianSofa();
            }
        }

        // Client Code
        public static void Driver()
        {
            // Create factories
            IFurnitureFactory modernFactory = new ModernFurnitureFactory();
            IFurnitureFactory victorianFactory = new VictorianFurnitureFactory();

            // Use factories to create products
            IChair modernChair = modernFactory.CreateChair();
            ISofa modernSofa = modernFactory.CreateSofa();
            IChair victorianChair = victorianFactory.CreateChair();
            ISofa victorianSofa = victorianFactory.CreateSofa();

            // Use products
            modernChair.SitOn();
            modernSofa.LieOn();
            victorianChair.SitOn();
            victorianSofa.LieOn();
        }
    }

    public static class Builder_4_IMP
    {
        /*
         4 Builder ✅
            Scenario: When you need to construct complex objects step by step and the construction process should allow different representations.
            Example Use Cases:
            Constructing complex objects with many parts
            Building objects that require several steps
            Fluent interfaces

        The Builder Design Pattern is a creational pattern that separates the construction of a complex object from
        its representation so that the same construction process can create different representations.
        It is particularly useful when you need to create an object step-by-step with varying configurations.

        ### Explanation:

        1. ** Product Class(`House`)**:
           - Represents the complex object being built.
           - Contains properties like `Walls`, `Roof`, `Doors`, `Windows`, and `Garage`.

        2. ** Builder Interface(`IHouseBuilder`)**:
           - Declares the building steps for constructing a `House`.
           - Methods include `BuildWalls`, `BuildRoof`, `BuildDoors`, `BuildWindows`, and `BuildGarage`.

        3. ** Concrete Builder Class(`ConcreteHouseBuilder`)**:
           - Implements the `IHouseBuilder` interface.
           - Provides specific implementations for each building step.
           - Maintains an instance of `House` and constructs it step-by-step.

        4. ** Director Class(`HouseDirector`)**:
           - Uses the builder interface to construct the `House`.
           - Directs the building process by calling the appropriate methods on the builder.

        5. **Client Code (`Program` Class)**:
           - Creates an instance of the concrete builder.
           - Creates an instance of the director and instructs it to construct the house.
           - Retrieves the constructed house and prints its details.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        House with Brick Walls, Concrete Roof, Wooden Doors, Glass Windows, and Two Car Garage
        

        ### Summary:

        - The Builder Design Pattern separates the construction of a complex object from its representation.
        - It allows you to create different representations of an object using the same construction process.
        - The client code interacts with the director and builder interfaces,
        promoting loose coupling and making the system more flexible and extensible.
        */

        // Product class
        public class House
        {
            public string Walls { get; set; }
            public string Roof { get; set; }
            public string Doors { get; set; }
            public string Windows { get; set; }
            public string Garage { get; set; }

            public override string ToString()
            {
                return $"House with {Walls}, {Roof}, {Doors}, {Windows}, and {Garage}";
            }
        }

        // Builder interface
        public interface IHouseBuilder
        {
            void BuildWalls();
            void BuildRoof();
            void BuildDoors();
            void BuildWindows();
            void BuildGarage();
            House GetHouse();
        }

        // Concrete Builder class
        public class ConcreteHouseBuilder : IHouseBuilder
        {
            private House house = new House();

            public void BuildWalls()
            {
                house.Walls = "Brick Walls";
            }

            public void BuildRoof()
            {
                house.Roof = "Concrete Roof";
            }

            public void BuildDoors()
            {
                house.Doors = "Wooden Doors";
            }

            public void BuildWindows()
            {
                house.Windows = "Glass Windows";
            }

            public void BuildGarage()
            {
                house.Garage = "Two Car Garage";
            }

            public House GetHouse()
            {
                return house;
            }
        }

        // Director class
        public class HouseDirector
        {
            private IHouseBuilder houseBuilder;

            public HouseDirector(IHouseBuilder builder)
            {
                houseBuilder = builder;
            }

            public void ConstructHouse()
            {
                houseBuilder.BuildWalls();
                houseBuilder.BuildRoof();
                houseBuilder.BuildDoors();
                houseBuilder.BuildWindows();
                houseBuilder.BuildGarage();
            }

            public House GetHouse()
            {
                return houseBuilder.GetHouse();
            }
        }

        // Client code
        public static void Driver()
        {
            // Create a builder instance
            IHouseBuilder builder = new ConcreteHouseBuilder();

            // Create a director instance and construct the house
            HouseDirector director = new HouseDirector(builder);
            director.ConstructHouse();

            // Get the constructed house
            House house = director.GetHouse();

            // Print the house details
            Console.WriteLine(house);
        }
    }

    public static class Prototype_5
    {
        /*
          5 Prototype
            Scenario: When the cost of creating a new object is expensive or complex and you need to create a copy of an existing object.
            Example Use Cases:
            Creating objects that are costly to create
            When objects should be independent of their classes
            Cloning objects


        The Prototype Design Pattern is a creational pattern that allows an object to create a copy of itself.
        This is useful when creating an instance of a class is costly or complex.
        The pattern involves implementing a prototype interface which defines a method for cloning objects.

        ### Implementation:

        Let's create an example where we have a `Shape` abstract class with concrete subclasses `Circle` and `Rectangle`.
        We'll use the Prototype Design Pattern to clone these objects.

        

        ### Explanation:

        1. ** Prototype Abstract Class(`Shape`)**:
           - Defines an abstract method `Clone()` that will be implemented by concrete classes to clone themselves.
           - Contains properties `Id` and `Type`.

        2. **Concrete Prototype Classes (`Circle`, `Rectangle`)**:
           - Inherit from the `Shape` class.
           - Implement the `Clone()` method using `MemberwiseClone()`, which creates a shallow copy of the current object.

        3. ** Client Code(`Program` Class)**:
           - Creates instances of `Circle` and `Rectangle`.
           - Clones these instances using the `Clone()` method.
           - Prints the details of the original and cloned objects.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Original and Cloned Shapes:
        Shape[Type = Circle, Id = 1]
        Shape[Type = Circle, Id = 1]
        Shape[Type = Rectangle, Id = 2]
        Shape[Type = Rectangle, Id = 2]
        

        ### Summary:

        - The Prototype Design Pattern allows an object to create a copy of itself.
        - It is useful when the creation of an instance is costly or complex.
        - By implementing the `Clone()` method, concrete classes can define how they should be cloned.
        - The client code interacts with the prototype interface, promoting loose coupling and making the system more flexible and extensible.
        */

        // Prototype abstract class
        public abstract class Shape
        {
            public string Id { get; set; }
            public string Type { get; set; }

            public abstract Shape Clone();

            public override string ToString()
            {
                return $"Shape [Type={Type}, Id={Id}]";
            }
        }

        // Concrete prototype class for Circle
        public class Circle : Shape
        {
            public Circle()
            {
                Type = "Circle";
            }

            public override Shape Clone()
            {
                return (Shape)this.MemberwiseClone();
            }
        }

        // Concrete prototype class for Rectangle
        public class Rectangle : Shape
        {
            public Rectangle()
            {
                Type = "Rectangle";
            }

            public override Shape Clone()
            {
                return (Shape)this.MemberwiseClone();
            }
        }

        // Client code
        public static void Driver()
        {
            // Create instances of Circle and Rectangle
            Circle circle = new Circle { Id = "1" };
            Rectangle rectangle = new Rectangle { Id = "2" };

            // Clone the Circle and Rectangle
            Circle clonedCircle = (Circle)circle.Clone();
            Rectangle clonedRectangle = (Rectangle)rectangle.Clone();

            // Print details of original and cloned objects
            Console.WriteLine("Original and Cloned Shapes:");
            Console.WriteLine(circle);
            Console.WriteLine(clonedCircle);
            Console.WriteLine(rectangle);
            Console.WriteLine(clonedRectangle);
        }
    }
}