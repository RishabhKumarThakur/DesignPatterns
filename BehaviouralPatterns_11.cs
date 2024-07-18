using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System;
using static BehaviouralPatterns.Command_2_IMP;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Reflection.Metadata.BlobBuilder;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using static BehaviouralPatterns.Interpreter_3;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Channels;
using static BehaviouralPatterns.Mediator_5_IMP;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Xml;
using static BehaviouralPatterns.State_8_IMP;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using static CreationalPatterns.Prototype_5;

namespace BehaviouralPatterns
{
    public class ChainOfResponsibility_1
    {
        /*
         1 Chain of Responsibility
            Scenario: When you need to pass a request along a chain of handlers.
            Example Use Cases:
            Logging systems
            Event handling systems
            Processing sequences

        The Chain of Responsibility Design Pattern is a behavioral pattern that allows a request to pass through a chain of handlers.
         Each handler either handles the request or passes it to the next handler in the chain.

        ### Implementation:

        Let's create an example where we have different levels of logging (`InfoLogger`, `ErrorLogger`, `DebugLogger`).
                    We'll create a chain of these loggers to handle log messages based on their severity.

        

        ### Explanation:

        1. ** Abstract Handler(`Logger`)**:
           - Declares the `SetNextLogger` method to set the next handler in the chain.
           - Declares the `LogMessage` method to process the request and pass it along the chain.
           - Contains an abstract method `Write` to be implemented by concrete handlers.

        2. **Concrete Handlers (`InfoLogger`, `DebugLogger`, `ErrorLogger`)**:
           - Each concrete handler class implements the `Write` method.
           - Each handler processes the log message if its level is equal to or greater than the handler's level.

        3. ** Client Code(`Program` Class)**:
           - Defines the log levels(`INFO`, `DEBUG`, `ERROR`).
           - Creates the chain of loggers and sets the chain order.
           - Sends log messages with different levels to the logger chain.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        Info Logger: This is an information.

        Info Logger: This is a debug level information.
        Debug Logger: This is a debug level information.

        Info Logger: This is an error information.
        Debug Logger: This is an error information.
        Error Logger: This is an error information.
        

        ### Summary:

        - The Chain of Responsibility Design Pattern allows a request to pass through a chain of handlers.
        - Each handler decides whether to handle the request or pass it to the next handler in the chain.
        - The abstract `Logger` class defines the structure for handling the request and chaining handlers.
        - The concrete handler classes (`InfoLogger`, `DebugLogger`, `ErrorLogger`) implement the actual handling logic.
        - This pattern promotes flexibility and loose coupling,
                allowing the chain structure to be easily modified by adding, removing, or reordering handlers.

         */


        // Abstract Handler
        public abstract class Logger
        {
            protected Logger nextLogger;
            protected int level;

            public void SetNextLogger(Logger nextLogger)
            {
                this.nextLogger = nextLogger;
            }

            public void LogMessage(int level, string message)
            {
                if (this.level <= level)
                {
                    Write(message);
                }
                if (nextLogger != null)
                {
                    nextLogger.LogMessage(level, message);
                }
            }

            protected abstract void Write(string message);
        }

        // Concrete Handlers
        public class InfoLogger : Logger
        {
            public InfoLogger(int level)
            {
                this.level = level;
            }

            protected override void Write(string message)
            {
                Console.WriteLine("Info Logger: " + message);
            }
        }

        public class DebugLogger : Logger
        {
            public DebugLogger(int level)
            {
                this.level = level;
            }

            protected override void Write(string message)
            {
                Console.WriteLine("Debug Logger: " + message);
            }
        }

        public class ErrorLogger : Logger
        {
            public ErrorLogger(int level)
            {
                this.level = level;
            }

            protected override void Write(string message)
            {
                Console.WriteLine("Error Logger: " + message);
            }
        }

        // Client code
        public static class DriverClass
        {
            // Logger levels
            public static int INFO = 1;
            public static int DEBUG = 2;
            public static int ERROR = 3;

            private static Logger GetChainOfLoggers()
            {
                Logger errorLogger = new ErrorLogger(ERROR);
                Logger debugLogger = new DebugLogger(DEBUG);
                Logger infoLogger = new InfoLogger(INFO);

                infoLogger.SetNextLogger(debugLogger);
                debugLogger.SetNextLogger(errorLogger);

                return infoLogger;
            }

            public static void Driver()
            {
                Logger loggerChain = GetChainOfLoggers();

                loggerChain.LogMessage(INFO, "This is an information.");
                Console.WriteLine();
                loggerChain.LogMessage(DEBUG, "This is a debug level information.");
                Console.WriteLine();
                loggerChain.LogMessage(ERROR, "This is an error information.");
            }
        }
    }

    public class Command_2_IMP
    {
        /*
         
        2 Command ✅
            Scenario: When you need to parameterize objects with operations, delay operations, or queue operations.
            Example Use Cases:
            Implementing undo/redo functionality
            Task scheduling
            Logging changes

        The Command Design Pattern is a behavioral pattern that turns a request into a stand-alone object that
                contains all information about the request.This transformation allows for parameterization of clients with different requests
                queuing of requests, and logging the requests.

        ### Implementation:

        Let's create an example where we have a `Light` class representing a light that can be turned on or off.
                We'll create `Command` objects to encapsulate the actions of turning the light on and off.
        

        ### Explanation:

        1. ** Command Interface(`ICommand`)**:
           - Declares the `Execute` and `Undo` methods that all concrete commands must implement.

        2. ** Receiver Class(`Light`)**:
           - Contains the actual operations to be performed(`On` and `Off` methods).

        3. ** Concrete Commands(`LightOnCommand`, `LightOffCommand`)**:
           - Implement the `ICommand` interface.
           - Encapsulate the receiver object and call its methods to perform the action.
           - Provide an `Undo` method to reverse the action.

        4. ** Invoker Class(`RemoteControl`)**:
           - Stores a command object.
           - Calls the `Execute` method of the command when the button is pressed.
           - Calls the `Undo` method of the command when the undo button is pressed.

        5. ** Client Code(`Program` Class)**:
           - Creates the receiver(`Light`), concrete commands(`LightOnCommand`, `LightOffCommand`), and the invoker(`RemoteControl`).
           - Configures the invoker with the command and simulates button presses.

        ### Output:

        When you run the `Main` method, you will see the following output:

        
        The light is on.
        The light is off.

        The light is off.
        The light is on.
        

        ### Summary:

        - The Command Design Pattern encapsulates a request as an object,
                thereby allowing for parameterization of clients with queues, requests, and operations.
        - The `ICommand` interface defines methods for executing and undoing actions.
        - The `Light` class acts as the receiver of the requests.
        - The `LightOnCommand` and `LightOffCommand` classes implement the `ICommand` interface,
                encapsulating the actions of turning the light on and off.
        - The `RemoteControl` class acts as the invoker, executing and undoing commands.
        - This pattern promotes loose coupling by separating the request sender and receiver,
                making it easier to add new commands without changing existing code.
         */

        // Command Interface
        public interface ICommand
        {
            void Execute();
            void Undo();
        }

        // Receiver Class
        public class Light
        {
            public void On()
            {
                Console.WriteLine("The light is on.");
            }

            public void Off()
            {
                Console.WriteLine("The light is off.");
            }
        }

        // Concrete Command for turning on the light
        public class LightOnCommand : ICommand
        {
            private Light light;

            public LightOnCommand(Light light)
            {
                this.light = light;
            }

            public void Execute()
            {
                light.On();
            }

            public void Undo()
            {
                light.Off();
            }
        }

        // Concrete Command for turning off the light
        public class LightOffCommand : ICommand
        {
            private Light light;

            public LightOffCommand(Light light)
            {
                this.light = light;
            }

            public void Execute()
            {
                light.Off();
            }

            public void Undo()
            {
                light.On();
            }
        }

        // Invoker Class
        public class RemoteControl
        {
            private ICommand command;

            public void SetCommand(ICommand command)
            {
                this.command = command;
            }

            public void PressButton()
            {
                command.Execute();
            }

            public void PressUndo()
            {
                command.Undo();
            }
        }

        // Client Code
        public static void Driver()
        {
            // Receiver
            Light light = new Light();

            // Concrete Commands
            ICommand lightOn = new LightOnCommand(light);
            ICommand lightOff = new LightOffCommand(light);

            // Invoker
            RemoteControl remote = new RemoteControl();

            // Turn the light on
            remote.SetCommand(lightOn);
            remote.PressButton();
            remote.PressUndo();
            Console.WriteLine();

            // Turn the light off
            remote.SetCommand(lightOff);
            remote.PressButton();
            remote.PressUndo();
        }
    }

    public class Interpreter_3
    {
        /*
         3 Interpreter
            Scenario: When you need to interpret sentences in a language.
            Example Use Cases:
            Simple scripting languages
            Parsing expressions

                The Interpreter Design Pattern is a behavioral pattern that provides a way to evaluate language grammar or expressions.
        This pattern involves defining a grammar for a simple language and using an interpreter to process sentences in the language.

        ### Implementation:

        Let's create an example where we have a simple language for arithmetic operations (addition and subtraction).
                We'll create an interpreter that can evaluate expressions in this language.

        ```

        ### Explanation:

        1. ** Abstract Expression(`IExpression`)**:
           - Declares the `Interpret` method that will be implemented by terminal and non-terminal expressions.

        2. **Terminal Expression (`Number`)**:
           - Implements the `IExpression` interface.
           - Represents a number in the expression.
           - The `Interpret` method returns the number.

        3. **Non-terminal Expressions (`Add`, `Subtract`)**:
           - Implement the `IExpression` interface.
           - Represent addition and subtraction operations.
           - The `Interpret` method performs the respective operation on the results of interpreting the left and right expressions.

        4. ** Context Class(`Context`)**:
           - Stores the variables and their values.
           - Provides methods to assign values to variables and to look up variable values.

        5. **Client Code (`Program` Class)**:
           - Creates a context and assigns values to variables.
           - Creates an expression that involves addition and subtraction.
           - Interprets the expression and prints the result.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        Result of the expression is: -5
        ```

        ### Summary:

        - The Interpreter Design Pattern provides a way to evaluate language grammar or expressions.
        - The `IExpression` interface defines a method for interpreting expressions.
        - The `Number` class is a terminal expression that returns a number.
        - The `Add` and `Subtract` classes are non-terminal expressions that perform addition and subtraction, respectively.
        - The `Context` class stores variables and their values, providing methods to assign and look up values.
        - The client code creates and interprets expressions using these classes,
                demonstrating how the interpreter pattern can be used to evaluate simple arithmetic expressions.
         */

        // Abstract Expression
        public interface IExpression
        {
            int Interpret(Dictionary<string, int> context);
        }

        // Terminal Expression
        public class Number : IExpression
        {
            private int number;

            public Number(int number)
            {
                this.number = number;
            }

            public int Interpret(Dictionary<string, int> context)
            {
                return number;
            }
        }

        // Non-terminal Expression for addition
        public class Add : IExpression
        {
            private IExpression leftExpression;
            private IExpression rightExpression;

            public Add(IExpression left, IExpression right)
            {
                this.leftExpression = left;
                this.rightExpression = right;
            }

            public int Interpret(Dictionary<string, int> context)
            {
                return leftExpression.Interpret(context) + rightExpression.Interpret(context);
            }
        }

        // Non-terminal Expression for subtraction
        public class Subtract : IExpression
        {
            private IExpression leftExpression;
            private IExpression rightExpression;

            public Subtract(IExpression left, IExpression right)
            {
                this.leftExpression = left;
                this.rightExpression = right;
            }

            public int Interpret(Dictionary<string, int> context)
            {
                return leftExpression.Interpret(context) - rightExpression.Interpret(context);
            }
        }

        // Context Class
        public class Context
        {
            private Dictionary<string, int> variables = new Dictionary<string, int>();

            public void Assign(string variable, int value)
            {
                variables[variable] = value;
            }

            public int Lookup(string variable)
            {
                return variables[variable];
            }
        }

        // Client Code
        public static void Driver()
        {
            // Creating the context with variables
            Context context = new Context();
            context.Assign("x", 10);
            context.Assign("y", 20);

            // Creating the expressions
            IExpression expression = new Add(new Number(5), new Subtract(new Number(context.Lookup("x")), new Number(context.Lookup("y"))));

            // Interpreting the expression
            int result = expression.Interpret(new Dictionary<string, int>());
            Console.WriteLine($"Result of the expression is: {result}");
        }

    }

    public class Iterator_4_IMP
    {
        /*
         4 Iterator ✅
        Scenario:
        When you need to provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.
        Example Use Cases:
        Traversing collections (e.g., lists, trees, hash tables)
        Providing uniform iteration interfaces for different types of collections

        The Iterator Design Pattern is a behavioral pattern that provides a way to access the elements of an aggregate object 
        sequentially without exposing its underlying representation.
        This pattern involves creating an iterator object that can traverse a collection.

        ### Implementation:

        Let's create an example where we have a collection of `Book` objects. We'll create an iterator to traverse this collection.

        ```

        ### Explanation:

        1. ** Aggregate Interface(`IBookCollection`)**:
           - Declares the `CreateIterator` method that returns an iterator for the collection.

        2. **Concrete Aggregate (`BookCollection`)**:
           - Implements the `IBookCollection` interface.
           - Contains a collection of `Book` objects.
           - Provides methods to add books and to get the iterator.
           - Indexer and `Count` property are used to access books and their count.

        3. **Iterator Interface (`IBookIterator`)**:
           - Declares methods for traversing the collection: `HasNext` and `Next`.

        4. ** Concrete Iterator(`BookIterator`)**:
           - Implements the `IBookIterator` interface.
           - Contains logic for iterating over the `BookCollection`.
           - Maintains a current index to track the iteration progress.

        5. ** Element Class(`Book`)**:
           - Represents an element in the collection.
           - Contains a property `Title` to store the book's title.

        6. **Client Code (`Program` Class)**:
           - Creates a `BookCollection` and adds several books to it.
           - Creates an iterator for the collection and traverses the books using the iterator.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        Book: Design Patterns: Elements of Reusable Object-Oriented Software
        Book: Clean Code: A Handbook of Agile Software Craftsmanship
        Book: The Pragmatic Programmer: Your Journey to Mastery
        Book: Refactoring: Improving the Design of Existing Code
        ```

        ### Summary:

        - The Iterator Design Pattern provides a way to access the elements of an
        aggregate object sequentially without exposing its underlying representation.
        - The `IBookCollection` interface defines a method for creating an iterator.
        - The `BookCollection` class implements this interface and contains a collection of `Book` objects.
        - The `IBookIterator` interface defines methods for traversing the collection.
        - The `BookIterator` class implements this interface, providing the logic for iteration.
        - The `Book` class represents elements in the collection.
        - The client code demonstrates creating a collection, adding elements, and using an iterator to traverse the collection.
         */

        // Aggregate Interface
        public interface IBookCollection
        {
            IBookIterator CreateIterator();
        }

        // Concrete Aggregate
        public class BookCollection : IBookCollection
        {
            private List<Book> books = new List<Book>();

            public void AddBook(Book book)
            {
                books.Add(book);
            }

            public IBookIterator CreateIterator()
            {
                return new BookIterator(this);
            }

            public int Count
            {
                get { return books.Count; }
            }

            public Book this[int index]
            {
                get { return books[index]; }
            }
        }

        // Iterator Interface
        public interface IBookIterator
        {
            bool HasNext();
            Book Next();
        }

        // Concrete Iterator
        public class BookIterator : IBookIterator
        {
            private BookCollection bookCollection;
            private int currentIndex = 0;

            public BookIterator(BookCollection bookCollection)
            {
                this.bookCollection = bookCollection;
            }

            public bool HasNext()
            {
                return currentIndex < bookCollection.Count;
            }

            public Book Next()
            {
                return bookCollection[currentIndex++];
            }
        }

        // Element Class
        public class Book
        {
            public string Title { get; set; }

            public Book(string title)
            {
                Title = title;
            }
        }

        // Client Code
        public static void Driver()
        {
            BookCollection bookCollection = new BookCollection();

            bookCollection.AddBook(new Book("Design Patterns: Elements of Reusable Object-Oriented Software"));
            bookCollection.AddBook(new Book("Clean Code: A Handbook of Agile Software Craftsmanship"));
            bookCollection.AddBook(new Book("The Pragmatic Programmer: Your Journey to Mastery"));
            bookCollection.AddBook(new Book("Refactoring: Improving the Design of Existing Code"));

            IBookIterator iterator = bookCollection.CreateIterator();

            while (iterator.HasNext())
            {
                Book book = iterator.Next();
                Console.WriteLine("Book: " + book.Title);
            }
        }
    }

    public class Mediator_5_IMP
    {
        /*
         5 Mediator ✅
            Scenario: When you need to reduce the complexity of communication between multiple objects.
            Example Use Cases:
            Implementing chat applications
            Coordinating complex interactions between objects
            Centralizing communication logic

            The Mediator Design Pattern is a behavioral pattern that reduces the complexity of communication between multiple objects by centralizing communication through a mediator object. This pattern promotes loose coupling by preventing objects from referring to each other explicitly, allowing their interaction to be varied independently.

            ### Implementation:

            Let's create an example where we have several `User` objects that can send messages to each other through a `ChatRoom` mediator.

            ```

            ### Explanation:

            1. ** Mediator Interface(`IChatRoomMediator`)**:
               - Declares methods for sending messages and adding users.

            2. ** Concrete Mediator(`ChatRoom`)**:
               - Implements the `IChatRoomMediator` interface.
               - Contains a list of users and handles the communication between them.
               - The `SendMessage` method sends messages to all users except the sender.

            3. ** Colleague Class(`User`)**:
               - Represents a user in the chat room.
               - Contains a reference to the mediator and interacts with other users through it.
               - The `Send` method sends a message through the mediator, while the `Receive` method handles receiving messages.

            4. **Client Code (`Program` Class)**:
               - Creates the `ChatRoom` mediator.
               - Creates several `User` objects and adds them to the chat room.
               - Demonstrates sending messages through the mediator.

            ### Output:

            When you run the `Main` method, you will see the following output:

            ```
            Alice sends: Hi everyone!
            Bob receives: Hi everyone!
            Charlie receives: Hi everyone!
            Diana receives: Hi everyone!

            Bob sends: Hello Alice!
            Alice receives: Hello Alice!
            Charlie receives: Hello Alice!
            Diana receives: Hello Alice!
            ```

            ### Summary:

            - The Mediator Design Pattern centralizes communication between multiple objects,
            promoting loose coupling by preventing objects from referring to each other explicitly.
            - The `IChatRoomMediator` interface defines methods for sending messages and adding users.
            - The `ChatRoom` class implements this interface and handles the communication between users.
            - The `User` class represents colleagues that communicate through the mediator.
            - The client code demonstrates creating a mediator, adding users, and using the mediator to send and receive messages.
            - This pattern simplifies the communication logic and makes it easier to manage and modify interactions between objects.
         */

        // Mediator Interface
        public interface IChatRoomMediator
        {
            void SendMessage(string message, User user);
            void AddUser(User user);
        }

        // Concrete Mediator
        public class ChatRoom : IChatRoomMediator
        {
            private List<User> users;

            public ChatRoom()
            {
                users = new List<User>();
            }

            public void AddUser(User user)
            {
                users.Add(user);
            }

            public void SendMessage(string message, User user)
            {
                foreach (var u in users)
                {
                    // Message should not be received by the user sending it
                    if (u != user)
                    {
                        u.Receive(message);
                    }
                }
            }
        }

        // Colleague Class
        public class User
        {
            private IChatRoomMediator chatRoom;
            public string Name { get; private set; }

            public User(string name, IChatRoomMediator chatRoom)
            {
                this.Name = name;
                this.chatRoom = chatRoom;
            }

            public void Send(string message)
            {
                Console.WriteLine(this.Name + " sends: " + message);
                chatRoom.SendMessage(message, this);
            }

            public void Receive(string message)
            {
                Console.WriteLine(this.Name + " receives: " + message);
            }
        }

        // Client Code
        public static void Driver()
        {
            IChatRoomMediator chatRoom = new ChatRoom();

            User user1 = new User("Alice", chatRoom);
            User user2 = new User("Bob", chatRoom);
            User user3 = new User("Charlie", chatRoom);
            User user4 = new User("Diana", chatRoom);

            chatRoom.AddUser(user1);
            chatRoom.AddUser(user2);
            chatRoom.AddUser(user3);
            chatRoom.AddUser(user4);

            user1.Send("Hi everyone!");
            Console.WriteLine();
            user2.Send("Hello Alice!");
        }
    }

    public class Memento_6
    {
        /*
         6 Memento
            Scenario: When you need to capture and restore an object's state.
            Example Use Cases:
            Undo mechanisms in applications
            Saving/restoring states

        The Memento Design Pattern is a behavioral pattern that allows you to capture and restore the internal state of an object
        without exposing its internal structure.This is useful for implementing undo/redo functionality.

        ### Implementation:

        Let's create an example where we have an `Originator` class representing a text editor.
        We'll create a `Memento` class to store the state of the `Originator`, and a `Caretaker` class to manage saving and restoring states.

        ```

        ### Explanation:

        1. ** Memento Class(`Memento`)**:
            - Stores the state of the `Originator`.
            - Provides a method to retrieve the stored state.

        2. **Originator Class (`TextEditor`)**:
            - Represents the object whose state needs to be saved and restored.
            - Provides methods to set and get the state.
            - Creates a `Memento` to store its state and restores its state from a `Memento`.

        3. **Caretaker Class (`Caretaker`)**:
            - Manages the `Memento` objects.
            - Provides methods to add and retrieve `Memento` objects.

        4. ** Client Code(`Program` Class)**:
            - Creates an `Originator` and a `Caretaker`.
            - Sets and saves multiple states in the `Originator`.
            - Restores states from the `Caretaker` to the `Originator`.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        Current State: State #1
        Saving state to Memento.
        Current State: State #2
        Saving state to Memento.
        Current State: State #3

        State restored from Memento: State #2

        State restored from Memento: State #1
        ```

        ### Summary:

        - The Memento Design Pattern allows capturing and restoring the internal state of an object without exposing its internal structure.
        - The `Memento` class stores the state of the `Originator`.
        - The `TextEditor` (Originator) class represents the object whose state is saved and restored.
        - The `Caretaker` class manages the `Memento` objects, storing and retrieving them as needed.
        - The client code demonstrates setting states in the `TextEditor`,
                saving those states using `Memento` objects managed by the `Caretaker`, and restoring the states when needed.
        - This pattern is useful for implementing undo/redo functionality and other scenarios where state management is crucial.
         */



        // Memento Class
        public class Memento
        {
            public string State { get; private set; }

            public Memento(string state)
            {
                State = state;
            }
        }

        // Originator Class
        public class TextEditor
        {
            private string state;

            public void SetState(string state)
            {
                this.state = state;
                Console.WriteLine("Current State: " + state);
            }

            public string GetState()
            {
                return state;
            }

            public Memento SaveStateToMemento()
            {
                Console.WriteLine("Saving state to Memento.");
                return new Memento(state);
            }

            public void GetStateFromMemento(Memento memento)
            {
                state = memento.State;
                Console.WriteLine("State restored from Memento: " + state);
            }
        }

        // Caretaker Class
        public class Caretaker
        {
            private List<Memento> mementoList = new List<Memento>();

            public void Add(Memento state)
            {
                mementoList.Add(state);
            }

            public Memento Get(int index)
            {
                return mementoList[index];
            }
        }

        // Client Code
        public static void Driver()
        {
            TextEditor textEditor = new TextEditor();
            Caretaker caretaker = new Caretaker();

            // Set initial state
            textEditor.SetState("State #1");
            caretaker.Add(textEditor.SaveStateToMemento());

            // Change state
            textEditor.SetState("State #2");
            caretaker.Add(textEditor.SaveStateToMemento());

            // Change state again
            textEditor.SetState("State #3");
            Console.WriteLine();

            // Restore to previous state
            textEditor.GetStateFromMemento(caretaker.Get(1));
            Console.WriteLine();

            // Restore to initial state
            textEditor.GetStateFromMemento(caretaker.Get(0));
        }

    }

    public class Observer_7_IMP
    {
        /*
         7 Observer ✅
            Scenario: When an object needs to notify other objects without knowing who or how many objects need to be notified.
            Example Use Cases:
            Event handling systems
            Implementing distributed event-handling systems
            GUI frameworks

                The Observer Design Pattern is a behavioral pattern that defines a one-to-many dependency between objects so that when one object changes state, 
        all its dependents are notified and updated automatically.This pattern is often used for implementing distributed event handling systems.

        ### Implementation:

        Let's create an example where we have a `Subject` (e.g., `WeatherStation`) 
        that maintains a list of observers (e.g., `WeatherDisplay`). When the `Subject`'s state changes, it notifies all registered observers.

        ```

        ### Explanation:

        1. ** Subject Interface(`IWeatherStation`)**:
           - Declares methods for registering, removing, and notifying observers.

        2. ** Concrete Subject(`WeatherStation`)**:
           - Implements the `IWeatherStation` interface.
           - Maintains a list of observers and notifies them of changes in temperature.

        3. ** Observer Interface(`IWeatherObserver`)**:
           - Declares an `Update` method that observers must implement to receive updates.

        4. **Concrete Observer (`WeatherDisplay`)**:
           - Implements the `IWeatherObserver` interface.
           - Receives updates from the `WeatherStation` and displays the temperature.

        5. **Client Code (`Program` Class)**:
           - Creates a `WeatherStation` and registers two `WeatherDisplay` observers.
           - Changes the temperature in the `WeatherStation`, triggering notifications to the observers.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        WeatherStation: New temperature is 25
        Display 1: Current temperature is 25
        Display 2: Current temperature is 25

        WeatherStation: New temperature is 30
        Display 1: Current temperature is 30
        Display 2: Current temperature is 30
        ```

        ### Summary:

        - The Observer Design Pattern defines a one-to-many dependency between objects
                so that when one object changes state, all its dependents are notified and updated automatically.
        - The `IWeatherStation` interface declares methods for managing observers.
        - The `WeatherStation` class implements this interface, maintaining a list of observers and notifying them of state changes.
        - The `IWeatherObserver` interface declares an `Update` method for receiving updates.
        - The `WeatherDisplay` class implements this interface, receiving and displaying temperature updates.
        - The client code demonstrates creating a subject and observers,
                changing the subject's state, and observing the automatic updates to the observers.

         */

        // Subject Interface
        public interface IWeatherStation
        {
            void RegisterObserver(IWeatherObserver observer);
            void RemoveObserver(IWeatherObserver observer);
            void NotifyObservers();
        }

        // Concrete Subject
        public class WeatherStation : IWeatherStation
        {
            private List<IWeatherObserver> observers;
            private float temperature;

            public WeatherStation()
            {
                observers = new List<IWeatherObserver>();
            }

            public void RegisterObserver(IWeatherObserver observer)
            {
                observers.Add(observer);
            }

            public void RemoveObserver(IWeatherObserver observer)
            {
                observers.Remove(observer);
            }

            public void NotifyObservers()
            {
                foreach (var observer in observers)
                {
                    observer.Update(temperature);
                }
            }

            public void SetTemperature(float temperature)
            {
                this.temperature = temperature;
                Console.WriteLine("WeatherStation: New temperature is " + temperature);
                NotifyObservers();
            }
        }

        // Observer Interface
        public interface IWeatherObserver
        {
            void Update(float temperature);
        }

        // Concrete Observer
        public class WeatherDisplay : IWeatherObserver
        {
            private string name;
            private float temperature;

            public WeatherDisplay(string name)
            {
                this.name = name;
            }

            public void Update(float temperature)
            {
                this.temperature = temperature;
                Display();
            }

            public void Display()
            {
                Console.WriteLine(name + ": Current temperature is " + temperature);
            }
        }

        // Client Code
        public static void Driver()
        {
            WeatherStation weatherStation = new WeatherStation();

            WeatherDisplay display1 = new WeatherDisplay("Display 1");
            WeatherDisplay display2 = new WeatherDisplay("Display 2");

            weatherStation.RegisterObserver(display1);
            weatherStation.RegisterObserver(display2);

            weatherStation.SetTemperature(25.0f);
            Console.WriteLine();

            weatherStation.SetTemperature(30.0f);
        }
    }

    public class State_8_IMP
    {
        /*
         8 State ✅
            Scenario: When an object should change its behavior when its internal state changes.
            Example Use Cases:
            Implementing state machines
            Workflow systems
            Game development for character states

            The State Design Pattern is a behavioral pattern that allows an object to change its behavior when its internal state changes.
        The object will appear to change its class. This pattern is used to encapsulate the behavior associated with a particular state in different classes.

        ### Implementation:

        Let's create an example where we have a `Context` class representing a `Document`.
                    The document can be in different states: `Draft`, `Moderation`, and `Published`.
                        Each state will have specific behaviors associated with it.

        ```

        ### Explanation:

        1. ** State Interface(`IDocumentState`)**:
           - Declares methods for actions that change depending on the state: `Render` and `Publish`.

        2. ** Concrete States(`DraftState`, `ModerationState`, `PublishedState`)**:
           - Implement the `IDocumentState` interface.
           - Each concrete state defines specific behavior for the `Render` and `Publish` methods.

        3. ** Context Class(`Document`)**:
           - Maintains an instance of a `IDocumentState` subclass that defines the current state.
           - Provides methods to change state (`SetState`) and to delegate behaviors to the current state (`Render` and `Publish`).

        4. ** Client Code(`Program` Class)**:
           - Creates a `Document` object.
           - Calls `Render` and `Publish` methods to observe how the behavior changes as the state changes.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        Rendering the document in draft mode.
        Document is now in moderation.

        Rendering the document in moderation mode.
        Document has been published.

        Rendering the document in published mode.
        Document is already published.
        ```

        ### Summary:

        - The State Design Pattern allows an object to change its behavior when its internal state changes.
        - The `IDocumentState` interface defines methods for actions that vary by state.
        - The `DraftState`, `ModerationState`, and `PublishedState` classes implement specific behaviors for each state.
        - The `Document` class maintains the current state and delegates behavior to the current state object.
        - The client code demonstrates how the behavior of the `Document` object changes as its state changes.using System;
         */

        // State Interface
        public interface IDocumentState
        {
            void Render(Document context);
            void Publish(Document context);
        }

        // Concrete State for Draft
        public class DraftState : IDocumentState
        {
            public void Render(Document context)
            {
                Console.WriteLine("Rendering the document in draft mode.");
            }

            public void Publish(Document context)
            {
                Console.WriteLine("Document is now in moderation.");
                context.SetState(new ModerationState());
            }
        }

        // Concrete State for Moderation
        public class ModerationState : IDocumentState
        {
            public void Render(Document context)
            {
                Console.WriteLine("Rendering the document in moderation mode.");
            }

            public void Publish(Document context)
            {
                Console.WriteLine("Document has been published.");
                context.SetState(new PublishedState());
            }
        }

        // Concrete State for Published
        public class PublishedState : IDocumentState
        {
            public void Render(Document context)
            {
                Console.WriteLine("Rendering the document in published mode.");
            }

            public void Publish(Document context)
            {
                Console.WriteLine("Document is already published.");
            }
        }

        // Context Class
        public class Document
        {
            private IDocumentState currentState;

            public Document()
            {
                currentState = new DraftState(); // Default state
            }

            public void SetState(IDocumentState state)
            {
                currentState = state;
            }

            public void Render()
            {
                currentState.Render(this);
            }

            public void Publish()
            {
                currentState.Publish(this);
            }
        }

        // Client Code
        public static void Driver()
        {
            Document document = new Document();

            document.Render();
            document.Publish();

            Console.WriteLine();

            document.Render();
            document.Publish();

            Console.WriteLine();

            document.Render();
            document.Publish();
        }
    }

    public class Stratergy_9_IMP
    {
        /*
         9 Strategy ✅
            Scenario: When a class needs to use multiple algorithms interchangeably or when an algorithm needs to be selected and executed at runtime.
            Example Use Cases:
            Different sorting algorithms
            Payment methods in an e-commerce application
            Various compression algorithms

        The Strategy Design Pattern is a behavioral pattern that allows you to define a family of algorithms,
        encapsulate each one as an object, and make them interchangeable.
        This pattern lets the algorithm vary independently from clients that use it.

        ### Implementation:

        Let's create an example where we have a `TextEditor` class that can format text using different strategies:
        `UpperCaseStrategy`, `LowerCaseStrategy`, and `TitleCaseStrategy`.

        ```

        ### Explanation:

        1. ** Strategy Interface(`ITextFormatter`)**:
           - Declares the `Format` method that all concrete strategies must implement.

        2. ** Concrete Strategies(`UpperCaseFormatter`, `LowerCaseFormatter`, `TitleCaseFormatter`)**:
           - Implement the `ITextFormatter` interface.
           - Each strategy defines its own way of formatting text.

        3. ** Context Class(`TextEditor`)**:
           - Maintains a reference to a `ITextFormatter` strategy.
           - Provides a method to set the current strategy(`SetTextFormatter`).
           - Uses the current strategy to format text in the `PublishText` method.

        4. ** Client Code(`Program` Class)**:
           - Creates a `TextEditor` object.
           - Sets different text formatting strategies and publishes text to observe how the output changes.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        HELLO WORLD FROM THE STRATEGY PATTERN

        hello world from the strategy pattern

        Hello World From The Strategy Pattern
        ```

        ### Summary:

        - The Strategy Design Pattern allows you to define a family of algorithms,
        encapsulate each one as an object, and make them interchangeable.
        - The `ITextFormatter` interface defines the `Format` method for text formatting strategies.
        - The `UpperCaseFormatter`, `LowerCaseFormatter`, and `TitleCaseFormatter` classes implement specific text formatting strategies.
        - The `TextEditor` class maintains the current strategy and delegates the formatting task to it.
        - The client code demonstrates how the behavior of the `TextEditor` object changes as its strategy changes.
         */

        // Strategy Interface
        public interface ITextFormatter
        {
            string Format(string text);
        }

        // Concrete Strategy for Upper Case
        public class UpperCaseFormatter : ITextFormatter
        {
            public string Format(string text)
            {
                return text.ToUpper();
            }
        }

        // Concrete Strategy for Lower Case
        public class LowerCaseFormatter : ITextFormatter
        {
            public string Format(string text)
            {
                return text.ToLower();
            }
        }

        // Concrete Strategy for Title Case
        public class TitleCaseFormatter : ITextFormatter
        {
            public string Format(string text)
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
            }
        }

        // Context Class
        public class TextEditor
        {
            private ITextFormatter textFormatter;

            public void SetTextFormatter(ITextFormatter formatter)
            {
                textFormatter = formatter;
            }

            public void PublishText(string text)
            {
                if (textFormatter != null)
                {
                    string formattedText = textFormatter.Format(text);
                    Console.WriteLine(formattedText);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }

        // Client Code
        public static void Driver()
        {
            TextEditor editor = new TextEditor();

            string sampleText = "hello world from the strategy pattern";

            // Use UpperCaseFormatter
            editor.SetTextFormatter(new UpperCaseFormatter());
            editor.PublishText(sampleText);

            Console.WriteLine();

            // Use LowerCaseFormatter
            editor.SetTextFormatter(new LowerCaseFormatter());
            editor.PublishText(sampleText);

            Console.WriteLine();

            // Use TitleCaseFormatter
            editor.SetTextFormatter(new TitleCaseFormatter());
            editor.PublishText(sampleText);
        }
    }

    public class TemplateMethod_10_IMP
    {
        /*
         10 Template Method ✅
            Scenario: When you need to define the skeleton of an algorithm in an operation, deferring some steps to subclasses.
            Example Use Cases:
            Algorithm framework
            Game development for defining game loops
            Workflow systems

        
        The Template Method Design Pattern is a behavioral pattern that defines the skeleton
        of an algorithm in a base class while allowing subclasses to override specific steps of the algorithm without changing its structure.

        ### Implementation:

        Let's create an example where we have an abstract `DocumentGenerator` class that defines the template method `GenerateDocument`.
                    We'll create concrete subclasses for generating different types of documents, such as `PDFDocumentGenerator` and `WordDocumentGenerator`.

        ```

        ### Explanation:

        1. ** Abstract Class(`DocumentGenerator`)**:
           - Defines the template method `GenerateDocument` which outlines the steps for generating a document.
           - Provides default implementations for some steps(`OpenDocument`, `SaveDocument`).
           - Declares an abstract method(`WriteContent`) to be implemented by subclasses.

        2. ** Concrete Classes(`PDFDocumentGenerator`, `WordDocumentGenerator`)**:
           - Extend the `DocumentGenerator` abstract class.
           - Implement the `WriteContent` method to provide specific content for PDF and Word documents.

        3. **Client Code (`Program` Class)**:
           - Creates instances of `PDFDocumentGenerator` and `WordDocumentGenerator`.
           - Calls the `GenerateDocument` method on each instance to generate documents.

        ### Output:

        When you run the `Main` method, you will see the following output:

        ```
        Generating PDF Document:
        Opening document...
        Writing PDF content...
        Saving document...

        Generating Word Document:
        Opening document...
        Writing Word content...
        Saving document...
        ```

        ### Summary:

        - The Template Method Design Pattern defines the skeleton of an algorithm in a base class
                while allowing subclasses to override specific steps of the algorithm without changing its structure.
        - The `DocumentGenerator` abstract class defines the template method `GenerateDocument` and provides default implementations for some steps.
        - The `PDFDocumentGenerator` and `WordDocumentGenerator` classes extend `DocumentGenerator` and implement the `WriteContent` method to provide specific content.
        - The client code demonstrates how to use the template method to generate different types of documents with a consistent structure.
         */


        // Abstract Class
        public abstract class DocumentGenerator
        {
            // Template Method
            public void GenerateDocument()
            {
                OpenDocument();
                WriteContent();
                SaveDocument();
            }

            // Steps with default implementation
            protected virtual void OpenDocument()
            {
                Console.WriteLine("Opening document...");
            }

            // Steps to be implemented by subclasses
            protected abstract void WriteContent();

            // Steps with default implementation
            protected virtual void SaveDocument()
            {
                Console.WriteLine("Saving document...");
            }
        }

        // Concrete Class for PDF Documents
        public class PDFDocumentGenerator : DocumentGenerator
        {
            protected override void WriteContent()
            {
                Console.WriteLine("Writing PDF content...");
            }
        }

        // Concrete Class for Word Documents
        public class WordDocumentGenerator : DocumentGenerator
        {
            protected override void WriteContent()
            {
                Console.WriteLine("Writing Word content...");
            }
        }

        // Client Code
        public static void Driver()
        {
            DocumentGenerator pdfGenerator = new PDFDocumentGenerator();
            DocumentGenerator wordGenerator = new WordDocumentGenerator();

            Console.WriteLine("Generating PDF Document:");
            pdfGenerator.GenerateDocument();

            Console.WriteLine();

            Console.WriteLine("Generating Word Document:");
            wordGenerator.GenerateDocument();
        }
    }

    public class Visitor_11
    {
        /*
         11 Visitor
            Scenario: When you need to define a new operation without changing the classes of the elements on which it operates.
            Example Use Cases:
            Adding operations to class hierarchies without modifying them
            Operations on composite objects

                    The Visitor Design Pattern is a behavioral pattern that allows you to add further operations to objects without having to modify them.
                It achieves this by using a visitor class that implements operations on elements of an object structure.
            The elements accept a visitor and let the visitor perform the operation.

            ### Implementation:

            Let's create an example where we have a `Shape` hierarchy with concrete classes like `Circle` and `Rectangle`.
            We'll create a `Visitor` interface and concrete visitors to calculate the area and perimeter of the shapes.
            ```

            ### Explanation:

            1. ** Visitor Interface(`IVisitor`)**:
               - Declares visit methods for each type of concrete element(`Visit(Circle circle)` and `Visit(Rectangle rectangle)`).

            2. ** Concrete Visitors(`AreaVisitor`, `PerimeterVisitor`)**:
               - Implement the `IVisitor` interface.
               - Provide specific implementations for calculating the area and perimeter of the shapes.

            3. **Element Interface (`IShape`)**:
               - Declares the `Accept` method that takes a visitor.

            4. ** Concrete Elements(`Circle`, `Rectangle`)**:
               - Implement the `IShape` interface.
               - Provide an `Accept` method that calls the visitor's `Visit` method.

            5. ** Client Code(`Program` Class)**:
               - Creates an array of shapes(`Circle` and `Rectangle`).
               - Creates visitors(`AreaVisitor` and `PerimeterVisitor`).
               - Iterates through the shapes and applies the visitors to calculate and print the area and perimeter.

            ### Output:

            When you run the `Main` method, you will see the following output:

            ```
            Calculating Area:
            Circle Area: 78.53981633974483
            Rectangle Area: 24

            Calculating Perimeter:
            Circle Perimeter: 31.41592653589793
            Rectangle Perimeter: 20
            ```

            ### Summary:

            - The Visitor Design Pattern allows you to add further operations to objects without modifying them.
            - The `IVisitor` interface declares visit methods for each type of concrete element.
            - The `AreaVisitor` and `PerimeterVisitor` classes implement the `IVisitor` interface to calculate the area and perimeter of shapes.
            - The `IShape` interface declares the `Accept` method that takes a visitor.
            - The `Circle` and `Rectangle` classes implement the `IShape` interface and provide an `Accept` method that calls the visitor's `Visit` method.
            - The client code demonstrates how to use the visitor pattern to calculate and print the area and perimeter of shapes.
         */



        // Visitor Interface
        public interface IVisitor
        {
            void Visit(Circle circle);
            void Visit(Rectangle rectangle);
        }

        // Concrete Visitor for calculating area
        public class AreaVisitor : IVisitor
        {
            public void Visit(Circle circle)
            {
                double area = Math.PI * circle.Radius * circle.Radius;
                Console.WriteLine($"Circle Area: {area}");
            }

            public void Visit(Rectangle rectangle)
            {
                double area = rectangle.Width * rectangle.Height;
                Console.WriteLine($"Rectangle Area: {area}");
            }
        }

        // Concrete Visitor for calculating perimeter
        public class PerimeterVisitor : IVisitor
        {
            public void Visit(Circle circle)
            {
                double perimeter = 2 * Math.PI * circle.Radius;
                Console.WriteLine($"Circle Perimeter: {perimeter}");
            }

            public void Visit(Rectangle rectangle)
            {
                double perimeter = 2 * (rectangle.Width + rectangle.Height);
                Console.WriteLine($"Rectangle Perimeter: {perimeter}");
            }
        }

        // Element Interface
        public interface IShape
        {
            void Accept(IVisitor visitor);
        }

        // Concrete Element for Circle
        public class Circle : IShape
        {
            public double Radius { get; set; }

            public Circle(double radius)
            {
                Radius = radius;
            }

            public void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        // Concrete Element for Rectangle
        public class Rectangle : IShape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public void Accept(IVisitor visitor)
            {
                visitor.Visit(this);
            }
        }

        // Client Code
        public static void Driver()
        {
            IShape[] shapes = new IShape[]
            {
                new Circle(5),
                new Rectangle(4, 6)
            };

            IVisitor areaVisitor = new AreaVisitor();
            IVisitor perimeterVisitor = new PerimeterVisitor();

            Console.WriteLine("Calculating Area:");
            foreach (var shape in shapes)
            {
                shape.Accept(areaVisitor);
            }

            Console.WriteLine("\nCalculating Perimeter:");
            foreach (var shape in shapes)
            {
                shape.Accept(perimeterVisitor);
            }
        }
    }
}
