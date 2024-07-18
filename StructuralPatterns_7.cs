namespace StructuralPatterns
{
    public static class Adapter_1_IMP
    {
        /*
         1 Adapter ✅
            Scenario: When you need to make a class's interface compatible with another interface.
            Example Use Cases:
            Integrating with third-party libraries
            Legacy system integration
            Adapting one interface to another

        The Adapter Design Pattern is a structural pattern that allows objects with incompatible interfaces to work together.
        The Adapter acts as a bridge between two incompatible interfaces.

        ### Implementation:

        Let's create an example where we have an existing `IRoundPeg` interface and a class `RoundPeg`.
        We also have a `SquarePeg` class that we want to use with the `IRoundPeg` interface.
        We'll create an `Adapter` to make `SquarePeg` compatible with `IRoundPeg`.

        

        ### Explanation:

        1. ** Existing RoundPeg Interface(`IRoundPeg`)**:
           - Defines a method `GetRadius()` to return the radius of a round peg.

        2. **Concrete Class Implementing RoundPeg (`RoundPeg`)**:
           - Implements the `IRoundPeg` interface.
           - Provides an implementation for `GetRadius()`.

        3. ** Class that We Want to Adapt(`SquarePeg`)**:
           - Contains a method `GetWidth()` to return the width of a square peg.

        4. **Adapter Class (`SquarePegAdapter`)**:
           - Implements the `IRoundPeg` interface.
           - Contains an instance of `SquarePeg` and adapts its `GetWidth()` method to the `GetRadius()` method.
           - Calculates the radius of the smallest circle that can fit the square peg using the formula `width* sqrt(2) / 2`.

        5. ** Client Code(`Program` Class)**:
           - Creates instances of `RoundPeg` and `SquarePeg`.
           - Uses the `SquarePegAdapter` to make the `SquarePeg` compatible with `IRoundPeg`.
           - Prints the details of the `RoundPeg`, `SquarePeg`, and `SquarePegAdapter`.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        RoundPeg radius: 5
        SquarePeg width: 7
        SquarePegAdapter radius: 4.949747468305833
        

        ### Summary:

        - The Adapter Design Pattern allows objects with incompatible interfaces to work together.
        - The Adapter acts as a bridge, converting the interface of a class into another interface that a client expects.
        - This promotes flexibility and reusability by allowing existing classes to be used in new ways without modifying their code.
        - In this example, the `SquarePegAdapter` allows a `SquarePeg` to be used where a `RoundPeg` is expected.
        */

        // Existing RoundPeg interface
        public interface IRoundPeg
        {
            double GetRadius();
        }

        // Concrete class implementing RoundPeg
        public class RoundPeg : IRoundPeg
        {
            private double radius;

            public RoundPeg(double radius)
            {
                this.radius = radius;
            }

            public double GetRadius()
            {
                return radius;
            }
        }

        // Class that we want to adapt
        public class SquarePeg
        {
            private double width;

            public SquarePeg(double width)
            {
                this.width = width;
            }

            public double GetWidth()
            {
                return width;
            }
        }

        // Adapter class making SquarePeg compatible with IRoundPeg
        public class SquarePegAdapter : IRoundPeg
        {
            private SquarePeg squarePeg;

            public SquarePegAdapter(SquarePeg peg)
            {
                this.squarePeg = peg;
            }

            public double GetRadius()
            {
                // Calculate the radius of the smallest circle that can fit this square peg
                return squarePeg.GetWidth() * Math.Sqrt(2) / 2;
            }
        }

        // Client code
        public static void Driver()
        {
            // Create instances of RoundPeg and SquarePeg
            RoundPeg roundPeg = new RoundPeg(5);
            SquarePeg squarePeg = new SquarePeg(7);

            // Create adapter to make SquarePeg compatible with RoundPeg
            IRoundPeg squarePegAdapter = new SquarePegAdapter(squarePeg);

            // Print details
            Console.WriteLine("RoundPeg radius: " + roundPeg.GetRadius());
            Console.WriteLine("SquarePeg width: " + squarePeg.GetWidth());
            Console.WriteLine("SquarePegAdapter radius: " + squarePegAdapter.GetRadius());
        }
    }

    public static class Bridge_2
    {
        /*
         2 Bridge
            Scenario: When you need to decouple an abstraction from its implementation so that the two can vary independently.
            Example Use Cases:
            When you have a complex class hierarchy that can be simplified by separating different concerns
            When you want to share an implementation among multiple objects 

                    The Bridge Design Pattern is a structural pattern that decouples an abstraction from its implementation so that the two can vary independently.It involves an abstraction class and an implementation class, with the abstraction containing a reference to the implementation.

        ### Implementation:

        Let's create an example where we have different types of `RemoteControl` (the abstraction) that can control different types of `Device` (the implementation). We'll use the Bridge Pattern to decouple these two hierarchies.

        

        ### Explanation:

        1. ** Implementor Interface(`IDevice`)**:
           - Declares methods that concrete implementations(e.g., `TV`, `Radio`) must implement, such as `PowerOn`, `PowerOff`, `SetVolume`, `IsPoweredOn`, and `GetVolume`.

        2. ** Concrete Implementor Classes(`TV`, `Radio`)**:
           - Implement the `IDevice` interface and provide concrete implementations for the methods.

        3. **Abstraction Class (`RemoteControl`)**:
           - Contains a reference to the `IDevice` interface.
           - Defines abstract methods(`TogglePower`, `VolumeUp`, `VolumeDown`) that will be implemented by subclasses.

        4. **Refined Abstraction (`BasicRemoteControl`)**:
           - Inherits from `RemoteControl` and implements the abstract methods.
           - Provides specific behavior for the remote control, such as toggling power and adjusting the volume.

        5. **Client Code (`Program` Class)**:
           - Creates instances of `TV` and `Radio` (concrete implementors).
           - Creates instances of `BasicRemoteControl` (refined abstraction) and associates them with the `TV` and `Radio`.
           - Uses the remote controls to interact with the devices.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        TV is powered on.
        TV volume set to 10%.
        TV volume set to 20%.
        TV volume set to 10%.
        TV is powered off.

        Radio is powered on.
        Radio volume set to 10%.
        Radio volume set to 20%.
        Radio volume set to 10%.
        Radio is powered off.
        

        ### Summary:

        - The Bridge Design Pattern decouples an abstraction from its implementation so that the two can vary independently.
        - The abstraction (`RemoteControl`) contains a reference to the implementation(`IDevice`).
        - Concrete implementations(`TV`, `Radio`) and refined abstractions(`BasicRemoteControl`) can be developed independently.
        - This promotes flexibility and scalability, allowing new devices or remote controls to be added without changing existing code.
         */

        // Implementor interface
        public interface IDevice
        {
            void PowerOn();
            void PowerOff();
            void SetVolume(int percent);
            bool IsPoweredOn();
            int GetVolume();
        }

        // Concrete Implementor for TV
        public class TV : IDevice
        {
            private bool powerOn = false;
            private int volume = 0;

            public void PowerOn()
            {
                powerOn = true;
                Console.WriteLine("TV is powered on.");
            }

            public void PowerOff()
            {
                powerOn = false;
                Console.WriteLine("TV is powered off.");
            }

            public void SetVolume(int percent)
            {
                volume = percent;
                Console.WriteLine($"TV volume set to {volume}%.");
            }

            public bool IsPoweredOn()
            {
                return powerOn;
            }

            public int GetVolume()
            {
                return volume;
            }
        }

        // Concrete Implementor for Radio
        public class Radio : IDevice
        {
            private bool powerOn = false;
            private int volume = 0;

            public void PowerOn()
            {
                powerOn = true;
                Console.WriteLine("Radio is powered on.");
            }

            public void PowerOff()
            {
                powerOn = false;
                Console.WriteLine("Radio is powered off.");
            }

            public void SetVolume(int percent)
            {
                volume = percent;
                Console.WriteLine($"Radio volume set to {volume}%.");
            }

            public bool IsPoweredOn()
            {
                return powerOn;
            }

            public int GetVolume()
            {
                return volume;
            }
        }

        // Abstraction class
        public abstract class RemoteControl
        {
            protected IDevice device;

            protected RemoteControl(IDevice device)
            {
                this.device = device;
            }

            public abstract void TogglePower();
            public abstract void VolumeUp();
            public abstract void VolumeDown();
        }

        // Refined Abstraction for Basic Remote Control
        public class BasicRemoteControl : RemoteControl
        {
            public BasicRemoteControl(IDevice device) : base(device) { }

            public override void TogglePower()
            {
                if (device.IsPoweredOn())
                {
                    device.PowerOff();
                }
                else
                {
                    device.PowerOn();
                }
            }

            public override void VolumeUp()
            {
                device.SetVolume(device.GetVolume() + 10);
            }

            public override void VolumeDown()
            {
                device.SetVolume(device.GetVolume() - 10);
            }
        }

        // Client code
        public static void Driver()
        {
            IDevice tv = new TV();
            RemoteControl tvRemote = new BasicRemoteControl(tv);

            tvRemote.TogglePower();
            tvRemote.VolumeUp();
            tvRemote.VolumeUp();
            tvRemote.VolumeDown();
            tvRemote.TogglePower();

            Console.WriteLine();

            IDevice radio = new Radio();
            RemoteControl radioRemote = new BasicRemoteControl(radio);

            radioRemote.TogglePower();
            radioRemote.VolumeUp();
            radioRemote.VolumeUp();
            radioRemote.VolumeDown();
            radioRemote.TogglePower();
        }

    }

    public static class Composite_3
    {
        /*
         3 Composite
            Scenario: When you need to treat individual objects and compositions of objects uniformly.
            Example Use Cases:
            Hierarchical tree structures
            File systems
            Graphic drawing editors

        
         The Composite Design Pattern is a structural pattern that allows you to compose objects into tree structures
        to represent part-whole hierarchies.This pattern lets clients treat individual objects and compositions of objects uniformly.

        ### Implementation:

        Let's create an example where we have a `Component` interface representing both individual objects and compositions of objects.
        We'll implement `Leaf` and `Composite` classes to demonstrate this pattern.

        

        ### Explanation:

        1. ** Component Interface(`IGraphic`)**:
           - Declares the `Draw` method that will be implemented by both leaf and composite classes.

        2. ** Leaf Classes(`Circle`, `Rectangle`)**:
           - Implement the `IGraphic` interface.
           - Provide specific implementations for the `Draw` method.

        3. ** Composite Class(`CompositeGraphic`)**:
           - Implements the `IGraphic` interface.
           - Contains a list of child `IGraphic` objects.
           - Provides methods to add and remove child graphics(`Add`, `Remove`).
           - Implements the `Draw` method by calling `Draw` on each child graphic.

        4. **Client Code (`Program` Class)**:
           - Creates instances of `Circle` and `Rectangle` (leaf objects).
           - Creates instances of `CompositeGraphic` (composite objects) and adds leaf objects and other composites to them.
           - Calls the `Draw` method on the composite objects to demonstrate the part-whole hierarchy.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Drawing composite2:
        Drawing a Circle
        Drawing a Circle
        Drawing a Rectangle
        

        ### Summary:

        - The Composite Design Pattern allows you to compose objects into tree structures to represent part-whole hierarchies.
        - Clients can treat individual objects and compositions of objects uniformly through a common interface.
        - Leaf objects(e.g., `Circle`, `Rectangle`) implement the component interface directly.
        - Composite objects(e.g., `CompositeGraphic`) also implement the component interface and contain a list of child components,
                    allowing them to recursively manage and operate on these children.
        - This pattern promotes flexibility and scalability,
        enabling complex hierarchical structures to be managed and interacted with in a consistent manner.
         */


        // Component interface
        public interface IGraphic
        {
            void Draw();
        }

        // Leaf class
        public class Circle : IGraphic
        {
            public void Draw()
            {
                Console.WriteLine("Drawing a Circle");
            }
        }

        // Another Leaf class
        public class Rectangle : IGraphic
        {
            public void Draw()
            {
                Console.WriteLine("Drawing a Rectangle");
            }
        }

        // Composite class
        public class CompositeGraphic : IGraphic
        {
            private List<IGraphic> _children = new List<IGraphic>();

            public void Add(IGraphic graphic)
            {
                _children.Add(graphic);
            }

            public void Remove(IGraphic graphic)
            {
                _children.Remove(graphic);
            }

            public void Draw()
            {
                foreach (var graphic in _children)
                {
                    graphic.Draw();
                }
            }
        }

        // Client code
        public static void Driver()
        {
            // Create individual graphics
            IGraphic circle1 = new Circle();
            IGraphic circle2 = new Circle();
            IGraphic rectangle = new Rectangle();

            // Create a composite graphic
            CompositeGraphic composite1 = new CompositeGraphic();
            composite1.Add(circle1);
            composite1.Add(rectangle);

            // Create another composite graphic
            CompositeGraphic composite2 = new CompositeGraphic();
            composite2.Add(circle2);
            composite2.Add(composite1);

            // Draw all graphics
            Console.WriteLine("Drawing composite2:");
            composite2.Draw();
        }

    }

    public static class Decorator_4_IMP
    {
        /*
         4 Decorator ✅
            Scenario: When you need to add behavior or responsibilities to objects dynamically.
            Example Use Cases:
            Adding functionality to objects at runtime
            Wrapping classes with additional behavior

                The Decorator Design Pattern is a structural pattern that allows behavior to be added to individual objects,
        either statically or dynamically, without affecting the behavior of other objects from the same class
        This pattern provides a flexible alternative to subclassing for extending functionality.

        ### Implementation:

        Let's create an example where we have a `Coffee` interface with a basic implementation (`SimpleCoffee`) 
        a series of decorators that add functionality to the `Coffee` objects.

        

        ### Explanation:

        1. ** Component Interface(`ICoffee`)**:
           - Declares methods that both the concrete component and decorators will implement, such as `GetDescription` and `GetCost`.

        2. ** Concrete Component Class(`SimpleCoffee`)**:
           - Implements the `ICoffee` interface.
           - Provides basic implementations for `GetDescription` and `GetCost`.

        3. ** Base Decorator Class(`CoffeeDecorator`)**:
           - Implements the `ICoffee` interface.
           - Contains a reference to an `ICoffee` object (the component being decorated).
           - Provides virtual implementations of `GetDescription` and `GetCost`, delegating to the decorated component.

        4. ** Concrete Decorator Classes(`MilkDecorator`, `SugarDecorator`)**:
           - Inherit from `CoffeeDecorator`.
           - Override `GetDescription` and `GetCost` to add their specific behavior while still calling the base implementation.

        5. ** Client Code(`Program` Class)**:
           - Creates an instance of `SimpleCoffee`.
           - Dynamically adds `MilkDecorator` and `SugarDecorator` to the coffee.
           - Prints the description and cost at each step.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Simple coffee costs $5
        Simple coffee, milk costs $6.5
        Simple coffee, milk, sugar costs $7
        

        ### Summary:

        - The Decorator Design Pattern allows behavior to be added to individual objects dynamically without affecting other objects from the same class.
        - It provides a flexible alternative to subclassing for extending functionality.
        - Decorators implement the same interface as the components they decorate and can be chained together to add multiple layers of behavior.
        - In this example, `MilkDecorator` and `SugarDecorator` extend the functionality of `SimpleCoffee` by adding descriptions and costs.
         */



        // Component interface
        public interface ICoffee
        {
            string GetDescription();
            double GetCost();
        }

        // Concrete Component class
        public class SimpleCoffee : ICoffee
        {
            public string GetDescription()
            {
                return "Simple coffee";
            }

            public double GetCost()
            {
                return 5.0;
            }
        }

        // Base Decorator class
        public abstract class CoffeeDecorator : ICoffee
        {
            protected ICoffee decoratedCoffee;

            public CoffeeDecorator(ICoffee coffee)
            {
                decoratedCoffee = coffee;
            }

            public virtual string GetDescription()
            {
                return decoratedCoffee.GetDescription();
            }

            public virtual double GetCost()
            {
                return decoratedCoffee.GetCost();
            }
        }

        // Concrete Decorator classes
        public class MilkDecorator : CoffeeDecorator
        {
            public MilkDecorator(ICoffee coffee) : base(coffee) { }

            public override string GetDescription()
            {
                return decoratedCoffee.GetDescription() + ", milk";
            }

            public override double GetCost()
            {
                return decoratedCoffee.GetCost() + 1.5;
            }
        }

        public class SugarDecorator : CoffeeDecorator
        {
            public SugarDecorator(ICoffee coffee) : base(coffee) { }

            public override string GetDescription()
            {
                return decoratedCoffee.GetDescription() + ", sugar";
            }

            public override double GetCost()
            {
                return decoratedCoffee.GetCost() + 0.5;
            }
        }

        // Client code
        public static void Driver()
        {
            // Create a simple coffee
            ICoffee myCoffee = new SimpleCoffee();
            Console.WriteLine($"{myCoffee.GetDescription()} costs ${myCoffee.GetCost()}");

            // Add milk to the coffee
            myCoffee = new MilkDecorator(myCoffee);
            Console.WriteLine($"{myCoffee.GetDescription()} costs ${myCoffee.GetCost()}");

            // Add sugar to the coffee
            myCoffee = new SugarDecorator(myCoffee);
            Console.WriteLine($"{myCoffee.GetDescription()} costs ${myCoffee.GetCost()}");
        }
    }

    public static class Facade_5
    {
        /*
        5 Facade
            Scenario: When you need to provide a unified interface to a set of interfaces in a subsystem.
            Example Use Cases:
            Simplifying complex systems
            Providing a simple interface for complex libraries
            Reducing dependencies between clients and subsystems 

         The Facade Design Pattern is a structural pattern that provides a simplified interface to a complex subsystem.
        The Facade Pattern hides the complexities of the system and provides an easy-to-use interface to the client.

        ### Implementation:

        Let's create an example where we have a complex system for home theater that includes 
        different components such as `DVDPlayer`, `Amplifier`, `Projector`, and `Lights`.
        We will create a `HomeTheaterFacade` to simplify the interface for the client.
        

        ### Explanation:

        1. ** Subsystem Classes(`DVDPlayer`, `Amplifier`, `Projector`, `Lights`)**:
           - Each subsystem class has its own methods and internal logic.
           - These classes represent the complex parts of the home theater system.

        2. ** Facade Class(`HomeTheaterFacade`)**:
           - Provides a simplified interface to the client.
           - Contains references to the subsystem components.
           - Offers methods (`WatchMovie`, `EndMovie`) that perform complex operations by delegating tasks to the subsystem classes.

        3. ** Client Code(`Program` Class)**:
           - Creates instances of the subsystem components.
           - Creates an instance of the `HomeTheaterFacade` and passes the subsystem components to it.
           - Uses the facade to interact with the subsystem, simplifying the client code.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Get ready to watch a movie...
        Lights dimmed to 10%.
        Projector is On.
        Projector in widescreen mode (16x9 aspect ratio).
        Amplifier is On.
        Amplifier volume set to 5.
        DVD Player is On.
        DVD Player is playing 'Inception'.

        Shutting down movie theater...
        Lights are On.
        Projector is Off.
        Amplifier is Off.
        DVD Player stopped playing.
        DVD Player is Off.
        

        ### Summary:

        - The Facade Design Pattern provides a simplified interface to a complex subsystem.
        - It hides the complexities of the subsystem from the client.
        - The Facade class delegates client requests to the appropriate subsystem classes.
        - In this example, `HomeTheaterFacade` simplifies the interaction with the home theater
            components (`DVDPlayer`, `Amplifier`, `Projector`, `Lights`) by providing easy-to-use methods (`WatchMovie`, `EndMovie`).
        - This pattern promotes loose coupling and enhances code readability and maintenance.
         */

        // Subsystem Class 1
        public class DVDPlayer
        {
            public void On()
            {
                Console.WriteLine("DVD Player is On.");
            }

            public void Off()
            {
                Console.WriteLine("DVD Player is Off.");
            }

            public void Play(string movie)
            {
                Console.WriteLine($"DVD Player is playing '{movie}'.");
            }

            public void Stop()
            {
                Console.WriteLine("DVD Player stopped playing.");
            }
        }

        // Subsystem Class 2
        public class Amplifier
        {
            public void On()
            {
                Console.WriteLine("Amplifier is On.");
            }

            public void Off()
            {
                Console.WriteLine("Amplifier is Off.");
            }

            public void SetVolume(int level)
            {
                Console.WriteLine($"Amplifier volume set to {level}.");
            }
        }

        // Subsystem Class 3
        public class Projector
        {
            public void On()
            {
                Console.WriteLine("Projector is On.");
            }

            public void Off()
            {
                Console.WriteLine("Projector is Off.");
            }

            public void WideScreenMode()
            {
                Console.WriteLine("Projector in widescreen mode (16x9 aspect ratio).");
            }
        }

        // Subsystem Class 4
        public class Lights
        {
            public void Dim(int level)
            {
                Console.WriteLine($"Lights dimmed to {level}%.");
            }

            public void On()
            {
                Console.WriteLine("Lights are On.");
            }

            public void Off()
            {
                Console.WriteLine("Lights are Off.");
            }
        }

        // Facade Class
        public class HomeTheaterFacade
        {
            private DVDPlayer dvdPlayer;
            private Amplifier amplifier;
            private Projector projector;
            private Lights lights;

            public HomeTheaterFacade(DVDPlayer dvdPlayer, Amplifier amplifier, Projector projector, Lights lights)
            {
                this.dvdPlayer = dvdPlayer;
                this.amplifier = amplifier;
                this.projector = projector;
                this.lights = lights;
            }

            public void WatchMovie(string movie)
            {
                Console.WriteLine("Get ready to watch a movie...");
                lights.Dim(10);
                projector.On();
                projector.WideScreenMode();
                amplifier.On();
                amplifier.SetVolume(5);
                dvdPlayer.On();
                dvdPlayer.Play(movie);
            }

            public void EndMovie()
            {
                Console.WriteLine("Shutting down movie theater...");
                lights.On();
                projector.Off();
                amplifier.Off();
                dvdPlayer.Stop();
                dvdPlayer.Off();
            }
        }

        // Client code
        public static void Driver()
        {
            // Create subsystem components
            DVDPlayer dvdPlayer = new DVDPlayer();
            Amplifier amplifier = new Amplifier();
            Projector projector = new Projector();
            Lights lights = new Lights();

            // Create the facade
            HomeTheaterFacade homeTheater = new HomeTheaterFacade(dvdPlayer, amplifier, projector, lights);

            // Use the facade to simplify the client interaction with the subsystem
            homeTheater.WatchMovie("Inception");
            Console.WriteLine();
            homeTheater.EndMovie();
        }
    }

    public static class Flyweight_6
    {
        /*
         6 Flyweight
            Scenario: When you need to minimize memory usage by sharing as much data as possible with similar objects.
            Example Use Cases:
            Caching
            Managing large numbers of fine-grained objects efficiently
            Text editors for character storage

        
        The Flyweight Design Pattern is a structural pattern that allows sharing of state among a large number of objects
        to reduce memory usage.It involves the use of a factory to manage the creation and sharing of flyweight objects.

        ### Implementation:

        Let's create an example where we have a `Tree` class with intrinsic (shared) and extrinsic (unshared) states
        We'll use a `TreeFactory` to manage the flyweight objects.
        

        ### Explanation:

        1. ** Flyweight Interface(`ITree`)**:
           - Declares the `Display` method that will be implemented by concrete flyweight objects.

        2. ** Concrete Flyweight Class(`Tree`)**:
           - Implements the `ITree` interface.
           - Contains the intrinsic state(e.g., `type`) that can be shared among multiple objects.
           - Implements the `Display` method, which also takes extrinsic state(e.g., `x`, `y` coordinates) as parameters.

        3. ** Flyweight Factory Class(`TreeFactory`)**:
           - Manages the creation and sharing of flyweight objects.
           - Contains a dictionary to store and reuse `Tree` objects based on their type.

        4. **Client Class (`Forest`)**:
           - Manages a collection of trees, each with its own coordinates.
           - Uses the `TreeFactory` to get shared `Tree` objects.
           - Stores tuples of `Tree` objects and their coordinates.

        5. **Client Code (`Program` Class)**:
           - Creates an instance of the `Forest` class.
           - Plants several trees, reusing `Tree` objects of the same type.
           - Displays the trees and their coordinates.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Displaying a Oak tree at (1, 2)
        Displaying a Pine tree at(3, 4)
        Displaying a Oak tree at(5, 6)
        Displaying a Pine tree at(7, 8)
        Displaying a Oak tree at(9, 10)
        

        ### Summary:

        - The Flyweight Design Pattern reduces memory usage by sharing common parts of the state among multiple objects.
        - The Flyweight interface (`ITree`) and its concrete implementation(`Tree`) separate intrinsic(shared) and extrinsic(unshared) states.
        - The Flyweight Factory(`TreeFactory`) manages the creation and reuse of flyweight objects, ensuring that only one instance of each type is created.
        - The client class (`Forest`) uses the factory to plant and display trees, demonstrating the benefits of shared state.
        - This pattern is useful in applications with a large number of similar objects, such as in graphical applications,
                text editors, or any system where memory usage is a concern.
         */



        // Flyweight interface
        public interface ITree
        {
            void Display(int x, int y);
        }

        // Concrete Flyweight class
        public class Tree : ITree
        {
            private string type;

            public Tree(string type)
            {
                this.type = type;
            }

            public void Display(int x, int y)
            {
                Console.WriteLine($"Displaying a {type} tree at ({x}, {y})");
            }
        }

        // Flyweight Factory class
        public class TreeFactory
        {
            private Dictionary<string, ITree> trees = new Dictionary<string, ITree>();

            public ITree GetTree(string type)
            {
                if (!trees.ContainsKey(type))
                {
                    trees[type] = new Tree(type);
                }
                return trees[type];
            }
        }

        // Client class
        public class Forest
        {
            private List<(ITree, int, int)> trees = new List<(ITree, int, int)>();
            private TreeFactory treeFactory = new TreeFactory();

            public void PlantTree(string type, int x, int y)
            {
                ITree tree = treeFactory.GetTree(type);
                trees.Add((tree, x, y));
            }

            public void DisplayTrees()
            {
                foreach (var (tree, x, y) in trees)
                {
                    tree.Display(x, y);
                }
            }
        }

        // Client code
        public static void Driver()
        {
            Forest forest = new Forest();

            // Plant trees
            forest.PlantTree("Oak", 1, 2);
            forest.PlantTree("Pine", 3, 4);
            forest.PlantTree("Oak", 5, 6);
            forest.PlantTree("Pine", 7, 8);
            forest.PlantTree("Oak", 9, 10);

            // Display trees
            forest.DisplayTrees();
        }
    }

    public static class Proxy_7
    {
        /*
         7 Proxy
            Scenario: When you need to provide a surrogate or placeholder for another object to control access to it.
            Example Use Cases:
            Lazy initialization
            Access control
            Remote proxies

        The Proxy Design Pattern is a structural pattern that provides a surrogate or placeholder for 
        another object to control access to it.Proxies are used to implement lazy initialization, access control, logging, and other purposes.

        ### Implementation:

        Let's create an example where we have an `IImage` interface with a `RealImage` class that loads an image from disk.
        We'll create a `ProxyImage` class to control access to the `RealImage` object.

        

        ### Explanation:

        1. ** Subject Interface(`IImage`)**:
           - Declares the `Display` method that both the `RealImage` and `ProxyImage` will implement.

        2. **Real Subject Class (`RealImage`)**:
           - Implements the `IImage` interface.
           - Contains the actual logic to load and display an image.
           - The `LoadFromDisk` method simulates loading the image from disk.

        3. **Proxy Class (`ProxyImage`)**:
           - Implements the `IImage` interface.
           - Contains a reference to a `RealImage` object.
           - Controls access to the `RealImage` by lazily initializing it when the `Display` method is called for the first time.

        4. ** Client Code(`Program` Class)**:
           - Creates instances of `ProxyImage`.
           - Calls the `Display` method on the proxy objects, demonstrating lazy initialization and controlled access to the real image.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Loading photo1.jpg from disk.
        Displaying photo1.jpg.

        Displaying photo1.jpg.

        Loading photo2.jpg from disk.
        Displaying photo2.jpg.

        Displaying photo2.jpg.
        

        ### Summary:

        - The Proxy Design Pattern provides a surrogate or placeholder for another object to control access to it.
        - The `IImage` interface defines the method to be implemented by both the `RealImage` and `ProxyImage`.
        - The `RealImage` class contains the actual logic for loading and displaying the image.
        - The `ProxyImage` class controls access to the `RealImage`,
                implementing lazy initialization by loading the image from disk only when it is needed for the first time.
        - This pattern is useful for implementing lazy initialization, access control,
                logging, and other purposes where controlling access to an object is necessary.
         */



        // Subject Interface
        public interface IImage
        {
            void Display();
        }

        // Real Subject Class
        public class RealImage : IImage
        {
            private string fileName;

            public RealImage(string fileName)
            {
                this.fileName = fileName;
                LoadFromDisk();
            }

            private void LoadFromDisk()
            {
                Console.WriteLine($"Loading {fileName} from disk.");
            }

            public void Display()
            {
                Console.WriteLine($"Displaying {fileName}.");
            }
        }

        // Proxy Class
        public class ProxyImage : IImage
        {
            private RealImage realImage;
            private string fileName;

            public ProxyImage(string fileName)
            {
                this.fileName = fileName;
            }

            public void Display()
            {
                if (realImage == null)
                {
                    realImage = new RealImage(fileName);
                }
                realImage.Display();
            }
        }

        // Client Code
        public static void Driver()
        {
            IImage image1 = new ProxyImage("photo1.jpg");
            IImage image2 = new ProxyImage("photo2.jpg");

            // Image will be loaded from disk and displayed
            image1.Display();
            Console.WriteLine();

            // Image will not be loaded from disk again, but will be displayed
            image1.Display();
            Console.WriteLine();

            // Image will be loaded from disk and displayed
            image2.Display();
            Console.WriteLine();

            // Image will not be loaded from disk again, but will be displayed
            image2.Display();
        }
    }
}
