# DesignPatterns
Design patterns in C#


1. CREATIONAL PATTERNS

1 Singleton ✅
Scenario: When you need exactly one instance of a class and global access to that instance.
Example Use Cases:
Logging
Configuration settings
Thread pools

2 Factory ✅
Scenario: When a class cannot anticipate the class of objects it needs to create or when a class wants its subclasses to specify the objects it creates.
Example Use Cases:
Creating objects without specifying the exact class
When the creation logic needs to be centralized

3 Abstract Factory ✅
Scenario: When you need to provide an interface for creating families of related or dependent objects without specifying their concrete classes.
Example Use Cases:
When the system needs to be independent of how its objects are created
When families of related objects need to be used together
When you want to provide a library of products without exposing implementation details

4 Builder ✅
Scenario: When you need to construct complex objects step by step and the construction process should allow different representations.
Example Use Cases:
Constructing complex objects with many parts
Building objects that require several steps
Fluent interfaces

5 Prototype
Scenario: When the cost of creating a new object is expensive or complex and you need to create a copy of an existing object.
Example Use Cases:
Creating objects that are costly to create
When objects should be independent of their classes
Cloning objects

--

2. STRUCTURAL PATTERNS

1 Adapter ✅
Scenario: When you need to make a class's interface compatible with another interface.
Example Use Cases:
Integrating with third-party libraries
Legacy system integration
Adapting one interface to another

2 Bridge
Scenario: When you need to decouple an abstraction from its implementation so that the two can vary independently.
Example Use Cases:
When you have a complex class hierarchy that can be simplified by separating different concerns
When you want to share an implementation among multiple objects

3 Composite
Scenario: When you need to treat individual objects and compositions of objects uniformly.
Example Use Cases:
Hierarchical tree structures
File systems
Graphic drawing editors

4 Decorator ✅
Scenario: When you need to add behavior or responsibilities to objects dynamically.
Example Use Cases:
Adding functionality to objects at runtime
Wrapping classes with additional behavior

5 Facade
Scenario: When you need to provide a unified interface to a set of interfaces in a subsystem.
Example Use Cases:
Simplifying complex systems
Providing a simple interface for complex libraries
Reducing dependencies between clients and subsystems

6 Flyweight
Scenario: When you need to minimize memory usage by sharing as much data as possible with similar objects.
Example Use Cases:
Caching
Managing large numbers of fine-grained objects efficiently
Text editors for character storage

7 Proxy
Scenario: When you need to provide a surrogate or placeholder for another object to control access to it.
Example Use Cases:
Lazy initialization
Access control
Remote proxies


3. BEHAVIORAL PATTERNS

1 Chain of Responsibility
Scenario: When you need to pass a request along a chain of handlers.
Example Use Cases:
Logging systems
Event handling systems
Processing sequences

2 Command ✅
Scenario: When you need to parameterize objects with operations, delay operations, or queue operations.
Example Use Cases:
Implementing undo/redo functionality
Task scheduling
Logging changes

3 Interpreter
Scenario: When you need to interpret sentences in a language.
Example Use Cases:
Simple scripting languages
Parsing expressions

4 Iterator ✅
Scenario: When you need to provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.
Example Use Cases:
Traversing collections (e.g., lists, trees, hash tables)
Providing uniform iteration interfaces for different types of collections

5 Mediator ✅
Scenario: When you need to reduce the complexity of communication between multiple objects.
Example Use Cases:
Implementing chat applications
Coordinating complex interactions between objects
Centralizing communication logic

6 Memento
Scenario: When you need to capture and restore an object's state.
Example Use Cases:
Undo mechanisms in applications
Saving/restoring states

7 Observer ✅
Scenario: When an object needs to notify other objects without knowing who or how many objects need to be notified.
Example Use Cases:
Event handling systems
Implementing distributed event-handling systems
GUI frameworks

8 State ✅
Scenario: When an object should change its behavior when its internal state changes.
Example Use Cases:
Implementing state machines
Workflow systems
Game development for character states

9 Strategy ✅
Scenario: When a class needs to use multiple algorithms interchangeably or when an algorithm needs to be selected and executed at runtime.
Example Use Cases:
Different sorting algorithms
Payment methods in an e-commerce application
Various compression algorithms

10 Template Method ✅
Scenario: When you need to define the skeleton of an algorithm in an operation, deferring some steps to subclasses.
Example Use Cases:
Algorithm framework
Game development for defining game loops
Workflow systems
11 Visitor
Scenario: When you need to define a new operation without changing the classes of the elements on which it operates.
Example Use Cases:
Adding operations to class hierarchies without modifying them
Operations on composite objects
